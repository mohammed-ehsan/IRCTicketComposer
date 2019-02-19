using System;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace IRC.Helpdesk.Core
{
    public class OutlookMailService : IMailService
    {
        #region Constructors

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public OutlookMailService()
        {

        }

        #endregion

        #region Public Methods

        public void Compose(string to, string subject, string message)
        {
            var app = new Outlook.Application();
            var mail = app.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            var i = mail.GetInspector;
            mail.To = to;
            mail.Subject = subject;
            mail.HTMLBody = message + mail.HTMLBody;
            mail.Display(false);
        }

        public void Send(string to, string subject, string messasge)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
