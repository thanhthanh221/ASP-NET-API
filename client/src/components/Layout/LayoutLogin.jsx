import React from 'react'
import img from '../../assets/images/JB-FE-Shop_10.png'
import { Link, Outlet } from 'react-router-dom'
import Footer from './Footer'

const LayoutLogin = () => {
    return (
        <div className="Log">
            <div className='Log__header'>
                <Link to={'/'}>
                    <img src={img} className="Log__header__image" />
                </Link>
                <div className="Log__header__text">Chào bạn đến với hệ thống ...</div>
            </div>
            <div className='Log__container'>
                <div className='Log__container__image'>
                    <img src={img} alt='' />
                </div>
                <Outlet />
            </div>
            <Footer />
        </div>
    )
}

export default LayoutLogin