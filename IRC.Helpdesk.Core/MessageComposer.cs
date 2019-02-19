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
        #region Public Properties

        public string ComposeAssetTicket(AssetTicket asset)
        {
            throw new NotImplementedException();
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
