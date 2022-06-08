import React from 'react'

import { BrowserRouter, Route, Routes } from 'react-router-dom'

import Home from '../pages/Home'
import Catalog from '../pages/Catalog'
import Cart from '../pages/Cart'
import Product from '../pages/Product'
import Layout from '../components/Layout'

const RoutesProject = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element = {<Layout />}>
                    <Route path='' element= {<Home />}/>
                    <Route path='catalog/:slug' element= {<Product />}/>
                    <Route path='catalog' element= {<Catalog/>}/>
                    <Route path='cart' element={<Cart />}/>

                    <Route path='*' element = {<Home />} />
                </Route>
            </Routes>
        </BrowserRouter>
    )
}

export default RoutesProject
