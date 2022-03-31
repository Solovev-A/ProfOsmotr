import React from 'react';

const InputRadioWrapper = ({ name, legend, onChange, isInvalid, errorMessage, children }) => {
    const changeHandler = event => {
        const input = event.target;
        if (input.checked) {
            onChange(input.name, input.value);
        }
    }

    const invalidClassName = isInvalid ? 'is-invalid' : '';

    return (
        <fieldset className="form-group">
            <legend className="col-form-label pt-0">{legend}</legend>
            {
                React.Children.map(children, child => {
                    return React.cloneElement(child, { name, onChange: changeHandler, type: 'radio', inline: true })
                })
            }
            <input type="radio"
                style={{ display: "none" }}
                name={name}
                className={invalidClassName}
            />
            <div className="invalid-feedback">
                {errorMessage}
            </div>
        </fieldset>
    )
}

export default InputRadioWrapper;