using BackEnd.Dto;
using System;
using System.Linq;
using System.Collections.Generic;
using Domain_Layer.Entities.Product;

namespace API.Extension
{
    public static class ProductLinq
    {
        public static IEnumerable<GetProductDto> ProductAndImgDto(
            IEnumerable<IGrouping<Guid, ImgAndVideoProduct>> imgCheck ,
            IEnumerable<Product> products)
        {
            IEnumerable<GetProductDto> ListProductReturn  =  from product in products
                                join img in imgCheck on product.Id equals img.Key into t
                                from img in t.DefaultIfEmpty()
                                orderby product.Name                      
                                select product.ToProductDto() with {
                                    files = (img == null) ? null : img.Select(p => p.Photo)
                                };
            return ListProductReturn;
        }
        public static IEnumerable<IGrouping<Guid, ImgAndVideoProduct>> GroupImgProductDto(
                        IEnumerable<ImgAndVideoProduct> FullFileProduct)
        {
            var GroupById = from a in FullFileProduct
                            orderby a.Id
                            group a by a.ProductId;

            return GroupById;
        }
        public static IEnumerable<String> AddFileProductDto()
        {
            return null;

        }
        
    }
}