import React from 'react'

import { BrowserRouter, Route, Routes } from 'react-router-dom'

import Home from '../pages/Home'
import Catalog from '../pages/Catalog'
import Cart from '../pages/Cart'
import Product from '../pages/Product'
import Layout from '../components/Layout'
import CatalogBackend from '../pages/CatalogBackend'
import ProductBackEnd from '../pages/ProductBackEnd'
import CartBackEnd from '../pages/CartBackEnd'
import LoginBackEnd from '../pages/LoginBackEnd'

const RoutesProject = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element = {<Layout />}>
                    <Route path='' element= {<Home />}/>
                    <Route path='catalog/:slug' element= {<Product />}/>
                    <Route path='catalog' element= {<Catalog />}/>
                    <Route path= 'catalog/?slug' element= {<Product/>} />
                    <Route path= 'cart' element={<CartBackEnd />}/>
                    <Route path= 'danhMucSanPham' element= {<CatalogBackend/>}/>
                    <Route path = 'danhMucSanPham/:id' element = {<ProductBackEnd/>} />
                </Route>
                <Route path='dangNhap' element={<LoginBackEnd />} />
            </Routes>
        </BrowserRouter>
    )
}

export default RoutesProject
