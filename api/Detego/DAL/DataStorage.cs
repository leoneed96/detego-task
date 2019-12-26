using Detego.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.DAL
{
    public class DataStorage : IDataStorage
    {
        public DataStorage()
        {

        }
        private ICollection<ReadRecord> _readRecords = new List<ReadRecord>();
        private ICollection<Transponder> _transponders = new List<Transponder>();

        public ReadRecord AddRecord(string transponderId)
        {
            var transponder = _transponders.SingleOrDefault(x => x.Id == transponderId);
            if (transponder == null)
            {
                transponder = new Transponder()
                {
                    Id = transponderId,
                    SeenCount = 1
                };
                _transponders.Add(transponder);
            }
            else
            {
                transponder.SeenCount++;
            }
            var record = new ReadRecord()
            {
                Transponder = transponder,
                ReadDate = DateTime.Now
            };
            _readRecords.Add(record);
            return record;
        }

        public ICollection<ReadRecord> GetRecords()
        {
            return _readRecords;
        }

        public ReadRecord GetRecord(string recordID)
        {
            var record = _readRecords.SingleOrDefault(x => x.Id == recordID);
            if (record == null)
                throw new Exception($"Record with id {record} was not found");
            return record;
        }

        public void RemoveRecord(string recordId)
        {
            var record = _readRecords.Where(x => x.Id == recordId)
                .SingleOrDefault();
            if (record == null)
                throw new Exception($"Record with id {record} was not found");
            _readRecords.Remove(record);
            record.Transponder.SeenCount--;
        }
    }
}
