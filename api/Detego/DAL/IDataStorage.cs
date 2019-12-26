using Detego.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.DAL
{
    public interface IDataStorage
    {
        ICollection<ReadRecord> GetRecords();
        ReadRecord GetRecord(string recordID);
        ReadRecord AddRecord(string transponderId);
        void RemoveRecord(string recordId);
    }
}
