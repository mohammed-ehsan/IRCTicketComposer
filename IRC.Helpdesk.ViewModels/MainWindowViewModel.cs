using IRC.Helpdesk.Core;
using ME.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace IRC.Helpdesk.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IClosable
    {
        #region Private Properties

        private string _mainCategory;
        private string _secondaryCategory;


        #endregion
        #region Public Properties

        public string MainCategory {
            get => _mainCategory;
            set {
                _mainCategory = value;
                this.SecondaryCategories = this.CategoriesProvider.GetSecondaryCategories(value);
            }
        }

        public string SecondaryCategory {
            get => _secondaryCategory;
            set {
                _secondaryCategory = value;
                this.DetailsList = this.CategoriesProvider.GetDetailsList(this.MainCategory, value);
            }
        }

        public string Details { get; set; }

        public List<string> MainCategories { get; set; }

        public List<string> SecondaryCategories { get; set; }

        public List<string> DetailsList { get; set; }

        public ICategoriesProvider CategoriesProvider { get; set; }

        public IMailService MailService { get; set; }

        public IDialogService DialogService { get; set; }

        #endregion

        #region Private Commands

        private ICommand _composeTicketCommand;
        private ICommand _NewHelpdeskMailCommand;

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

        #endregion

        #region Constructors

        public MainWindowViewModel(ICategoriesProvider categoriesProvider, IMailService mailService, IDialogService dialogService)
        {
            this.CategoriesProvider = categoriesProvider;
            this.MailService = mailService;
            this.DialogService = dialogService;
            this.MainCategories = categoriesProvider.MainCategories;
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
            string message = string.Format(@"<P>Dear IT Team,<br><br>
                Please I need help with the following issue:<br>
                <b>Main Category:</b> {0}<br>
                <b>Secondary Category:</b> {1}<br>
                <b>Details:</b> {2}<br><br>
                Regards.</P>", MainCategory, SecondaryCategory, Details);
            MailService.Compose("helpdesk@rescue.org", subject, message);
        }

        public void NewHelpdeskMail()
        {
            MailService.Compose("helpdesk@rescue.org", string.Empty, string.Empty);
        }

        #endregion
    }
}
