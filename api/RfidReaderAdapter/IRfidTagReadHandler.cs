using Detego_RfidReaderAdapter.Events;
using System.Threading.Tasks;

namespace Detego_RfidReaderAdapter
{
    public interface IRfidTagReadHandler
    {
        Task<TagEventData> ProcessSeenTag(TagEventData tagEventData);
        /// <summary>
        /// If true, next event handlers will not be activated if current handler throws an exception
        /// </summary>
        bool BreakOnError { get; }
    }
}
