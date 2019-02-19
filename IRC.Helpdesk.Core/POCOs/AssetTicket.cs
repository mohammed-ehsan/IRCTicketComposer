﻿using System.ComponentModel;

namespace IRC.Helpdesk.Core.POCOs
{
    public class AssetTicket : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Properties

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string AssetID { get; set; }

        public string SerailNumber { get; set; }

        public string Location { get; set; }

        public string SubLocation { get; set; }

        #endregion

        #region Public Methods


        #endregion
    }
}