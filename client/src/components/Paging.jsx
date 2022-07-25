import React, { useEffect, useRef, useState } from 'react'
import { BiCaretLeft, BiCaretRight} from "react-icons/bi"

const Paging = (props) => {
    const [pagingNumber, SetPagingNumber] = useState([
      props.page ,
      props.page + 1,
      props.page + 2,
      props.page + 3
    ]);
    
    const refPaging = useRef(null);
    useEffect(() => {
      const childPaging = refPaging.current.childNodes;
      childPaging.forEach(e => {
        if(parseInt(e.innerHTML , 10)  === props.page){
          e.classList.add("paging__page__active");
        }
        else {
          e.classList.remove("paging__page__active");
        }
      });
    },[props.page]);
    const onClickHandler = (e) => {
      props.setPage(parseInt(e.target.innerHTML, 10))
      let ArrayHash = [
        props.page - 2,
        props.page - 1,
        props.page ,
        props.page + 1,
        props.page + 2,
      ];
      let ArrayPaging = [];
      ArrayHash.forEach((value) => {
        if(value >= 0) {
          ArrayPaging.push(value);
        }
      });
      SetPagingNumber(ArrayPaging);
    }
    const onClickBackUpHandler = (e) => {
      if(props.page > 0) {
        props.setPage(props.page - 1);
      }
    }
    const onClickNextHander = () => {
      props.setPage(props.page + 1);
    }
  return (
    <div className="paging">  
        <ul className="paging__page" ref= {refPaging}>
            <li className="paging__page__btn paging__page__backup" ><BiCaretLeft onClick={onClickBackUpHandler}/></li>
            {
              pagingNumber.map(p => (
                <li className="paging__page__numbers" onClick={onClickHandler}>{p}</li>
              ))
            }
            <li className="paging__page__dots">...</li>
            <li className="paging__page__btn paging__page__next" onClick={onClickNextHander}><BiCaretRight /></li>
        </ul>
    </div>
  )
}

export default Paging