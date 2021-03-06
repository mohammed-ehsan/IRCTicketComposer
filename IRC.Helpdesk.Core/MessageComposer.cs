﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRC.Helpdesk.Core.POCOs;

namespace IRC.Helpdesk.Core
{
    public class MessageComposer : IMessageComposer
    {
        #region Public Methods

        public string ComposeKACEMSOfficeTicket(AssetTicket asset)
        {
            string commentBody = string.Empty;
            if (!string.IsNullOrWhiteSpace(asset.Comment))
                commentBody = string.Format(@"<br><b>Note:</b> {0}", asset.Comment);
            string message = string.Format(
                @"<P>Dear IT Team,<br><br>
Please setup KACE and MS Office on computer asset number <b>‘{0}’</b>, Make <b>‘{1}’</b> and Model <b>‘{2}’</b>{3}.<br><br><br>Created By IRC Assets Setup Application<br>Regards,</P>"
                , asset.InventoryNumber, asset.Make, asset.Model, commentBody);
            return message;
        }

        public string ComposeSetupTicket(AssetTicket asset)
        {
            string commentBody = string.Empty;
            if (!string.IsNullOrWhiteSpace(asset.Comment))
                commentBody = string.Format(@"<br><b>Note:</b> {0}",asset.Comment);
            string message = string.Format(
                @"<P>Dear IT Team,<br><br>
Please configure the following computer <b>‘{0} - {1}’</b> with asset tag <b>‘{2}’</b> to the user <b>‘{3}’</b> in <b>‘{4}’</b>, expected delivery date <b>{5}</b>.{6}<br><br>Created By IRC Assets Setup Application<br>Regards,</P>"
                , asset.Make, asset.Model, asset.InventoryNumber, asset.User,asset.Location, asset.DeliveryDate.ToShortDateString(), commentBody);
            return message;
        }

        public string ComposeTicket(string mainCategory, string secondaryCategory, string details)
        {
            string message = string.Format(@"<P>Dear IT Team,<br><br>
                Please I need help with the following issue:<br>
                <b>Main Category:</b> {0}<br>
                <b>Secondary Category:</b> {1}<br>
                <b>Details:</b> {2}<br><br>
                Regards.</P>", mainCategory, secondaryCategory, details);
            return message;
        }

        #endregion
    }
}
