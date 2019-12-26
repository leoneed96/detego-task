using System;
using System.Collections.Generic;
using System.Text;

namespace Detego_RfidReaderAdapter
{
    public class RfidReaderState
    {
        public RfidReaderState(RfidReaderStatus status)
        {
            Status = status;
        }
        public RfidReaderStatus Status { get; set; }
        public bool IsActive => Status == RfidReaderStatus.Activated;
    }

    public enum RfidReaderStatus
    {
        Activated,
        Deactivated,
        Error
    }
}
