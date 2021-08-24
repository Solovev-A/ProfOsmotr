import React from 'react';
import classNames from 'classnames';

const DropdownSelect = ({ label, onChange, isInvalid, errorMessage, children, ...selectProps }) => {
    const changeHandler = (event) => {
        const select = event.target;
        onChange(select.name, select.value);
    }

    const selectClassname = classNames('form-control', { 'is-invalid': isInvalid });

    return (
        <div className="form-group">
            <label htmlFor={selectProps.id}>{label}</label>
            <select {...selectProps}
                onChange={changeHandler}
                className={selectClassname}
            >
                {children}
            </select>
            <div className="invalid-feedback">
                {errorMessage}
            </div>
        </div>
    )
}

export default DropdownSelect;