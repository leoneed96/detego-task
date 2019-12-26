using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.Model
{
    public class Transponder: IModel
    {
        // Unique
        public string Id { get; set; }
        public long SeenCount { get; set; }
    }
}
