using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Order
{
    public class Address
    {
        public Address(string city,
            string district, 
            string commune, 
            string street)
        {
            City = city;
            District = district;
            Commune = commune;
            Street = street;
        }

        public String City {get; set;}
        public String District {get; set;}
        public String Commune {get; set;}
        public String Street {get; set;}
    }
}