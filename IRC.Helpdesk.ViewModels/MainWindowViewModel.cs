using IRC.Helpdesk.Core;
using IRC.Helpdesk.Core.POCOs;
using ME.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;


namespace IRC.Helpdesk.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IClosable
    {
        #region Private Properties

        /// <summary>
        /// Backup field for <see cref="MainCategory"/>
        /// </summary>
        private string _mainCategory;

        /// <summary>
        /// Backup field for <see cref="SecondaryCategory"/>
        /// </summary>
        private string _secondaryCategory;

        
        #endregion
        #region Public Properties

        /// <summary>
        /// Selected main category.
        /// </summary>
        public string MainCategory {
            get => _mainCategory;
            set {
                _mainCategory = value;
                this.SecondaryCategories = this.CategoriesProvider.GetSecondaryCategories(value);
            }
        }

        /// <summary>
        /// Selected secondary category.
        /// </summary>
        public string SecondaryCategory {
            get => _secondaryCategory;
            set {
                _secondaryCategory = value;
                this.DetailsList = this.CategoriesProvider.GetDetailsList(this.MainCategory, value);
            }
        }

        /// <summary>
        /// Selected details.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// List of main categories.
        /// </summary>
        public List<string> MainCategories { get; set; }

        /// <summary>
        /// List of secondary categories.
        /// </summary>
        public List<string> SecondaryCategories { get; set; }

        /// <summary>
        /// List of details.
        /// </summary>
        public List<string> DetailsList { get; set; }

        /// <summary>
        /// Categories provider.
        /// </summary>
        public ICategoriesProvider CategoriesProvider { get; set; }

        /// <summary>
        /// Mailing service provider.
        /// </summary>
        public IMailService MailService { get; set; }

        /// <summary>
        /// Dialog service provider.
        /// </summary>
        public IDialogService DialogService { get; set; }

        /// <summary>
        /// List of asstes needs to be setup.
        /// </summary>
        public ObservableCollection<AssetTicket> AssetsTickets { get; set; }

        /// <summary>
        /// Path for assets excel sheet.
        /// </summary>
        public string AssetsSourcePath { get; set; }

        /// <summary>
        /// Message composing service.
        /// </summary>
        public IMessageComposer MessageComposer { get; set; }

        public IAssetSource AssetsSource { get; set; }
        #endregion

        #region Private Commands

        private ICommand _composeTicketCommand;
        private ICommand _NewHelpdeskMailCommand;
        private ICommand _selectSourceFileCommand;

        public event EventHandler<DialogClosedEventArgs> Closed;

        #endregion

        #region Public Commands

        public ICommand ComposeTicketCommand {
            get {
                if (_composeTicketCommand == null)
                    _composeTicketCommand = new RelayCommand(this.ComposeTicket);
                return _composeTicketCommand;
            }
        }

        public ICommand NewHelpdeskMailCommand {
            get {
                if (_NewHelpdeskMailCommand == null)
                    _NewHelpdeskMailCommand = new RelayCommand(this.NewHelpdeskMail);
                return _NewHelpdeskMailCommand;
            }
        }

        public ICommand SelectSourceFileCommand
        {
            get {
                if (_selectSourceFileCommand == null)
                    _selectSourceFileCommand = new RelayCommand(this.SelectSource);
                return _selectSourceFileCommand;
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel(ICategoriesProvider categoriesProvider, 
            IMailService mailService, 
            IDialogService dialogService, 
            IMessageComposer messageComposer,
            IAssetSource assetsSource)
        {
            this.CategoriesProvider = categoriesProvider;
            this.MailService = mailService;
            this.DialogService = dialogService;
            this.MainCategories = categoriesProvider.MainCategories;
            this.MessageComposer = messageComposer;
            this.AssetsSource = assetsSource;
        }

        #endregion


        #region Public Methods

        public void ComposeTicket()
        {
            if (string.IsNullOrWhiteSpace(this.MainCategory))
            {
                DialogService.ShowDialog(new MessageDialogViewModel("Notice", "Please select main category first."));
                return;
            }
            if (string.IsNullOrWhiteSpace(this.SecondaryCategory))
            {
                DialogService.ShowDialog(new MessageDialogViewModel("Notice", "Please select sub category first."));
                return;
            }
            string subject = this.SecondaryCategory;
            if (!string.IsNullOrWhiteSpace(this.Details))
                subject += " - " + this.Details;
            string message = this.MessageComposer.ComposeTicket(this.MainCategory, this.SecondaryCategory, this.Details);
            MailService.Compose("helpdesk@rescue.org", subject, message);
        }

        public void NewHelpdeskMail()
        {
            MailService.Compose("helpdesk@rescue.org", string.Empty, string.Empty);
        }

        public void SelectSource()
        {
            string selectedPath = DialogService.ShowOpenFileDialog("Excel File|*.xlsx");
            if (string.IsNullOrWhiteSpace(selectedPath))
                return;
            if (File.Exists(selectedPath))
                this.AssetsSourcePath = selectedPath;
            Stream sourceStream = null;
            try
            {
                sourceStream = new FileStream(this.AssetsSourcePath, FileMode.Open);
                this.AssetsSource.SetSource(sourceStream);
            }
            catch (Exception)
            {
                //TODO: log data.
                throw;
            }
            finally
            {
                sourceStream.Dispose();
            }
            this.AssetsTickets = new ObservableCollection<AssetTicket>(this.AssetsSource.ReadAssets());
        }

        public void SubmitAssetsSetup()
        {
            foreach (var ticket in this.AssetsTickets)
            {
                MailService.Send("helpdesk@rescue.org", "Asset Setup", MessageComposer.ComposeAssetTicket(ticket));
            }
        }
        #endregion
    }
}
