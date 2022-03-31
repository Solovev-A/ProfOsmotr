import React from 'react';
import { observer } from 'mobx-react-lite';

import InputCheckbox from './../general/inputCheckbox';

const InputCheckboxWithObserver = ({ label, name, formStore, ...inputProps }) => {
    return (
        <InputCheckbox {...inputProps}
            label={label}
            name={name}
            id={name}
            checked={formStore.model[name]}
            onChange={formStore.updateProperty}
        />
    )
}

export default observer(InputCheckboxWithObserver);