using Domain_Layer.Base;
using Domain_Layer.Entities.Order;

namespace Domain_Layer.Entities
{
    public class WareHome : BaseEntity
    {
        public string name {get; set;}
        public Address address {get; set;}
        
    }
}