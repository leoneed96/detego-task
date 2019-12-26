using static Detego_RfidReaderAdapter.RfidReaderAdapter;

namespace Detego_RfidReaderAdapter
{
    public interface IRfidReaderAdapter
    {
        RfidReaderState GetState();
        void Activate();
        void Deactivate();
        void AddHandler(IRfidTagReadHandler handler);
    }
}
