import React from 'react';
import classNames from 'classnames';

const CheckInput = (props) => {
    const { inline, ...inputProps } = props;

    const className = classNames('form-check', { 'form-check-inline': inline });

    return (
        <div className={className}>
            <input {...inputProps} className='form-check-input' />
            <label className='form-check-label' htmlFor={props.id}>
                {props.label}
            </label>
        </div>
    )
}

export default CheckInput;