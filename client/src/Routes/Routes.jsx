import React from 'react'

import { BrowserRouter, Route, Routes } from 'react-router-dom'

import Home from '../pages/Home'
import CatalogBackend from '../pages/CatalogBackend'
import ProductBackEnd from '../pages/ProductBackEnd'
import CartBackEnd from '../pages/CartBackEnd'
import LayoutLogin from '../components/Layout/LayoutLogin'
import Login from '../components/Log/Login'
import Register from '../components/Log/Register'
import Layout from '../components/Layout/Layout'

const RoutesProject = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Layout />}>
                    <Route path='' element={<Home />} />
                    <Route path='cart' element={<CartBackEnd />} />
                    <Route path='danhMucSanPham' element={<CatalogBackend />} />
                    <Route path='danhMucSanPham/:id' element={<ProductBackEnd />} />
                </Route>
                <Route path='/identity' element={<LayoutLogin />}>
                    <Route path='' element={<Login />} />
                    <Route path='dangNhap' element={<Login />} />
                    <Route path='dangKy/nguoiMua' element={<Register />} />
                </Route>
                <Route path='*' element={<Home />} />
            </Routes>
        </BrowserRouter>
    )
}

export default RoutesProject
