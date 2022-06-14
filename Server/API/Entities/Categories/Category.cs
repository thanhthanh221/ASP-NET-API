using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Entities
{
    public class Category : Entities
    {
        [Required]
        public String name {get; set;} 
        [Required]
        public String imgCategory {get; set;}
        // Danh sách mã sản phẩm
        public IEnumerable<Guid> products {get; set;}
        public IEnumerable<Guid> parentsCategoryId {get; set;} // danh mục cha
        public IEnumerable<Guid> subCategoryId {get; set;}     // Danh mục con

    }
}