using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Product
{
    public class ImgAndVideoProduct : BaseEntity
    {
        public Guid ProductId {get; set;}
        public string Photo {get; set;}
    }
}