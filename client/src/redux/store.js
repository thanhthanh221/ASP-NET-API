import { configureStore } from '@reduxjs/toolkit'
import loginProjectReducer from './Login-Project/loginProject';

import productModalReducer from './product-modal/productModalSlice'

import cartItemsReducer from './shopping-cart/cartItemsSlide'

// Khởi tạo kho lưu trữ
export const store = configureStore({
    reducer: {
        productModal: productModalReducer,
        cartItems: cartItemsReducer,
        LoginProject: loginProjectReducer
    },
});