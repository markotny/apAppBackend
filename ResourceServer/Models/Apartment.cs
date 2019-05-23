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
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string ImgThumb { get; set; }
        public string[] ImgList { get; set; }
        public decimal? Rate { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string IDUser { get; set; }        
    }
}
