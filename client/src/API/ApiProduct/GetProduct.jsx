import { useState } from 'react';
import axios from 'axios';
import { useEffect } from 'react';

/*
axios({
    method: 'get',
    url: 'https://localhost:5001/Product',
    data: {product}
});
*/
// Cơ bản của Axios
function GetProduct() {
    const [products, setProduct] = useState([]);
    useEffect(() => {
        axios.get('https://localhost:5001/Product').then(res => {
            setProduct(res.data);
        })
        .catch(error => console.log(error));
    },[products]);


    return (
        <div>
            <h1>Thông tin các sản phẩm</h1>
            <ul>
                {
                    products.map((product, index) => (
                        <li key={index}>{product.name} - {product.price}</li>
                    ))
                }
            </ul>
        </div>
    )
}
export default GetProduct