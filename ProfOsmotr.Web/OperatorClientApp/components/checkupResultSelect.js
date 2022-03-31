import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../hooks/useStore';
import DropdownSelect from './forms/general/dropdownSelect';


const CheckupResultSelect = ({ formStore }) => {
    const { checkupResultsStore } = useStore();

    useEffect(() => {
        checkupResultsStore.loadCheckupResults();
    }, [])

    return (
        <DropdownSelect
            label='Результат медицинского осмотра'
            name='checkupResultId'
            id='checkupResultId'
            value={formStore.model.checkupResultId}
            onChange={formStore.updateProperty}
            isInvalid={formStore.errors['checkupResultId']}
            errorMessage={formStore.errors['checkupResultId']}
        >
            <option value='empty'></option>
            {
                checkupResultsStore.checkupResults.map(result => {
                    return (
                        <option value={result.id}
                            key={result.id}
                        >
                            {result.text}
                        </option>
                    )
                })
            }
        </DropdownSelect>
    );
}

export default observer(CheckupResultSelect);