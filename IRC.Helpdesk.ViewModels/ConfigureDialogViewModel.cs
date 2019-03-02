using IRC.Helpdesk.Core;
using ME.Wpf.Mvvm;
using System;
using System.Windows.Input;

namespace IRC.Helpdesk.ViewModels
{
    public class ConfigureDialogViewModel : BaseViewModel, IClosable
    {
        #region Public Properties

        public int FirstRowIndex { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string InventoryNumber { get; set; }

        public string User { get; set; }

        public string DeliveryDate { get; set; }

        public event EventHandler<DialogClosedEventArgs> Closed;

        #endregion

        #region Private Fields

        private readonly IAssetSourceConfiguration _configuration;
        private readonly IDialogService _dialogService;

        #endregion

        #region Private Commands

        private ICommand _updateCommand;
        private ICommand _cancelCommand;

        #endregion

        #region Public Commands

        public ICommand UpdateCommand
        {
            get {
                if (this._updateCommand == null)
                    this._updateCommand = new RelayCommand(this.Update);
                return _updateCommand;
            }
        }

        public ICommand CancelCommand
        {
            get {
                if (this._cancelCommand == null)
                    this._cancelCommand = new RelayCommand(this.Cancel);
                return _cancelCommand;
            }
        }

        #endregion

        #region Constructors

        public ConfigureDialogViewModel(IAssetSourceConfiguration configuration, IDialogService dialogService)
        {
            this._configuration = configuration;
            this._dialogService = dialogService;
            Populate(configuration);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the AssetsSourceConfiguration on memory and on physical hard drive.
        /// </summary>
        public void Update()
        {
            if (!this.IsValid())
            {
                this._dialogService.ShowDialog(new MessageDialogViewModel("Invalid input", "Please input a valid configuration data. Empty fields are not allowed."));
                return;
            }
            var config = CreateConfiguration();
            this._configuration.Update(config);
            try
            {
                this._configuration.SaveChanges();
            }
            catch(Exception e)
            {
                this._dialogService.ShowDialog(new MessageDialogViewModel("Update failed", "Saving the changes failed. File note found or being used by other process."));
                return;
            }
            this.Closed?.Invoke(this, new DialogClosedEventArgs(true));
        }

        public void Cancel()
        {
            this.Closed?.Invoke(this, new DialogClosedEventArgs(false));
        }
        #endregion

        #region Private Helpers

        private void Populate(IAssetSourceConfiguration config)
        {
            var jsonConfig = config.GetJsonObject();
            this.FirstRowIndex = jsonConfig.FirstRow;
            this.Make = jsonConfig.MakeColumn;
            this.Model = jsonConfig.ModelColumn;
            this.InventoryNumber = jsonConfig.InventoryNumberColumn;
            this.User = jsonConfig.UserColumn;
            this.DeliveryDate = jsonConfig.DeliveryDateColumn;
        }

        /// <summary>
        /// Creates <see cref="JsonConfiguration"/> object from the viewmodel data.
        /// </summary>
        /// <returns></returns>
        private JsonConfiguration CreateConfiguration()
        {
            var config = new JsonConfiguration
            {
                FirstRow = this.FirstRowIndex,
                MakeColumn = this.Make,
                ModelColumn = this.Model,
                InventoryNumberColumn = this.InventoryNumber,
                UserColumn = this.User,
                DeliveryDateColumn = this.DeliveryDate
            };
            return config;
        }

        /// <summary>
        /// Checks if the viewmodel has the required data to create new <see cref="JsonConfiguration"/> object.
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (this.FirstRowIndex < 0)
                return false;
            if (string.IsNullOrWhiteSpace(this.Make))
                return false;
            if (string.IsNullOrWhiteSpace(this.Model))
                return false;
            if (string.IsNullOrWhiteSpace(this.InventoryNumber))
                return false;
            if (string.IsNullOrWhiteSpace(this.User))
                return false;
            if (string.IsNullOrWhiteSpace(this.DeliveryDate))
                return false;
            return true;
        }

        #endregion
    }
}
