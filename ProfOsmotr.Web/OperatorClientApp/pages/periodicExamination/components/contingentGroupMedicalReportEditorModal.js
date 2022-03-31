import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../../hooks/useStore';
import EditorModal from '../../../components/editorModal';
import CheckupResultSelect from '../../../components/checkupResultSelect';
import InputField from './../../../components/forms/observers/inputField';
import InputCheckbox from '../../../components/forms/observers/inputCheckbox';


const ContingentGroupMedicalReportEditorModal = () => {
    const { periodicExaminationEditorStore, periodicExaminationsStore } = useStore();
    const { contingentGroupMedicalReportEditorStore } = periodicExaminationEditorStore;
    const { contingentGroupMedicalReportEditorModal } = periodicExaminationsStore;

    return (
        <EditorModal title="Групповое редактирование заключения"
            modalStore={contingentGroupMedicalReportEditorModal}
            editorStore={contingentGroupMedicalReportEditorStore}
        >
            <CheckupResultSelect formStore={contingentGroupMedicalReportEditorStore} />
            <div style={{ maxWidth: "250px" }}>
                <InputField label='Дата завершения'
                    name='dateOfCompletion'
                    formStore={contingentGroupMedicalReportEditorStore}
                    disabled={contingentGroupMedicalReportEditorStore.model.checkupResultId === 'empty'}
                    type='date'
                />
            </div>
            <InputCheckbox
                label="Медосмотр начат"
                name="checkupStarted"
                formStore={contingentGroupMedicalReportEditorStore}
                disabled={contingentGroupMedicalReportEditorStore.model.checkupResultId !== 'empty'}
                inline
            />
        </EditorModal>
    );
}

export default observer(ContingentGroupMedicalReportEditorModal);