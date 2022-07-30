import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'

import { Link } from 'react-router-dom'

import { useDispatch } from 'react-redux'

import { set } from '../redux/product-modal/productModalSlice'

import Button from './Button'

import numberWithCommas from '../utils/numberWithCommas'
import image1 from '../assets/images/ImgProduct/Ảnh-chụp-màn-hình-2022-06-17-221530.jpg'

const ProductCardBackEnd = (props) => {
    const dispatch = useDispatch();
    console.log(require('../assets/images/ImgProduct/Ảnh-chụp-màn-hình-2022-06-17-221530.jpg').default);
    console.log(props.img)
    return (
        <div className="product-card">
            <Link to={`/danhMucSanPham/${props.id}`}>
                <div className="product-card__image">
                    <img src={"/static/media/"+ props.img}  alt="" />
                    <img src={props.img02} alt="" />
                </div>
                <h3 className="product-card__name">{props.name}</h3>
                <div className="product-card__price">
                    {numberWithCommas(props.price)}
                    <span className="product-card__price__old">
                        <del>{numberWithCommas(399000)}</del>
                    </span>
                </div>
            </Link>
            <div className="product-card__btn">
                <Button
                    size="sm"    
                    icon="bx bx-cart"
                    animate={true}
                    onClick={() => dispatch(set(props.slug))}
                >
                    chọn mua
                </Button>
            </div>
        </div>
    )
}

ProductCardBackEnd.propTypes = {
    img01: PropTypes.string.isRequired ,
    img02: PropTypes.string.isRequired ,
    name: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    slug: PropTypes.string.isRequired,
}

export default ProductCardBackEnd
// const {data: res} = await axios.get(`${url}photo-2.jpg`,
// {  headers: { Authorization: `${tokenApp}` },responseType: 'json',});
// return res;};
