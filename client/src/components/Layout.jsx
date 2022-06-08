import React from 'react'

import { Outlet } from 'react-router-dom'

import Header from './Header'
import Footer from './Footer'
import ProductViewModal from './ProductViewModal'
  
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
            <ProductViewModal />
        </div>
    )
}

export default Layout
