import { useLayoutEffect } from "react";
import { useState } from "react";
import { useEffect } from "react";

function UseLayEf() {
    // Khi UseEf bị lỗi đến Ui người dùng  => UseLayoutEf
    const [count, SetCount] = useState(0);
    useLayoutEffect(() => { 
        if(count > 3) {
            SetCount(0);
        }
    },[count]);

    return (
        <div>
            <h1>{count}</h1>
            <button onClick={() => SetCount(count + 1)}>Ấn nút !</button>
        </div>
    )
}
export default UseLayEf;