using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Product
{
    public class Product : BaseEntity
    {
        public decimal Price {get; set;}
        public string Name {get; set;}
        public string Describe {get; set;}
        public DateTimeOffset DateTimeCreate {get; set;}
        public double numberOfStars {get; set;}
        
        public override bool Equals(object obj)
        {
             Product s = obj as Product;
            if(s == null){
                return false;
            }
            if(Name.Equals(s.Name)){
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (Name).GetHashCode();
        }

    }
}