import React from 'react';
import { observer } from 'mobx-react-lite';
import styled from 'styled-components';

import EmployerAutocomplete from './employerAutocomplete';
import DepartmentAutocomplete from './departmentAutocomplete';
import ProfessionAutocomplete from './professionAutocomplete';
import { AddBtn, EditBtn } from './buttons';
import EmployerEditorModal from './employerEditorModal';
import useStore from './../hooks/useStore';
import EmployerDepartmentEditorModal from './employerDepartmentEditorModal';


const WorkPlaceEditorForm = observer(({ editorStore, canChangeEmployer = true }) => {
    const { employerEditorStore, employerDepartmentEditorStore } = useStore();

    const openEmployerEditorModal = (employerId) => {
        employerEditorStore.setEmployerId(employerId);
        editorStore.employerEditorModalStore.open();
    }

    const openEmployerDepartmentEditorModal = (employerDepartment) => {
        if (!editorStore.employer) throw 'Не задана организация';

        employerDepartmentEditorStore.setEmployerDepartmentId(employerDepartment?.id);
        employerDepartmentEditorStore.setParentId(editorStore.employer.id);
        employerDepartmentEditorStore.setInitialValues(employerDepartment);
        editorStore.employerDepartmentEditorModalStore.open();
    }

    return (
        <>
            <div className="form-group">
                <label>Организация</label>
                <ControlRow>
                    <EmployerAutocomplete
                        value={editorStore.employer}
                        onChange={editorStore.setEmployer}
                        disabled={!canChangeEmployer}
                    />
                    <ActionsContainer>
                        <AddBtn onClick={() => openEmployerEditorModal(null)} />
                        <EditBtn onClick={() => openEmployerEditorModal(editorStore.employer.id)}
                            disabled={!editorStore.employer}
                        />
                    </ActionsContainer>
                </ControlRow>
            </div>
            <div className="form-group">
                <label>Структурное подразделение</label>
                <ControlRow>
                    <DepartmentAutocomplete
                        value={editorStore.employerDepartment}
                        onChange={editorStore.setEmployerDepartment}
                        hasEmployer={!!editorStore.employer}
                        departmentsListStore={editorStore.employerDepartmentsList}
                    />
                    <ActionsContainer>
                        <AddBtn onClick={() => openEmployerDepartmentEditorModal(null)}
                            disabled={!editorStore.employer}
                        />
                        <EditBtn onClick={() => openEmployerDepartmentEditorModal(editorStore.employerDepartment)}
                            disabled={!editorStore.employerDepartment}
                        />
                    </ActionsContainer>
                </ControlRow>
            </div>
            <div className="form-group">
                <label>Профессия</label>
                <ControlRow>
                    <ProfessionAutocomplete
                        value={editorStore.profession}
                        onChange={editorStore.setProfession}
                        professionListStore={editorStore.professionList}
                    />
                    <ActionsContainer>
                        <AddBtn />
                        <EditBtn />
                    </ActionsContainer>
                </ControlRow>
            </div>
            <EmployerEditorModal workPlaceEditorStore={editorStore} />
            <EmployerDepartmentEditorModal workPlaceEditorStore={editorStore} />
        </>
    )
})

const ControlRow = styled.div`
display: flex;
`

const ActionsContainer = styled.div`
margin-left: 3px;
display: grid;
grid-template-columns: repeat(2, 1fr);
grid-column-gap: 2px;
max-height: calc(1.5em + .75rem + 2px);
align-self: center;
`

export default WorkPlaceEditorForm;