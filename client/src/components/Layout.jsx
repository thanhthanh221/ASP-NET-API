import React from 'react'

import { Outlet } from 'react-router-dom'

import Header from './Header'
import Footer from './Footer'
import ProductViewModal from './ProductViewModal'
import ProductViewModalBackEnd from './ProductViewModalBackEnd'
import LoginBackEnd from '../pages/LoginBackEnd'
  
const Layout = () => {
    return (
        <div>
            <Header />
            <div className='cotainer'>
                <div className='main'>
                    <Outlet />
                </div>
            </div>  
            <Footer />
            <ProductViewModalBackEnd />
        </div>
    )
}

export default Layout
