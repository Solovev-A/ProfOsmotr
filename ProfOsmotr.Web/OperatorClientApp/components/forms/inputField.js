import React from 'react';
import classNames from 'classnames';

const InputField = ({ name, value, onChange, type, id, label, placeholder, isInvalid, errorMessage }) => {
    const changeHandler = (event) => {
        const input = event.target;
        onChange(input.name, input.value);
    }

    const inputClassname = classNames('form-control', { 'is-invalid': isInvalid });

    return (
        <div className="form-group">
            <label htmlFor={id}>{label}</label>
            <input type={type || 'text'}
                id={id}
                name={name}
                value={value}
                placeholder={placeholder || ''}
                onChange={changeHandler}
                className={inputClassname}
            />
            <div className="invalid-feedback">
                {errorMessage}
            </div>
        </div>
    )
}

export default InputField;