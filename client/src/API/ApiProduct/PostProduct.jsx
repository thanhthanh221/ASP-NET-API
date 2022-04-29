import axios from "axios";
import { useState } from "react";
import { useEffect } from "react";

function PostProduct() {

    const [name, setName] = useState("");
    const [price, setPrice] = useState(0);
    const [describe, setDescribe] = useState("");
    const [numberOfStars, setNumberOfStars] = useState(0);

    const [product,setProduct] = useState({});

    const SubmitProduct = () => {
        setProduct({
            name: name,
            price: parseFloat(price),
            describe: describe,
            numberOfStars: parseFloat(numberOfStars)
        });

        axios.post("https://localhost:5001/Product",{product}).then(res => {
            console.log(res.data);
        }).catch(error => console.log(error));
        // Chỉnh hết tất cả về mặc định
        setName("");
        setPrice(0);
        setNumberOfStars(0);
        setDescribe("");
        setProduct({});
    }

    return (
        <div>
            <div>
                <label >Name : </label>
                <input type="text" value={name} onChange={e => setName(e.target.value)}/>
                <br />
                <label >Price :</label>
                <input type="text" value={price} onChange={e => setPrice(e.target.value)}/>
                <br />
                <label >Describe : </label>
                <input type="text" value={describe} onChange={e => setDescribe(e.target.value)}/>
                <br />
                <label >Number of Stars : </label>
                <input type="text" value={numberOfStars} onChange={e => setNumberOfStars(e.target.value)}/>
                <br />
                <input type="file"  />
                <br />
                <button onClick={SubmitProduct}>Thêm Sản Phẩm</button>



            </div>
        </div>
    )
}
export default PostProduct;


