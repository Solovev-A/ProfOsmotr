import React from 'react';

import EmployerEditor from './employerEditor';
import EditorModal from './editorModal';
import useStore from './../hooks/useStore';

const EmployerEditorModal = ({ workPlaceEditorStore }) => {
    const { employerEditorStore } = useStore();

    const onSubmitted = (response) => {
        const newEmployer = response.success == true
            // для результата редактирования
            ? { ...employerEditorStore.data, id: employerEditorStore.employerId }
            // для результата создания
            : response;

        workPlaceEditorStore.setEmployer(newEmployer);
    }

    return (
        <EditorModal
            title="Организация"
            modalStore={workPlaceEditorStore.employerEditorModalStore}
            editorStore={employerEditorStore}
            onSubmitted={onSubmitted}
            reloadOnSubmit={false}
        >
            <EmployerEditor />
        </EditorModal>
    );
}

export default EmployerEditorModal;