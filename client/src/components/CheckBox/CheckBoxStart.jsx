import React, { useEffect, useRef, useState } from 'react'
import PropTypes from 'prop-types'
import {AiOutlineStar, AiFillStar} from 'react-icons/ai'

const CheckBoxStart = (props) => {
    
    const [checkTrueStar, setCheckTrueStar] = useState((new Array(props.numberStar)).fill(0));
    const [checkFalse , setCheckFalseStar] = useState((new Array(5-props.numberStar).fill(0)));
    
    const aRef = React.useRef(null);
    const falseBackGroundRef = useRef(null);

    
    useEffect(() => {
        if(checkTrueStar.length === 5) {
            aRef.current.innerHTML = '';
        }
    },[]);
    useEffect (() => {
        if(checkTrueStar.length !== props.filerByStar) {
            falseBackGroundRef.current.classList.remove('checkBoxStart__CheckTrue');
        }
    },[props.filerByStar])


    return (
        <div className='checkBoxStart' >
            <ul className='checkBoxStart__Contener' onClick= {props.onClick} ref={falseBackGroundRef}>
                {
                    checkTrueStar.map(() => (
                        <li className='checkBoxStart__Contener__Value checkBoxStart__Contener__Value__True '><AiFillStar /></li>
                    ))
                }
                {
                    checkFalse.map(() => (
                        <li className='checkBoxStart__Contener__Value checkBoxStart__Contener__Value__False'><AiOutlineStar /></li>
                    ))
                }
                {
                    <a ref={aRef} className='checkBoxStart__Contener__Less'>Trở lên</a>
                }
            </ul>
        </div>
    )
}

CheckBoxStart.propTypes = {
    checked: PropTypes.bool ,
}

export default CheckBoxStart
