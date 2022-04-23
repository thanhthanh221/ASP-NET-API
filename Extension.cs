using BackEnd.Dto;
using BackEnd.Entities;

namespace BackEnd {
    // Các trường hợp xử lý biến đổi
    public static class Extensions{
        public static ItemDto AsDto(this Item item){
            return new ItemDto{
                Id = item.Id,
                Name = item.Name,
                Price =  item.Price,
                CreateDate = item.CreateDate
            }; 
        }
        public static GetProductDto AsDtoGetProduct(this Product product)
        {
            return new GetProductDto 
            {
                Name = product.Name,
                Price = product.Price,
                Describe = product.Describe
            };
        }
        
    }

}