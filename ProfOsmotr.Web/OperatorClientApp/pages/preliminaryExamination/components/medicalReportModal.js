import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../../hooks/useStore';
import DropdownSelect from './../../../components/forms/general/dropdownSelect';
import InputField from './../../../components/forms/observers/inputField'
import EditorModal from '../../../components/editorModal';


const MedicalReportModal = (props) => {
    const {
        preliminaryExaminationsStore: { medicalReportModal },
        checkupResultsStore,
        preliminaryExaminationEditorStore: { checkupStatusMedicalReportEditorStore }
    } = useStore();

    useEffect(() => {
        checkupResultsStore.loadCheckupResults();
    }, [])

    return (
        <EditorModal
            title="Медицинское заключение"
            editorStore={checkupStatusMedicalReportEditorStore}
            modalStore={medicalReportModal}
        >
            <DropdownSelect
                label='Результат медицинского осмотра'
                name='checkupResultId'
                id='checkupResultId'
                value={checkupStatusMedicalReportEditorStore.model.checkupResultId}
                onChange={checkupStatusMedicalReportEditorStore.updateProperty}
                isInvalid={checkupStatusMedicalReportEditorStore.errors['checkupResultId']}
                errorMessage={checkupStatusMedicalReportEditorStore.errors['checkupResultId']}
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
                formStore={checkupStatusMedicalReportEditorStore}
            />
            <div className='row'>
                <div className='col'>
                    <InputField label='Дата завершения'
                        name='dateOfComplition'
                        formStore={checkupStatusMedicalReportEditorStore}
                        type='date'
                    />
                </div>
                <div className='col'>
                    <InputField label='Номер в журнале'
                        name='registrationJournalEntryNumber'
                        formStore={checkupStatusMedicalReportEditorStore}
                        type='number'
                    />
                </div>
            </div>
        </EditorModal>
    )
}

export default observer(MedicalReportModal);