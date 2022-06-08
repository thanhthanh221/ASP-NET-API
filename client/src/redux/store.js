import { configureStore } from '@reduxjs/toolkit'

import productModalReducer from './product-modal/productModalSlice'

import cartItemsReducer from './shopping-cart/cartItemsSlide'

// Khởi tạo kho lưu trữ
export const store = configureStore({
    reducer: {
        productModal: productModalReducer,
        cartItems: cartItemsReducer
    },
})