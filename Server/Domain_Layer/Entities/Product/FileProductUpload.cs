using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Product
{
    public class FileProductUpload : BaseEntity
    {
        public Guid CommentId {get; set;}
        public Guid UserId {get; set;}
        public string Photo {get; set;} 
    }
}