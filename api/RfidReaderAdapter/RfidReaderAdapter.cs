using Detego_RfidReaderAdapter.Events;
using Detego_RfidReaderAdapter.Exceptions;
using Rfid;
using System;
using System.Collections.Generic;

namespace Detego_RfidReaderAdapter
{
    /// <summary>
    /// An adapter to encapsulate RfidReader 
    /// Should be registered as singleton to provide single instance of reader
    /// </summary>
    public class RfidReaderAdapter : IRfidReaderAdapter
    {
        private readonly RfidReader _reader;
        private readonly List<IRfidTagReadHandler> _handlers = new List<IRfidTagReadHandler>();
        public void AddHandler(IRfidTagReadHandler handler) => _handlers.Add(handler);

        public RfidReaderAdapter()
        {
            _reader = new RfidReader();
            // to ensure reader is in deactivated state cos RfidReader doesn't provide state
            _reader.Deactivate();
            _reader.TagSeen += async (sender, e) =>
            {
                if (sender is RfidReader)
                {
                    var data = new TagEventData() { Identifier = e.Identifier };
                    foreach (var handler in _handlers)
                    {
                        try
                        {
                            data = await handler.ProcessSeenTag(data);
                        }
                        catch(Exception ex)
                        {
                            if (handler.BreakOnError)
                            {
                                break;
                                throw ex;
                            }
                        }
                    }
                }
            };
        }

        public RfidReaderStatus Status { get; private set; } = RfidReaderStatus.Deactivated;

        public RfidReader Reader { get; set; }

        public void Activate()
        {
            try
            {
                _reader.Activate();
                Status = RfidReaderStatus.Activated;
            }
            catch (Exception ex)
            {
                Status = RfidReaderStatus.Error;
                throw new RfidReaderCommunicationException("An error has occured while attempting to activate reader", ex);
            }
        }

        public void Deactivate()
        {
            try
            {
                _reader.Deactivate();
                Status = RfidReaderStatus.Deactivated;
            }
            catch (Exception ex)
            {
                Status = RfidReaderStatus.Error;
                throw new RfidReaderCommunicationException("An error has occured while attempting to deactivate reader", ex);
            }
        }

        public RfidReaderState GetState() => 
            new RfidReaderState(Status);
    }
}
