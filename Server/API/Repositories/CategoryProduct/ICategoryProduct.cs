using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Entities;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public interface ICategoryProduct
    {
        Task<IEnumerable<Category>> GetCategorysAsync();   
        Task<Category> GetCategoryAsync(Guid id);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        
         
    }
}