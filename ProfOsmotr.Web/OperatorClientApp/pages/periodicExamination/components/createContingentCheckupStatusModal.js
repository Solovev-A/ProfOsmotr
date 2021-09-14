import React from 'react';
import { observer } from 'mobx-react-lite';

import EditorModal from '../../../components/editorModal';
import WorkPlaceEditorForm from '../../../components/workPlaceEditorForm';
import PatientAutocomplete from '../../../components/patientAutocomplete';
import useStore from '../../../hooks/useStore';
import { AddBtn, EditBtn } from '../../../components/buttons';
import { ActionsContainer, ControlRow } from '../../../components/forms/general/grid';
import PatientEditorModal from '../../../components/patientEditorModal';


const CreateContingentCheckupStatusModal = observer((props) => {
    const { contingentCheckupStatusCreatorStore, periodicExaminationsStore, patientEditorStore } = useStore();
    const { patient, patientAutocompleteStore, setPatient, workPlace, patientEditorModal } = contingentCheckupStatusCreatorStore;

    const openPatientEditorModal = (patientId) => {
        patientEditorStore.setPatientId(patientId);
        patientEditorModal.open();
    }

    return (
        <EditorModal
            title="Добавление работника в список"
            editorStore={contingentCheckupStatusCreatorStore}
            modalStore={periodicExaminationsStore.createContingentCheckupStatusModal}
            scrollable={false}
            closeOnSubmit={false}
            reloadOnExited={true}
        >
            <div className="form-group">
                <label>Работник</label>
                <ControlRow>
                    <PatientAutocomplete
                        value={patient}
                        onChange={setPatient}
                        patientListStore={patientAutocompleteStore}
                    />
                    <ActionsContainer>
                        <AddBtn onClick={() => openPatientEditorModal(null)} />
                        <EditBtn onClick={() => openPatientEditorModal(patient.id)}
                            disabled={!patient}
                        />
                    </ActionsContainer>
                </ControlRow>
            </div>
            <WorkPlaceEditorForm
                editorStore={workPlace}
                canChangeEmployer={false}
            />
            <PatientEditorModal />
        </EditorModal>
    )
})

export default CreateContingentCheckupStatusModal;