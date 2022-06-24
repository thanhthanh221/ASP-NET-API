using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Product
{
    public class ImgAndVideoProduct : BaseEntity
    {
        public string Photo {get; set;}
        public Guid ProductId {get; set;}
    }
}