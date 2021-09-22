import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../hooks/useStore';
import DropdownSelect from './forms/general/dropdownSelect';
import InputField from './forms/observers/inputField';


const BaseMedicalReportEditorForm = ({ formStore }) => {
    const { checkupResultsStore } = useStore();

    useEffect(() => {
        checkupResultsStore.loadCheckupResults();
    }, [])

    return (
        <>
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
            <InputField label='Медицинское заключение'
                name='medicalReport'
                formStore={formStore}
            />
            <div className='row'>
                <div className='col'>
                    <InputField label='Дата завершения'
                        name='dateOfComplition'
                        formStore={formStore}
                        type='date'
                    />
                </div>
                <div className='col'>
                    <InputField label='Номер в журнале'
                        name='registrationJournalEntryNumber'
                        formStore={formStore}
                        type='number'
                    />
                </div>
            </div>
        </>
    );
}


export default observer(BaseMedicalReportEditorForm);