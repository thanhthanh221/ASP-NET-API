import React, { useState, useRef, useEffect } from 'react'
import PropTypes from 'prop-types'

import { useDispatch } from 'react-redux'
import { updateItem, removeItem } from '../../redux/shopping-cart/cartItemsSlide'

import { GrFormAdd } from "react-icons/gr";
import { FiMinus } from "react-icons/fi";
import { RiDeleteBin5Line } from 'react-icons/ri'

import numberWithCommas from '../../utils/numberWithCommas'
import { Link } from 'react-router-dom'

const CartItemBackEnd = (props) => {

    const dispatch = useDispatch()

    const itemRef = useRef(null)

    const [item, setItem] = useState(props.item)
    const [quantity, setQuantity] = useState(props.item.quantity)

    useEffect(() => {
        setItem(props.item)
        setQuantity(props.item.quantity)
    }, [props.item])

    console.log(props.item);
    const updateQuantity = (opt) => {
        if (opt === '+') {
            dispatch(updateItem({ ...item, quantity: quantity + 1 }))
        }
        if (opt === '-') {
            dispatch(updateItem({ ...item, quantity: quantity - 1 === 0 ? 1 : quantity - 1 }))
        }
    }

    // const updateCartItem = () => {
    //     dispatch(updateItem({...item, quantity: quantity}))
    // }

    const removeCartItem = () => {
        console.log('removeCartItem')
        dispatch(removeItem(item))
    }

    return (
        <div className="cart__item" ref={itemRef}>
            <div className="cart__item__image">
                <img src={'data:image/jpeg;base64,' + item.product.imgAndVideoProducts} />
            </div>
            <div className="cart__item__info">
                <div className="cart__item__info__name">
                    <Link to={`/danhMucSanPham/${item.product.id}`}>
                        {`${item.product.name}`}
                    </Link>
                </div>
                <div className="cart__item__info__price">
                    {numberWithCommas(item.product.price)}
                </div>
                <div className="cart__item__info__quantity">
                    <div className="product__info__item__quantity">
                        <div className="product__info__item__quantity__btn" onClick={() => updateQuantity('-')}>
                            <FiMinus />
                        </div>
                        <div className="product__info__item__quantity__input">
                            {quantity}
                        </div>
                        <div className="product__info__item__quantity__btn" onClick={() => updateQuantity('+')}>
                            <GrFormAdd />
                        </div>
                    </div>
                </div>
                <div className="cart__item__del">
                    <i onClick={() => removeCartItem()}>
                        <RiDeleteBin5Line className='cart__item__del__value' size={35} />
                    </i>
                </div>
            </div>
        </div>
    )
}

CartItemBackEnd.propTypes = {
    item: PropTypes.object
}

export default CartItemBackEnd
