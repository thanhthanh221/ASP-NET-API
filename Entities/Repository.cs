using System;

namespace BackEnd.Entities
{
    public class Repository : Entities
    {
        public Guid WarehomeId {get; set;}
        public Guid UserId {get; set;}
        public int Quantity {get; set;}
        public DateTimeOffset CreateTime {get; set;}
    }
}