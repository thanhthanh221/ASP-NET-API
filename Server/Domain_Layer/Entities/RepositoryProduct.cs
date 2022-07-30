using System;
using Domain_Layer.Base;
using Domain_Layer.Entities;

namespace Domain_Layer.Entities
{
    public class RepositoryProduct : BaseEntity
    {
        public WareHome wareHome  {get; set;}
        public Guid userId {get; set;}
        public int quantity {get; set;}
        public DateTimeOffset createTime {get; set;}
    }
}