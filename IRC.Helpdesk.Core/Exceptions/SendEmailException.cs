using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.Core
{

    /// <summary>
    /// Throw this exception when sending emial fails.
    /// </summary>
    [Serializable]
    public class SendEmailException : Exception
    {
        public SendEmailException() { }
        public SendEmailException(string message) : base(message) { }
        public SendEmailException(string message, Exception inner) : base(message, inner) { }
        protected SendEmailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
