using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
namespace BackEnd.Entities
{
    public class ImgProduct : Entities
    {
        public Guid ProductId {get; set;}
        public string Photo {get; set;}
    }
}