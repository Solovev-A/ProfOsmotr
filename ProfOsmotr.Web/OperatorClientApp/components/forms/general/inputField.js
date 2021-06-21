import React from 'react';
import classNames from 'classnames';

const InputField = ({ label, onChange, isInvalid, errorMessage, ...inputProps }) => {
    const changeHandler = (event) => {
        const input = event.target;
        onChange(input.name, input.value);
    }

    const inputClassname = classNames('form-control', { 'is-invalid': isInvalid });

    return (
        <div className="form-group">
            <label htmlFor={inputProps.id}>{label}</label>
            <input {...inputProps}
                type={inputProps.type || 'text'}
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