using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Entities
{
    public class Users : Entities
    {
        [Required]
        public String Name {get; set;}
        [Required]
        public String Email {get; set;}
        [Required]
        public String PassWord {get; set;}
    }
}