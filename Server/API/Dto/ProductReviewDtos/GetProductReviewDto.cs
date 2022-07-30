using System;
using System.Collections.Generic;

namespace API.Dto.ProductReviewDtos
{
    public class GetProductReviewDto
    {

        public GetProductReviewDto(Guid id, 
                                    Guid userId, 
                                    Guid productId, 
                                    string comment, 
                                    int numberOfStars, 
                                    DateTimeOffset dateTimeCreate, 
                                    List<string> photo)
        {
            Id = id;
            this.userId = userId;
            ProductId = productId;
            Comment = comment;
            this.numberOfStars = numberOfStars;
            this.dateTimeCreate = dateTimeCreate;
            Photo = photo;
        }

        public Guid Id {get; set;}
        public Guid userId {get; set;}
        public Guid ProductId {get; set;}
        public string Comment {get; set;}
        public int numberOfStars {get; set;}
        public DateTimeOffset dateTimeCreate {get; set;}
        public List<string> Photo {get; set;} 
        
    }
}