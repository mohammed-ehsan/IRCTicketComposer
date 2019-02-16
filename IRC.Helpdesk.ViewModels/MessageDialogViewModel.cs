using ME.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IRC.Helpdesk.ViewModels
{
    public class MessageDialogViewModel : BaseViewModel, IClosable
    {
        #region Public Properties

        public string Title { get; set; }

        public string Message { get; set; }

        #endregion


        #region Private Commands

        private ICommand _okCommand;

        public event EventHandler<DialogClosedEventArgs> Closed;

        #endregion

        #region Public Commands

        public ICommand OkCommand
        {
            get {
                if (_okCommand == null)
                    _okCommand = new RelayCommand(this.CloseDialog);
                return _okCommand;

            }

        }

        #endregion

        #region Constructors

        public MessageDialogViewModel(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        #endregion

        #region Public Methods

        public void CloseDialog()
        {
            this.Closed?.Invoke(this, new DialogClosedEventArgs(true));
        }

        #endregion
    }
}
