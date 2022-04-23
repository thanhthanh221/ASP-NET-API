import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';
import { useRef } from 'react';

function UseRefTest() {
    const [count, setcount] = useState(60);

    let timerId = useRef() // Luôn trả về obj - biến timeId sẽ coi như bị đưa ra ngoài

    let PrevCount = useRef();
    let h1ref = useRef();

    useEffect(() => {
        PrevCount.current = count;
    },[count]);
    useEffect(() => {
        const rect = h1ref.current.getBoundingClientRect();

        console.log(rect);
    })

    const handlerStart = () => {
        timerId.current = setInterval(() => {
            setcount(prev => prev - 1);         
        }, 1000);

    }
    const handlerStop = () => {
        clearInterval(timerId.current);

    }
    console.log(count, PrevCount.current);
    return (
        <div style={{padding: 20}}>
            <h1 ref={h1ref}>{count}</h1>
            <button onClick={handlerStart}>Start</button>
            <button onClick={handlerStop}>Stop</button>

        </div>
    )
}
export default UseRefTest