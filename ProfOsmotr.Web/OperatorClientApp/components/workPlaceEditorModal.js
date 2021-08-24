import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import EmployerAutocomplete from './employerAutocomplete';
import DepartmentAutocomplete from './departmentAutocomplete';
import ProfessionAutocomplete from './professionAutocomplete';
import EditorModal from './editorModal';
import useStore from '../hooks/useStore';


const WorkPlaceEditorModal = ({ modalStore, editorStore, canChangeEmployer = true }) => {
    const onEnter = () => {

    }

    const onExited = () => {
        editorStore.resetEditorView();
    }

    return (
        <EditorModal
            title="Место работы"
            editorStore={editorStore}
            modalStore={modalStore}
            onEnter={onEnter}
            onExited={onExited}
            scrollable={false}
        >
            <EmployerAutocomplete
                value={editorStore.employer}
                onChange={editorStore.setEmployer}
                disabled={!canChangeEmployer}
            />
            <DepartmentAutocomplete
                value={editorStore.employerDepartment}
                onChange={editorStore.setEmployerDepartment}
                hasEmployer={!!editorStore.employer}
                departmentsListStore={editorStore.employerDepartmentsList}
            />
            <ProfessionAutocomplete
                value={editorStore.profession}
                onChange={editorStore.setProfession}
                professionListStore={editorStore.professionList}
            />
        </EditorModal>
    )
}

export default observer(WorkPlaceEditorModal);