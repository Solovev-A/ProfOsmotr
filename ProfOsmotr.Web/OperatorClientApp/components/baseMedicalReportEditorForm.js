import React from 'react';
import { observer } from 'mobx-react-lite';

import InputField from './forms/observers/inputField';
import CheckupResultSelect from './checkupResultSelect';


const BaseMedicalReportEditorForm = ({ formStore }) => {
    return (
        <>
            <CheckupResultSelect formStore={formStore} />
            <InputField label='Медицинское заключение'
                name='medicalReport'
                formStore={formStore}
            />
            <div className='row'>
                <div className='col'>
                    <InputField label='Дата завершения'
                        name='dateOfCompletion'
                        formStore={formStore}
                        disabled={formStore.model.checkupResultId === 'empty'}
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