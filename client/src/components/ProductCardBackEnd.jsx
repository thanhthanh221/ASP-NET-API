import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'

import { Link } from 'react-router-dom'

import { useDispatch } from 'react-redux'

import { set } from '../redux/product-modal/productModalSlice'

import Button from './Button'

import numberWithCommas from '../utils/numberWithCommas'

const ProductCardBackEnd = (props) => {
    const dispatch = useDispatch();
    return (
        <div className="product-card">
            <Link to={`/danhMucSanPham/${props.product.id}`}>
                <div className="product-card__image">
                    <img src='https://cf.shopee.vn/file/411e4eb8dd4e63329fc2fea0c96ac279'  alt="" />
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
                    animate={true}
                    onClick={() => dispatch(set(props.product))}
                >
                    chọn mua
                </Button>
            </div>
        </div>
    )
}

ProductCardBackEnd.propTypes = {
    name: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    id: PropTypes.string.isRequired,
}

export default ProductCardBackEnd

