using System.ComponentModel.DataAnnotations;
using System;

namespace BackEnd.Dto{
    public record DeleteItem{
        public  Guid id{get; init;}
        
    }
}