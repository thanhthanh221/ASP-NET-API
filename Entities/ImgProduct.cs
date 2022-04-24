using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BackEnd.Entities
{
    public class ImgProduct : Entities
    {
        public Guid ProductId {get; set;}
        public byte[] Photo {get; set;}
        
    }
}