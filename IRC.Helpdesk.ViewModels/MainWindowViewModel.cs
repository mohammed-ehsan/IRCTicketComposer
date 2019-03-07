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
        public string MainCategory
        {
            get => _mainCategory;
            set {
                _mainCategory = value;
                this.SecondaryCategories = this.CategoriesProvider.GetSecondaryCategories(value);
            }
        }

        /// <summary>
        /// Selected secondary category.
        /// </summary>
        public string SecondaryCategory
        {
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

        /// <summary>
        /// Assets source provider.
        /// </summary>
        public IAssetSource AssetsSource { get; set; }

        /// <summary>
        /// Clipboard service.
        /// </summary>
        public IClipBoard Clipboard { get; set; }

        /// <summary>
        /// Currently selected asset. nll if no assets is selected.
        /// </summary>
        public AssetTicket SelectedAsset { get; set; }

        public int AssetsCount
        {
            get => this.AssetsTickets.Count;
        }

        /// <summary>
        /// Global comment value to be applied to all assets.
        /// </summary>
        public string GlobalComment { get; set; }

        /// <summary>
        /// Apply global comment for all assets.
        /// </summary>
        public bool ApplyGlobalComment { get; set; }

        public new PropertyChangedEventHandler PropertyChanged;

        public ISettingsProvider SettingsProvider { get; set; }
        #endregion

        #region Private Commands

        private ICommand _composeTicketCommand;
        private ICommand _NewHelpdeskMailCommand;
        private ICommand _selectSourceFileCommand;
        private ICommand _configureCommand;
        private ICommand _pasteCommand;
        private ICommand _deleteSelectedCommand;
        private ICommand _clearAllCommand;
        private ICommand _showAboutCommand;
        private ICommand _sendAppsSetupCommand;

        public event EventHandler<DialogClosedEventArgs> Closed;

        #endregion

        #region Public Commands

        public ICommand SendAppsSetupCommand
        {
            get {
                if (this._sendAppsSetupCommand == null)
                    this._sendAppsSetupCommand = new RelayCommand(this.SendAppsSetupTickets);
                return _sendAppsSetupCommand;
            }

        }

        public ICommand ConfigureCommand
        {
            get {
                if (_configureCommand == null)
                    _configureCommand = new RelayCommand(this.Configure);
                return _configureCommand;
            }
        }

        public ICommand ComposeTicketCommand
        {
            get {
                if (_composeTicketCommand == null)
                    _composeTicketCommand = new RelayCommand(this.SubmitAssetsSetup);
                return _composeTicketCommand;
            }
        }

        public ICommand NewHelpdeskMailCommand
        {
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

        public ICommand PasteCommand
        {
            get {
                if (_pasteCommand == null)
                    _pasteCommand = new RelayCommand(this.Paste);
                return _pasteCommand;
            }
        }

        public ICommand DeleteSelectedCommand
        {
            get {
                if (_deleteSelectedCommand == null)
                    _deleteSelectedCommand = new RelayCommand(this.DeleteSelected);
                return _deleteSelectedCommand;
            }
        }

        public ICommand ClearAllCommand
        {
            get {
                if (_clearAllCommand == null)
                    _clearAllCommand = new RelayCommand(this.ClearAll);
                return _clearAllCommand;
            }
        }

        public ICommand ShowAboutCommand
        {
            get {
                if (_showAboutCommand == null)
                    _showAboutCommand = new RelayCommand(this.ShowAbout);
                return _showAboutCommand;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="categoriesProvider">Categories provider for tickets generating.</param>
        /// <param name="mailService">Emailing service provider.</param>
        /// <param name="dialogService">Dialog service provider.</param>
        /// <param name="messageComposer">Message composing service.</param>
        /// <param name="assetsSource">Assets source provider.</param>
        /// <param name="clipboard">Clipboard access provider.</param>
        public MainWindowViewModel(ICategoriesProvider categoriesProvider,
            IMailService mailService,
            IDialogService dialogService,
            IMessageComposer messageComposer,
            IAssetSource assetsSource,
            IClipBoard clipboard,
            ISettingsProvider settingsProvider)
        {
            this.CategoriesProvider = categoriesProvider;
            this.MailService = mailService;
            this.DialogService = dialogService;
            this.MainCategories = categoriesProvider.MainCategories;
            this.MessageComposer = messageComposer;
            this.AssetsSource = assetsSource;
            this.Clipboard = clipboard;
            this.SettingsProvider = settingsProvider;
            this.AssetsTickets = new ObservableCollection<AssetTicket>();
            this.AssetsTickets.CollectionChanged += AssetsTickets_CollectionChanged;
        }

        private void AssetsTickets_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.RaisePropertyChanged(nameof(this.AssetsCount));
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Compose a single ticket fro specified incident.
        /// </summary>
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
            MailService.Compose(SettingsProvider.GetSettings().To, subject, message);
        }

        public void NewHelpdeskMail()
        {
            MailService.Compose(SettingsProvider.GetSettings().To, string.Empty, string.Empty);
        }

        /// <summary>
        /// Select an excel file source for assets to make setup tickets.
        /// </summary>
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
                var assets = this.AssetsSource.ReadAssets();
                foreach (var item in assets)
                {
                    this.AssetsTickets.Add(item);
                }
            }
            catch (IOException e)
            {
                //TODO: log data.
                this.DialogService.ShowDialog<MessageDialogViewModel>(new MessageDialogViewModel("IO error", "The application can not access the file because it is being used by other process."));
            }
            finally
            {
                if (sourceStream != null)
                    sourceStream.Dispose();
            }
        }

        /// <summary>
        /// Submit the tickets arranged in the datagrid.
        /// </summary>
        public void SubmitAssetsSetup()
        {
            foreach (var item in this.AssetsTickets)
            {
                sendTicket(item,"Computer Setup", MessageComposer.ComposeSetupTicket(item));
            }
        }

        /// <summary>
        /// Paste into the grid the copied assets from excel sheet.
        /// </summary>
        public void Paste()
        {
            var result = this.AssetsSource.ReadAssets(Clipboard.GetText());
            if (result == null)
                return;
            foreach (var item in result)
            {
                this.AssetsTickets.Add(item);
            }
        }

        /// <summary>
        /// Delete currently selected asset from the assets grid.
        /// </summary>
        public void DeleteSelected()
        {
            if (this.SelectedAsset == null)
                return;
            this.AssetsTickets.Remove(this.SelectedAsset);
        }

        /// <summary>
        /// Clear all assets.
        /// </summary>
        public void ClearAll()
        {
            this.AssetsTickets.Clear();
        }

        /// <summary>
        /// Open the excel configuration dialog.
        /// </summary>
        public void Configure()
        {
            DialogService.ShowDialog(new ConfigureDialogViewModel(DI.GetService<IAssetSourceConfiguration>(), this.DialogService));
        }

        /// <summary>
        /// Open the about dialog.
        /// </summary>
        public void ShowAbout()
        {
            DialogService.ShowDialog(new AboutViewModel());
        }

        public void SendAppsSetupTickets()
        {
            foreach (var item in this.AssetsTickets)
            {
                sendTicket(item, "KACE and MS Office Setup", MessageComposer.ComposeKACEMSOfficeTicket(item));
            }
        }
        #endregion

        #region Private Helpers

        private void sendTicket(AssetTicket ticket, string subject, string message)
        {
            if (ticket.Sent)
                return;
            if (this.ApplyGlobalComment)
                ticket.Comment = this.GlobalComment;
            else
                ticket.Comment = null;
            try
            {
                MailService.Compose(SettingsProvider.GetSettings().To, subject, message);
            }
            catch (OutlookException)
            {
                this.DialogService.ShowDialog(new MessageDialogViewModel("Application Error", "Failed to access Outlook application, please check the application is installed and running."));
                return;
            }
            catch (SendEmailException)
            {
                this.DialogService.ShowDialog(new MessageDialogViewModel("Email Error", "Failed to send email due to application being busy with other process, has open dialog, or has permission grant required."));
                return;
            }
            ticket.Sent = true;

        }

        #endregion
    }
}
