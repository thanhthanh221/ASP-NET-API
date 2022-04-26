using System;
using System.Collections.Generic;
using BackEnd.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace BackEnd.Repositories 
{
    public interface IImgProduct
    {
        Task<IReadOnlyCollection<ImgProduct>> GetImgProductsUserAsync(Guid ProductId);
        Task<ImgProduct> GetImgProductAsync(Guid Id);   
        Task CreateImgProductAsync(ImgProduct imgProduct);
        Task UpdateImgProductAsync(Guid Id, ImgProduct imgProduct);
        Task DeleteImgProductAsync(ImgProduct imgProduct);
    }
}