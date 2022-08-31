import React, { useState, useEffect, useMemo } from 'react'
import PropTypes from 'prop-types'

import { useDispatch } from 'react-redux'

import { addItem } from '../../redux/shopping-cart/cartItemsSlide'
import { remove } from '../../redux/product-modal/productModalSlice'

import { GrFormAdd } from "react-icons/gr";
import { FiMinus } from "react-icons/fi";

import Button from '../Customs/Button'
import numberWithCommas from '../../utils/numberWithCommas'
import { useNavigate } from 'react-router-dom'

const ProductViewBackEnd = (props) => {
    const navigate = useNavigate();

    const dispatch = useDispatch();

    const [descriptionExpand, setDescriptionExpand] = useState(false)
    console.log(props.product.imgAndVideoProducts);

    //Chưa gán giá trị cho nó
    const [color, setColor] = useState(undefined)

    const [size, setSize] = useState(undefined)

    const [quantity, setQuantity] = useState(1)

    const updateQuantity = (type) => {
        if (type === 'plus') {
            setQuantity(quantity + 1)
        }
        else {
            setQuantity(quantity - 1 < 1 ? 1 : quantity - 1)
        }
    }

    // // Gọi khi mà product thay đổi
    // useEffect(() => {
    //     setPreviewImg(product.image01)
    //     setQuantity(1)
    //     setColor(undefined)
    //     setSize(undefined)
    // }, [product])

    // // Nếu chưa chọn

    const addToCart = () => {
        let newItem = {
            product: props.product,
            quantity: quantity
        }
        if (dispatch(addItem(newItem))) {
            alert('Success')
        } else {
            alert('Fail')
        }
    }

    const goToCart = () => {
        let newItem = {
            product: props.product,
            quantity: quantity
        }
        if (dispatch(addItem(newItem))) {
            alert('Success');
            navigate('/cart')
        } else {
            alert('Fail')
        }
    }

    return (
        <div className="product">
            <div className="product__images">
                <div className="product__images__list">
                    <div className="product__images__list__item">
                        <img src={'data:image/jpeg;base64,' + props.product.imgAndVideoProducts} />
                    </div>
                </div>
                {/* <div className="product__images__main">
                    <img src={previewImg} alt="" />
                </div> */}
                <div className={`product-description ${descriptionExpand ? 'expand' : ''}`}>
                    <div className="product-description__title">
                        Chi tiết sản phẩm
                    </div>
                    <div className="product-description__content" dangerouslySetInnerHTML={{ __html: props.product.describe }}></div>
                    <div className="product-description__toggle">
                        <Button size="sm" onClick={() => setDescriptionExpand(!descriptionExpand)}>
                            {
                                descriptionExpand ? 'Thu gọn' : 'Xem thêm'
                            }
                        </Button>
                    </div>
                </div>
            </div>
            <div className="product__info">
                <h1 className="product__info__title">{props.product.name}</h1>
                <div className="product__info__item">
                    <span className="product__info__item__price">
                        {props.product.price}
                    </span>
                </div>
                <div className="product__info__item">
                    {/* <div className="product__info__item__title">
                        Màu sắc
                    </div> */}
                    {/* <div className="product__info__item__list">
                        {
                            product.colors.map((item, index) => (
                                <div key={index} className={`product__info__item__list__item ${color === item ? 'active' : ''}`} onClick={() => setColor(item)}>
                                    <div className={`circle bg-${item}`}></div>
                                </div>
                            ))
                        } */}
                    {/* </div> */}
                </div>
                <div className="product__info__item">
                    {/* <div className="product__info__item__title">
                        Kích cỡ
                    </div>   */}
                    {/* <div className="product__info__item__list">
                        {
                            product.size.map((item, index) => (
                                <div key={index} className={`product__info__item__list__item ${size === item ? 'active' : ''}`} onClick={() => setSize(item)}>
                                    <span className="product__info__item__list__item__size">
                                        {item}
                                    </span>
                                </div>
                            ))
                        }
                    </div> */}
                </div>
                <div className="product__info__item">
                    <div className="product__info__item__title">
                        Số lượng
                    </div>
                    <div className="product__info__item__quantity">
                        <div className="product__info__item__quantity__btn" onClick={() => updateQuantity('minus')}>
                            <FiMinus />
                        </div>
                        <div className="product__info__item__quantity__input">
                            {quantity}
                        </div>
                        <div className="product__info__item__quantity__btn" onClick={() => updateQuantity('plus')}>
                            <GrFormAdd />
                        </div>
                    </div>
                </div>
                <div className="product__info__item">
                    <Button onClick={() => addToCart()}>thêm vào giỏ</Button>
                    <Button onClick={() => goToCart()}>mua ngay</Button>
                </div>
            </div>
            <div className={`product-description mobile ${descriptionExpand ? 'expand' : ''}`}>
                <div className="product-description__title">
                    Chi tiết sản phẩm
                </div>
                {/* <div className="product-description__content" dangerouslySetInnerHTML={{__html: props.product.describe}}></div> */}
                <div className="product-description__toggle">
                    <Button size="sm" onClick={() => setDescriptionExpand(!descriptionExpand)}>
                        {
                            descriptionExpand ? 'Thu gọn' : 'Xem thêm'
                        }
                    </Button>
                </div>
            </div>
        </div>
    )
}

ProductViewBackEnd.propTypes = {
    product: PropTypes.object
}

export default ProductViewBackEnd
