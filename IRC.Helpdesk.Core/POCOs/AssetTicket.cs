using System.ComponentModel;

namespace IRC.Helpdesk.Core.POCOs
{
    public class AssetTicket : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Properties

        public string MainCategory { get; set; }

        public string SubCategory { get; set; }

        public string InventoryNumber { get; set; }

        public string SerialNumber { get; set; }

        public string Location { get; set; }

        public string SubLocation { get; set; }

        #endregion

    }
}
