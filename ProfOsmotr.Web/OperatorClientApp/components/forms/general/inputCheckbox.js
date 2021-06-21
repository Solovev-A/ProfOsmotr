import React from 'react';
import CheckInput from './checkInput';

const InputCheckbox = (props) => {
    const changeHandler = event => {
        const input = event.target;
        props.onChange(input.name, input.checked);
    }

    return <CheckInput {...props}
        type='checkbox'
        onChange={changeHandler} />
}

export default InputCheckbox;