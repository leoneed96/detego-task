using System.Threading.Tasks;
using Detego_RfidReaderAdapter;
using Detego_RfidReaderAdapter.Events;
using Detego.DAL;

namespace Detego.Communication
{
    public interface IStorageRfidReaderHandler: IRfidTagReadHandler
    {

    }
    public class StorageRfidReaderHandler : IStorageRfidReaderHandler
    {
        private IDataStorage _dataStorage;
        public StorageRfidReaderHandler(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public bool BreakOnError => true;

        public async Task<TagEventData> ProcessSeenTag(TagEventData tagEventData)
        {
            var record = _dataStorage.AddRecord(tagEventData.Identifier);
            tagEventData.RecordID = record.Id;
            return tagEventData;
        }
    }
}
