import React, { useEffect, useState } from 'react'
import { BsStar, BsStarFill } from "react-icons/bs"

const StartPart = (props) => {
  const [checkTrueStar, setCheckTrueStar] = useState([]);
  const [checkFalseStar , setCheckFalseStar] = useState([]);

  useEffect(() => {
    if(props.StarTrue) {
      setCheckTrueStar(new Array(props.StarTrue).fill(0));
      setCheckFalseStar(new Array(5-props.StarTrue).fill(0));
    }
  },[props.StarTrue])

  return (
    <div className="product-comments__boddy__top__startpart__div">
      {
        checkTrueStar.map(() => (
          <BsStarFill size={20} className="product-comments__boddy__top__startpart__div__value" />
        ))
      }
      {
        checkFalseStar.map(() => (
          <BsStar size={20} className="product-comments__boddy__top__startpart__div__value" />
        ))
      }
    </div>
  )
}

export default StartPart