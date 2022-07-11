using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Order
{
    public class Address
    {
        public String City {get; set;}
        public String District {get; set;}
        public String Commune {get; set;}
        public String Street {get; set;}
    }
}