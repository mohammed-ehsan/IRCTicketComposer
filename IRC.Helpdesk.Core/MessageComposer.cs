using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRC.Helpdesk.Core.POCOs;

namespace IRC.Helpdesk.Core
{
    public class MessageComposer : IMessageComposer
    {
        public const string Message = @"<P>Dear IT Team,<br><br>
Please configure the following computer <b>‘{0}’</b> with asset tag <b>‘{1}’</b> to the user <b>‘{2}’</b>, <b>‘{3}’>/b>, expected delivery date <b>{4}</b>.</P>";
        #region Public Methods

        public string ComposeAssetTicket(AssetTicket asset)
        {
            string message = string.Format(
                @"<P>Dear IT Team,<br><br>
Please configure the following computer <b>‘{0} - {1}’</b> with asset tag <b>‘{2}’</b> to the user <b>‘{3}’</b>, expected delivery date <b>{4}</b>.</P>"
                , asset.Make, asset.Model, asset.InventoryNumber, asset.User, asset.DelivaryDate);
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
