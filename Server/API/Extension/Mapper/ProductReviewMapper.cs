using API.Dto.ProductReviewDtos;
using Domain_Layer.Entities.Product;

namespace API.Extension.Mapper
{
    public static class ProductReviewMapper
    {
        public static GetProductReviewDto ConverToDto(this ProductReviews productReview)
        {
            return new GetProductReviewDto(
                            productReview.Id,
                            productReview.userId, 
                            productReview.ProductId, 
                            productReview.Comment, 
                            productReview.numberOfStars, 
                            productReview.dateTimeCreate, 
                            productReview.Photo);
        }      
    }
}