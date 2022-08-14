import { createSlice } from '@reduxjs/toolkit'

const items = localStorage.getItem('cartItems') !== null ? JSON.parse(localStorage.getItem('cartItems')) : []

// trạng thái ban đầu
const initialState = {
    value: items,
}

export const cartItemsSlice = createSlice({
    name: 'cartItems',
    initialState,
    reducers: {
        addItem: (state, action) => {
            const newItem = action.payload
            const duplicate = state.value.filter(e => e.product.id === newItem.product.id)
            if (duplicate.length > 0) {
                state.value = state.value.filter(e => e.product.id !== newItem.product.id)
                state.value = [...state.value, {
                    product : newItem.product,
                    quantity: newItem.quantity + duplicate[0].quantity
                }]
            } 
            else {
                state.value = [...state.value, action.payload]
            }
            localStorage.setItem('cartItems', JSON.stringify(state.value.sort((a, b) => a.product.id > b.product.id ? 1 : (a.product.id < b.product.id ? -1 : 0))))
        },
        updateItem: (state, action) => {
            const newItem = action.payload
            const item = state.value.filter(e => e.product.id === newItem.product.id)
            if (item.length > 0) {
                state.value = state.value.filter(e => e.product.id !== newItem.product.id)
                state.value = [...state.value, {
                    product : newItem.product,
                    quantity: newItem.quantity
                }]
            }
            localStorage.setItem('cartItems', JSON.stringify(state.value.sort((a, b) => a.id > b.id ? 1 : (a.id < b.id ? -1 : 0))))
        },
        removeItem: (state, action) => {
            const item = action.payload
            state.value = state.value.filter(e => e.product.id !== item.product.id)
            localStorage.setItem('cartItems', JSON.stringify(state.value.sort((a, b) => a.id > b.id ? 1 : (a.id < b.id ? -1 : 0))))
        },
    },
})

// Action creators are generated for each case reducer function
export const { addItem, removeItem, updateItem } = cartItemsSlice.actions

export default cartItemsSlice.reducer