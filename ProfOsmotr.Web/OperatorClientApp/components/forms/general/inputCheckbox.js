import React from 'react';
import CheckInput from './checkInput';

const InputCheckbox = (props) => {
    const changeHandler = event => {
        const input = event.target;
        props.onChange(input.name, input.checked);
    }

    return (
        <div className="form-group">
            <CheckInput {...props}
                type='checkbox'
                onChange={changeHandler} />
        </div>
    )
}

export default InputCheckbox;