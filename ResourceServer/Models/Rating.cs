using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.Models
{
    public class Rating
    {
        public int ID_Rating { get; set; }
        public int Owner { get; set; }
        public int Location { get; set; }
        public int Standard { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string IDUser { get; set; }
        public int IDAp { get; set; }
    }
}
