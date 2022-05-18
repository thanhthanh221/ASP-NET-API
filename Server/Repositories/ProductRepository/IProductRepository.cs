using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using BackEnd.Entities;

namespace BackEnd.Repositories
{
    public interface IProductRepository
    {
        Task<IReadOnlyCollection<Product>> GetAllAsync();
        Task<Product> GetIdAsync(Guid Id);
        Task<Boolean> CreateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}