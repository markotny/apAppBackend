using System;

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
        public double OwnerRating { get; set; }
        public double LocationRating { get; set; }
        public double StandardRating { get; set; }
        public double PriceRating { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public String Description { get; set; }
        public string IDUser { get; set; }        
    }
}
