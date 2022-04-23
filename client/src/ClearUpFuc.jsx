import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';
import { useMemo } from 'react';
import { useRef } from 'react';

function ClearUpFuc() {
    const [count, setcount] = useState(1);

    useEffect(() => {
        console.log("Liên kết lại");
        // Trước khi tái tạo lại useEf thì sẽ dọn dẹp cái trước
        return () => {
            console.log("Dọn dẹp");
        }
    },[count]);
    return (
        <div className="App">
            <h1>{count}</h1>
            <button onClick={() => setcount(count + 1)}>Ấn vào đây !</button>
      
        </div>
    );
}
export default ClearUpFuc;