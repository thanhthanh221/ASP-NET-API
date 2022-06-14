using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Entities;

namespace BackEnd.Repositories
{
    public interface IProductsReviews
    {
        Task<IEnumerable<ProductsReviews>> GetProductsReviewsAsync(Guid ProductId);   
        Task CreateProductsReviewsAsync(ProductsReviews productsReviews);
        // Sửa Bình Luận chỉ có người Bình Luận mới được sửa nó - Xóa nó
        Task UpdateProductsReviewsAsync(ProductsReviews productsReviews); 
        Task DeleteCategoryAsync(ProductsReviews productsReviews);
    }
}