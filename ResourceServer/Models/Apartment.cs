using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.Models
{
    public class Apartment
    {
        public int ID_Ap { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String ApartmentNumber { get; set; }
        public String ImgThumb { get; set; }
        public String[] ImgList { get; set; }
        public decimal? Rate { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public String Description { get; set; }
        public string IDUser { get; set; }        
    }
}
