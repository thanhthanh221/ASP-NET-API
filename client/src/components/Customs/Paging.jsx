import React, { useEffect, useRef, useState } from 'react'
import { BiCaretLeft, BiCaretRight } from "react-icons/bi"
import PropTypes from 'prop-types'

const Paging = (props) => {
  const [pagingNumber, SetPagingNumber] = useState([
    props.page,
    props.page + 1,
    props.page + 2,
    props.page + 3,
  ]);

  const refPaging = useRef(null);
  useEffect(() => {
    const childPaging = refPaging.current.childNodes;
    childPaging.forEach(e => {
      if (parseInt(e.innerHTML, 10) === props.page) {
        e.classList.add("paging__page__active");
      }
      else {
        e.classList.remove("paging__page__active");
      }
    });
  }, [props.page]);
  const onClickHandler = (e) => {
    const pageNumber = parseInt(e.target.innerHTML, 10)
    props.setPage(pageNumber)
    let ArrayHash = [
      pageNumber - 1,
      pageNumber,
      pageNumber + 1,
      pageNumber + 2
    ];
    let ArrayPaging = [];
    ArrayHash.forEach((value) => {
      if (value >= 0) {
        ArrayPaging.push(value);
      }
    });
    SetPagingNumber(ArrayPaging);
  }
  const onClickBackUpHandler = (e) => {
    if (props.page > 0) {
      props.setPage(props.page - 1);
      let ArrayPaging = [
        props.page - 1,
        props.page,
        props.page + 1,
        props.page + 2,
      ];
      SetPagingNumber(ArrayPaging);
    }
  }
  const onClickNextHander = () => {
    props.setPage(props.page + 1);
    let ArrayPaging = [
      props.page + 1,
      props.page + 2,
      props.page + 3,
      props.page + 4,
    ];
    SetPagingNumber(ArrayPaging);
  }
  return (
    <div className="paging">
      <ul className="paging__page" ref={refPaging}>
        <li className="paging__page__btn paging__page__backup" ><BiCaretLeft onClick={onClickBackUpHandler} /></li>
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
Paging.protoType = {
  page: PropTypes.number.isRequired
}

export default Paging