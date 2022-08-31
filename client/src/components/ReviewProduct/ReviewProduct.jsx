import React from 'react'
import CommentsByUser from './CommentsByUser'
import ReviewProductCheckStar from './ReviewProductCheckStar'
import Paging from '../Customs/Paging'
import StartPart from './StartPart'

const ReviewProduct = (props) => {
  const StarFill = new Array(5).fill(0);
  return (
    <div className="product-comments">
      <div className="product-comments__headding">
        <h1 className='product-comments__headding__title'>
          ĐÁNH GIÁ SẢN PHẨM
        </h1>
      </div>
      <div className="product-comments__boddy">

        <div className='product-comments__boddy__top'>
          <ReviewProductCheckStar
            numberOfStars = {props.product.numberOfStars}
          />
          
          <div className="product-comments__boddy__top__ratingreview">
            <div className="product-comments__boddy__top__ratingreview__value"></div>
            <div className="product-comments__boddy__top__ratingreview__value"></div>
            <div className="product-comments__boddy__top__ratingreview__value"></div>
            <div className="product-comments__boddy__top__ratingreview__value"></div>
            <div className="product-comments__boddy__top__ratingreview__value"></div>
          </div>

          <div className='product-comments__boddy__top__startpart'>
            {
              StarFill.map((item, index) => (
                <StartPart StarTrue = {5-index} />   
              ))
            }
          </div>
        </div>
        <p>Bình luận của người mua</p>
        <hr></hr>
        <div className='product-comments__boddy__bottom'>
          {
            props.CommentsInPage ? props.CommentsInPage.map((item,index) => (
              <CommentsByUser key={index} commentsOfProduct = {item} />    
            ))
            : ''
          }
        </div>
      </div>
      <div>
        <Paging 
          page={props.page}
          setPage = {(input) => props.setPage(input)}
          />
      </div>
    </div>
  )
}

export default ReviewProduct
