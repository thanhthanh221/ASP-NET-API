import React from 'react'
import PropTypes from 'prop-types'
import { useEffect } from 'react'

const CheckBoxBackend = (props) => {

    const inputRef = React.useRef(null)

    const onChange = () => {
        if (props.onChange) {
            props.onChange(inputRef.current);
        }
    }
    return (
        <label className="custom-checkbox">
            <input type="checkbox" ref={inputRef} id={props.id} onChange={onChange} checked= {props.checked}/>
            <span className="custom-checkbox__checkmark">
                <i className="bx bx-check"></i>
            </span>
            {props.name}
        </label>
    )
}

CheckBoxBackend.propTypes = {
    checked: PropTypes.bool
}

export default CheckBoxBackend
