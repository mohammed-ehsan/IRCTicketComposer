using System.ComponentModel;

namespace IRC.Helpdesk.Core.POCOs
{
    public class AssetTicket : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Properties

        public string Make { get; set; }

        public string Model { get; set; }

        public string InventoryNumber { get; set; }

        public string User { get; set; }

        public string DelivaryDate { get; set; }

        public string Comment { get; set; }

        #endregion

    }
}
