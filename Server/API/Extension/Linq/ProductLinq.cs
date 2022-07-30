// using BackEnd.Dto;
// using System;
// using System.Linq;
// using System.Collections.Generic;
// using Domain_Layer.Entities.Product;

// namespace API.Extension
// {
//     // public static class ProductLinq
//     // {
//     //     public static IEnumerable<GetProductDto> ProductAndImgDto(
//     //         IEnumerable<IGrouping<Guid, ImgAndVideoProduct>> imgCheck ,
//     //         IEnumerable<Product> products)
//     //     {
//     //         IEnumerable<GetProductDto> ListProductReturn  =  from product in products
//     //                             join img in imgCheck on product.Id equals img.Key into t
//     //                             from img in t.DefaultIfEmpty()
//     //                             orderby product.Name                      
//     //                             select product.ToProductDto() with {
//     //                                 files = (img == null) ? null : img.Select(p => p.Photo)
//     //                             };
//     //         return ListProductReturn;
//     //     }
//     //     public static IEnumerable<String> AddFileProductDto()
//     //     {
//     //         return null;

//     //     }
//         public static IEnumerable<ImgAndVideoProduct> FindImgProduct(
//             IEnumerable<ImgAndVideoProduct> FullFileProduct, Guid ProductId)
//         {
//             IEnumerable<ImgAndVideoProduct> FindFile = from a in FullFileProduct
//                             where a.ProductId == ProductId
//                             select (a != null) ? a : null;

//             return FindFile;
//         }
        
//     }
// }