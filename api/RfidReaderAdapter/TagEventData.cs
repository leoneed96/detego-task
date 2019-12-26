using System;
using System.Collections.Generic;
using System.Text;

namespace Detego_RfidReaderAdapter.Events
{
    /// <summary>
    /// data that will be transfered from handler to handler
    /// identifier is internal set to avoid origin data to be owerriten
    /// recordID should be some events-shared DTO
    /// </summary>
    public class TagEventData
    {
        public string Identifier { get; internal set; }
        public string RecordID { get; set; }
    }
}
