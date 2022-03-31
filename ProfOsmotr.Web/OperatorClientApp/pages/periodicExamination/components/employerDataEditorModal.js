import React from 'react';

import useStore from './../../../hooks/useStore';
import EditorModal from './../../../components/editorModal';
import EmployerDataView from './employerDataView';

const EmployerDataEditorModal = () => {
    const {
        periodicExaminationEditorStore: { employerDataEditorStore },
        periodicExaminationsStore: { employerDataModal }
    } = useStore();

    return (
        <EditorModal title="Сведения о работодателе"
            editorStore={employerDataEditorStore}
            modalStore={employerDataModal}
            size='xl'
        >
            <EmployerDataView formStore={employerDataEditorStore} />
        </EditorModal>
    );
}

export default EmployerDataEditorModal;