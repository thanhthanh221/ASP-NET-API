import React from 'react'

import { Outlet } from 'react-router-dom'

import Header from './Header'
import Footer from './Footer'
import ProductViewModalBackEnd from '../Product/ProductViewModalBackEnd'


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
