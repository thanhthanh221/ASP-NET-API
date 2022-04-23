using System;

namespace BackEnd.Entities
{
    public class Warehouse : Entities
    {
        public Guid ProductId {get; set;}
        public Guid UserId {get; set;}
        public int Quantity {get; set;}
        public DateTimeOffset CreateTime {get; set;}
    }
}