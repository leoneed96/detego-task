using Detego_RfidReaderAdapter;
using Detego.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.Handlers
{
    /// <summary>
    /// Here you can inject any handler's abstractions. For example 
    /// - some reader logger
    /// - sending data to any client
    /// ect.
    /// </summary>
    public class HandlerRegistrator
    {
        private bool _registered;
        private readonly IRfidReaderAdapter _adapter;

        private readonly IStorageRfidReaderHandler _storageRfidReaderHandler;
        private readonly ISignalrTagReadHandler _signalrTagReadHandler;
        public HandlerRegistrator(IRfidReaderAdapter adapter,
            ISignalrTagReadHandler signalrTagReadHandler,
            IStorageRfidReaderHandler storageRfidReaderHandler)
        {
            _adapter = adapter;
            _storageRfidReaderHandler = storageRfidReaderHandler;
            _signalrTagReadHandler = signalrTagReadHandler;
        }

        public void Register()
        {
            if (_registered)
                throw new Exception("Handlers already registered");

            _adapter.AddHandler(_storageRfidReaderHandler);
            _adapter.AddHandler(_signalrTagReadHandler);

            _registered = true;
        }
    }
}
