using System;
using Domain_Layer.Base;

namespace BackEnd.Entities
{
    public class Repository : BaseEntity
    {
        public Guid WarehomeId {get; set;}
        public Guid UserId {get; set;}
        public int Quantity {get; set;}
        public DateTimeOffset CreateTime {get; set;}
    }
}