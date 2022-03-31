import React from 'react';

import useStore from '../hooks/useStore';
import EditorModal from './editorModal';
import EmployerDepartmentEditor from './employerDepartmentEditor';

const EmployerDepartmentEditorModal = ({ workPlaceEditorStore }) => {
    const { employerDepartmentEditorStore } = useStore();

    const onSubmitted = (response) => {
        const newEmployerDepartment = response.success === true
            // для результата редактирования
            ? { ...employerDepartmentEditorStore.data, id: employerDepartmentEditorStore.employerDepartmentId }
            // для результата создания
            : response;

        workPlaceEditorStore.setEmployerDepartment(newEmployerDepartment);
    }

    return (
        <EditorModal
            title="Структурное подразделение"
            modalStore={workPlaceEditorStore.employerDepartmentEditorModalStore}
            editorStore={employerDepartmentEditorStore}
            onSubmitted={onSubmitted}
            reloadOnSubmit={false}
            backdropClassName='z-index-1050'
        >
            <EmployerDepartmentEditor />
        </EditorModal>
    );
}

export default EmployerDepartmentEditorModal;