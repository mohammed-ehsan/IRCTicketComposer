using System;

namespace IRC.Helpdesk.Core
{

    /// <summary>
    /// Throw this outlook to indicate a problem with outlook application like not being installed or not opened.
    /// </summary>
    [Serializable]
    public class OutlookException : Exception
    {
        public OutlookException() { }
        public OutlookException(string message) : base(message) { }
        public OutlookException(string message, Exception inner) : base(message, inner) { }
        protected OutlookException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
