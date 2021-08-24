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
        preliminaryExaminationEditorStore: { preliminaryExaminationMedicalReportEditorStore }
    } = useStore();

    useEffect(() => {
        checkupResultsStore.loadCheckupResults();
    }, [])

    return (
        <EditorModal
            title="Медицинское заключение"
            editorStore={preliminaryExaminationMedicalReportEditorStore}
            modalStore={medicalReportModal}
        >
            <DropdownSelect
                label='Результат медицинского осмотра'
                name='checkupResultId'
                id='checkupResultId'
                value={preliminaryExaminationMedicalReportEditorStore.model.checkupResultId}
                onChange={preliminaryExaminationMedicalReportEditorStore.updateProperty}
                isInvalid={preliminaryExaminationMedicalReportEditorStore.errors['checkupResultId']}
                errorMessage={preliminaryExaminationMedicalReportEditorStore.errors['checkupResultId']}
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
                formStore={preliminaryExaminationMedicalReportEditorStore}
            />
            <div className='row'>
                <div className='col'>
                    <InputField label='Дата завершения'
                        name='dateOfComplition'
                        formStore={preliminaryExaminationMedicalReportEditorStore}
                        type='date'
                    />
                </div>
                <div className='col'>
                    <InputField label='Номер в журнале'
                        name='registrationJournalEntryNumber'
                        formStore={preliminaryExaminationMedicalReportEditorStore}
                        type='number'
                    />
                </div>
            </div>
        </EditorModal>
    )
}

export default observer(MedicalReportModal);