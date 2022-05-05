using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Entities
{
    public class Category : Entities
    {
        public IEnumerable<Guid> parentsCategoryId {get; set;} // danh mục cha
        public IEnumerable<Guid> subCategoryId {get; set;}     // Danh mục con
        [Required]
        public string name {get; set;} 
        [Required]
        public string ImgCategory {get; set;}
        // Danh sách mã sản phẩm
        public IEnumerable<Guid> products {get; set;}

    }
}