import logo from './logo.svg';
import './App.css';
import { useState } from 'react';
import { useMemo } from 'react';
import { useRef } from 'react';
import { useEffect } from 'react';

function ReadProduct() {
    const [Products, SetProducts] = useState([]);
    
    useEffect(() => {
        fetch("https://localhost:5001/Product").then(p => p.json()).then(p => SetProducts(p));
    },[]);

    return (
        <div>
            <ul>
                {
                    Products.map((product) => (
                        <li>{product["name"]} - {product["price"]}</li>
                    ))
                }
            </ul>
        </div>

    )
}
export default ReadProduct;