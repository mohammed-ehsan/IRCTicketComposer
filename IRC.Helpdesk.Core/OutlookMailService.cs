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

        /// <summary>
        /// Compose new email with provided data. A new compose dialog from outlook will be opened.
        /// </summary>
        /// <param name="to">To part of the message</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="message">Message body in xml</param>
        public void Compose(string to, string subject, string message)
        {
            var app = new Outlook.Application();
            var mail = app.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            var i = mail.GetInspector;
            mail.To = to;
            mail.Subject = subject;
            mail.HTMLBody = message + mail.HTMLBody;
            //mail.Display(false);
            mail.Send();
        }

        /// <summary>
        /// Send the email directly without user interaction.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="messasge"></param>
        public void Send(string to, string subject, string messasge)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
