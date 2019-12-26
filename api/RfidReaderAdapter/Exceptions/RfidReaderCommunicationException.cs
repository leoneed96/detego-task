using System;
using System.Collections.Generic;
using System.Text;

namespace Detego_RfidReaderAdapter.Exceptions
{

    [Serializable]
    public class RfidReaderCommunicationException : Exception
    {
        public RfidReaderCommunicationException() { }
        public RfidReaderCommunicationException(string message) : base(message) { }
        public RfidReaderCommunicationException(string message, Exception inner) : base(message, inner) { }
        protected RfidReaderCommunicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
