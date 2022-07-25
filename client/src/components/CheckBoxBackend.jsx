import React from 'react'
import PropTypes from 'prop-types'

const CheckBoxBackend = (props) => {

    const inputRef = React.useRef(null)

    const onChange = () => {
        if (props.onChange) {
            props.onChange(inputRef.current);
            console.log(inputRef.current.checked);
        }
    }

    return (
        <label className="custom-checkbox">
            <input type="checkbox" ref={inputRef} onChange={onChange} checked={props.checked}/>
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
