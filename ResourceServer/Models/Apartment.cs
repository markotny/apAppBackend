using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.Models
{
    public class Apartment
    {
        public int ID_Apartment { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String Address { get; set; }
        public String ImgThumb { get; set; }
        public decimal? Rate { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int IDUser { get; set; }        
    }
}
