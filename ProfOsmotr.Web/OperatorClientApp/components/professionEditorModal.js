import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../hooks/useStore';
import EditorModal from './editorModal';
import ProfessionEditor from './professionEditor';


const ProfessionEditorModal = ({ workPlaceEditorStore, initialValues }) => {
    const { professionEditorStore } = useStore();

    const onSubmitted = (response) => {
        workPlaceEditorStore.setProfession(response);
    }

    return (
        <EditorModal
            title="Профессия"
            modalStore={workPlaceEditorStore.professionEditorModalStore}
            editorStore={professionEditorStore}
            onSubmitted={onSubmitted}
            reloadOnSubmit={false}
            backdropClassName='z-index-1050'
        >
            <ProfessionEditor />
        </EditorModal>
    );
}

export default ProfessionEditorModal;