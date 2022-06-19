using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Product
{
    public class ImgProductReview : BaseEntity
    {
        public Guid CommentId {get; set;}
        public string Photo {get; set;}      
    }
}