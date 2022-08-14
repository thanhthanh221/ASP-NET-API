import React, { useEffect, useState } from 'react'
import { BsStarHalf, BsStar, BsStarFill } from "react-icons/bs";

const CommentsByUser = (props) => {
  const [date, setDate] = useState(new Date(props.commentsOfProduct.dateTimeCreate));
  const [starTrue, setStarTrue] = useState((new Array(props.commentsOfProduct.numberOfStars)).fill(0));
  const [starFalse, setStarFalse] = useState((new Array(5-props.commentsOfProduct.numberOfStars)).fill(0));
  console.log(starTrue);

  return (
    <div className="product-comments__boddy__bottom__comment">
        <div className="product-comments__boddy__bottom__comment__infomations">
            <div className="product-comments__boddy__bottom__comment__infomations__image">
              <img src="https://www.pngkey.com/png/full/114-1149878_setting-user-avatar-in-specific-size-without-breaking.png" />
            </div>
            <div className="product-comments__boddy__bottom__comment__infomations__value">
              <h4>{date.getUTCDay()}-{date.getMonth()+ 1}-{date.getFullYear()} {date.getHours()}:{date.getMinutes()} </h4>
              <p>{props.commentsOfProduct.userName}</p>
            </div>
        </div>
        <div className="product-comments__boddy__bottom__comment__star">
         <div className="product-comments__boddy__bottom__comment__star__value">
          {
            starTrue.map(() => (
              <BsStarFill size={15} className="product-comments__boddy__bottom__comment__star__value__icon" />
            ))
          }
          {
            starFalse.map(() => (
              <BsStar size={15} className="product-comments__boddy__bottom__comment__star__value__icon" />
            ))
          }

         </div>

          <p>{props.commentsOfProduct.comment}.</p>
        </div>
    </div>
  )
}

export default CommentsByUser
