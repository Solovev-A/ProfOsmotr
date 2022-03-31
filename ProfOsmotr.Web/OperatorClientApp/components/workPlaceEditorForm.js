import React from 'react';
import { observer } from 'mobx-react-lite';
import styled from 'styled-components';

import EmployerAutocomplete from './employerAutocomplete';
import DepartmentAutocomplete from './departmentAutocomplete';
import ProfessionAutocomplete from './professionAutocomplete';
import { AddBtn, CloneBtn, EditBtn } from './buttons';
import EmployerEditorModal from './employerEditorModal';
import useStore from './../hooks/useStore';
import EmployerDepartmentEditorModal from './employerDepartmentEditorModal';
import ProfessionEditorModal from './professionEditorModal';
import { ControlRow, ActionsContainer } from './forms/general/grid';


const WorkPlaceEditorForm = observer(({ editorStore, canChangeEmployer = true }) => {
    const { employerEditorStore, employerDepartmentEditorStore, professionEditorStore, orderStore } = useStore();

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

    const openProfessionEditorModal = (profession) => {
        professionEditorStore.setProfession(profession);
        editorStore.professionEditorModalStore.open();
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
                        <AddBtn onClick={() => openEmployerEditorModal(null)}
                            disabled={!canChangeEmployer}
                        />
                        <EditBtn onClick={() => openEmployerEditorModal(editorStore.employer.id)}
                            disabled={!editorStore.employer || !canChangeEmployer}
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
                        <AddBtn onClick={() => openProfessionEditorModal(null)} />
                        <CloneBtn onClick={() => openProfessionEditorModal(editorStore.profession)}
                            disabled={!editorStore.profession}
                        />
                    </ActionsContainer>
                </ControlRow>
            </div>
            <EmployerEditorModal workPlaceEditorStore={editorStore} />
            <EmployerDepartmentEditorModal workPlaceEditorStore={editorStore} />
            <ProfessionEditorModal workPlaceEditorStore={editorStore} />
        </>
    )
})

export default WorkPlaceEditorForm;