import React from 'react';

import useStore from './../../../hooks/useStore';
import EditorModal from '../../../components/editorModal';
import BaseMedicalReportEditorForm from './../../../components/baseMedicalReportEditorForm';


const MedicalReportModal = (props) => {
    const {
        preliminaryExaminationsStore: { medicalReportModal },
        preliminaryExaminationEditorStore: { checkupStatusMedicalReportEditorStore }
    } = useStore();

    return (
        <EditorModal
            title="Медицинское заключение"
            editorStore={checkupStatusMedicalReportEditorStore}
            modalStore={medicalReportModal}
        >
            <BaseMedicalReportEditorForm formStore={checkupStatusMedicalReportEditorStore} />
        </EditorModal>
    )
}

export default MedicalReportModal;