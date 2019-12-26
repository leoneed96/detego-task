using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.Model
{
    public class ReadRecord: IModel
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public Transponder Transponder { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
