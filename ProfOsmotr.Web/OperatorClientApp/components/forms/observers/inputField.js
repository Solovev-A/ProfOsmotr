import React from 'react';
import InputField from './../general/inputField';
import { observer } from 'mobx-react-lite';

const InputFieldWithObserver = ({ label, name, formStore, ...inputProps }) => {
    return <InputField {...inputProps}
        label={label}
        name={name}
        id={name}
        value={formStore.model[name]}
        errorMessage={formStore.errors[name] || ''}
        isInvalid={formStore.errors[name] || formStore.errors[name] === ''}
        onChange={formStore.updateProperty}
    />
}

export default observer(InputFieldWithObserver);