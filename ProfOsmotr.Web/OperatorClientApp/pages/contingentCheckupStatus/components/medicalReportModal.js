import React from 'react';

import useStore from './../../../hooks/useStore';
import EditorModal from '../../../components/editorModal';
import BaseMedicalReportEditorForm from './../../../components/baseMedicalReportEditorForm';
import MedicalReportEditorForm from './medicalReportEditorForm';


const MedicalReportModal = (props) => {
    const {
        contingentCheckupStatusStore: { medicalReportModal },
        contingentCheckupStatusEditorStore: { checkupStatusMedicalReportEditorStore }
    } = useStore();

    return (
        <EditorModal
            title="Медицинское заключение"
            editorStore={checkupStatusMedicalReportEditorStore}
            modalStore={medicalReportModal}
        >
            <BaseMedicalReportEditorForm formStore={checkupStatusMedicalReportEditorStore} />
            <MedicalReportEditorForm formStore={checkupStatusMedicalReportEditorStore} />
        </EditorModal>
    )
}

export default MedicalReportModal;