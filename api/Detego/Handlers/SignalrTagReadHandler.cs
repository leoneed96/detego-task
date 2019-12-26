using Detego_RfidReaderAdapter;
using Detego_RfidReaderAdapter.Events;
using Detego.DAL;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Detego.Communication
{
    public interface ISignalrTagReadHandler : IRfidTagReadHandler
    {

    }
    public class SignalrTagReadHandler : ISignalrTagReadHandler
    {
        private readonly IHubContext<TagHub> _hubContext;
        private readonly IDataStorage _dataStorage;
        public SignalrTagReadHandler(IHubContext<TagHub> hubContext,
            IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            _hubContext = hubContext;
        }

        public bool BreakOnError => true;

        public async Task<TagEventData> ProcessSeenTag(TagEventData tagEventData)
        {
            if (string.IsNullOrEmpty(tagEventData.RecordID))
                throw new System.Exception("RecordID cannot be empty. Ensure this handler added after IStorageRfidReaderCommunicator");
            var data = _dataStorage.GetRecord(tagEventData.RecordID);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "add", data);
            return tagEventData;
        }
    }
}
