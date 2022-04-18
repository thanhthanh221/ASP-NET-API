using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entities
{
    public class User 
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name {get; set;}
        [Required]
        public String Email {get; set;}
        [Required]
        public String PassWord {get; set;}
    }
}