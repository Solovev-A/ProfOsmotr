import React from 'react';
import { observer } from 'mobx-react-lite';

import EditorModal from './editorModal';
import WorkPlaceEditorForm from './workPlaceEditorForm';


const WorkPlaceEditorModal = ({ modalStore, editorStore, canChangeEmployer = true }) => {
    const onExited = () => {
        editorStore.resetEditorView();
    }

    return (
        <EditorModal
            title="Место работы"
            editorStore={editorStore}
            modalStore={modalStore}
            onExited={onExited}
            scrollable={false}
        >
            <WorkPlaceEditorForm editorStore={editorStore} canChangeEmployer={canChangeEmployer} />
        </EditorModal>
    )
}

export default observer(WorkPlaceEditorModal);