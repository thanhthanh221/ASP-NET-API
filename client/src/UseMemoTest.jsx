import logo from './logo.svg';
import './App.css';
import { useState } from 'react';
import { useMemo } from 'react';
import { useRef } from 'react';
import { useEffect } from 'react';

import axios, { Axios } from 'axios';

function UseMemoTest() {
  const [Name, SetName] = useState("");
  const [Price, SetPrice] = useState("");
  const [numberOffStar, SetnumberofStar] = useState("");

  const [Products, SetProducts] = useState([]);
  const nameRef = useRef();

  const handlerSubmmit = () => {
    SetProducts([...Products, {
      Name,
      Price: parseInt(Price)
    }])
    SetName("");
    SetPrice("");

    nameRef.current.focus();
  }
  useEffect(() => {
    axios({
      method: 'post',
      url: 'https://localhost:5001/Product',
      data: Products
    })

  })
  

  const total = useMemo(() => {
    const result = Products.reduce((Sum, Product) => Sum + Product.Price,0); 
    
    return result;
  },[Products]);
  // Khi Products thay đổi thì sẽ tính lại

  return (
    <div style={{padding: '10px 32px'}} className="App">
      <input value= {Name}
             ref= {nameRef}
             placeholder = "Enter Name...."
             onChange={e => SetName(e.target.value)} />
      <br />
      <input value={Price}
             placeholder = "Enter Price" 
             onChange={e => SetPrice(e.target.value)} />
      <br />
      <button onClick={handlerSubmmit}>Thêm</button>
      <br />

      Tổng giá : {total}
      <ul>{
          Products.map((p, index) => (
            <li key={index}> {p.Name} - {p.Price}</li>
          ))
        }  
      </ul>    
      
    </div>
  );
}
export default UseMemoTest;