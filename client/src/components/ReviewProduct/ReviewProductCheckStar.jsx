import React, { useRef, useState } from 'react'
import { useEffect } from 'react';
import { BsStarHalf, BsStar, BsStarFill } from "react-icons/bs";

const ReviewProductCheckStar = (props) => {
  const [checkTrueStar, setCheckTrueStar] = useState([]);
  const [checkFalseStar , setCheckFalseStar] = useState([]);
  const [halfStar, setHalfStar] = useState(false);
  useEffect(() => {
    if(props.numberOfStars) {
      setCheckTrueStar(new Array(Math.floor((props.numberOfStars))).fill(0));
      setCheckFalseStar(new Array(5-Math.round((props.numberOfStars))).fill(0));
      if(Math.floor((props.numberOfStars)) !== props.numberOfStars) {
        setHalfStar(true);
      }
    }
    else if(!props.numberOfStars) {
      setCheckFalseStar(new Array(5).fill(0));
    }
  },[props.numberOfStars])
  return (
    <div className="product-comments__boddy__top__checkStar">
        <h2>{props.numberOfStars}</h2>
        <div className='className="product-comments__boddy__top__checkStar__div'>
          {
            checkTrueStar.map(() => (
              <BsStarFill size={35} className="product-comments__boddy__top__checkStar__div__icon" />         
            ))
          }

          {
            halfStar ? <BsStarHalf size={35} className="product-comments__boddy__top__checkStar__div__icon" /> : '' 
          }

          {
            checkFalseStar.map(() => (
              <BsStar size={35} className="product-comments__boddy__top__checkStar__div__icon" />
            )) 
          }
        </div>

        <p>Sản Phẩm trung bình</p>
    </div>
  )
}

export default ReviewProductCheckStar
