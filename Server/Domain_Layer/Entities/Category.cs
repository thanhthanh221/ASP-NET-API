using Domain_Layer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain_Layer.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
        }

        public Category(Guid Id,
            string name,
            string imgCategory, 
            IEnumerable<Guid> parentsCategoryId, 
            IEnumerable<Guid> subCategoryId)
        {
            this.Id = Id;
            this.name = name;
            this.imgCategory = imgCategory;
            this.parentsCategoryId = parentsCategoryId;
            this.subCategoryId = subCategoryId;
        }

        [Required]
        public String name {get; set;} 
        [Required]
        public String imgCategory {get; set;}
        public IEnumerable<Guid> parentsCategoryId {get; set;} // danh mục cha
        public IEnumerable<Guid> subCategoryId {get; set;}     // Danh mục con
    }
}