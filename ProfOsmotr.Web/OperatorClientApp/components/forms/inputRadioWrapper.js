import React from 'react';

const InputRadioWrapper = ({ name, onChange, children }) => {
    const changeHandler = event => {
        const input = event.target;
        if (input.checked) {
            onChange(input.name, input.value);
        }
    }

    return (
        React.Children.map(children, child => {
            return React.cloneElement(child, { name, onChange: changeHandler, type: 'radio', inline: true })
        })
    )
}

export default InputRadioWrapper;