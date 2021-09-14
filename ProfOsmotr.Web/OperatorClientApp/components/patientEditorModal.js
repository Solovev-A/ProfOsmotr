import React from 'react';

import useStore from '../hooks/useStore';
import EditorModal from './editorModal';
import PatientEditor from './patientEditor';


const PatientEditorModal = (props) => {
    const { patientEditorStore, contingentCheckupStatusCreatorStore } = useStore();

    const onSubmitted = (response) => {
        const patient = response.success === true
            // для реузультата редактирования
            ? { ...patientEditorStore.data, id: patientEditorStore.patientId }
            // для результата создания
            : response

        contingentCheckupStatusCreatorStore.setPatient(patient);
    }

    return (
        <EditorModal
            title="Пациент"
            modalStore={contingentCheckupStatusCreatorStore.patientEditorModal}
            editorStore={patientEditorStore}
            onSubmitted={onSubmitted}
            reloadOnSubmit={false}
            backdropClassName='z-index-1050'
            size='xl'
        >
            <PatientEditor />
        </EditorModal>
    );
}

export default PatientEditorModal;