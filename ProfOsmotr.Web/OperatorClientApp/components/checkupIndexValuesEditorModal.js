import React from 'react';
import { observer } from 'mobx-react-lite';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLightbulb } from '@fortawesome/free-solid-svg-icons';

import CheckupExaminationResultIndexesEditor from './checkupExaminationResultIndexesEditor';
import EditorModal from './editorModal';


const CheckupIndexValuesEditorModal = ({ modalStore, editorStore }) => {
    return (
        <EditorModal
            title="Результаты обследований"
            editorStore={editorStore}
            modalStore={modalStore}
            scrollable={true}
        >
            <CheckupExaminationResultIndexesEditor
                examinationResultIndexes={editorStore.checkupExaminationResultIndexes}
                editorStore={editorStore}
                onValueChange={editorStore.updateIndexValue}
            />
            <div className="h4 mb-3">
                <FontAwesomeIcon icon={faLightbulb} /> Обследования, подходящие для профессии
            </div>
            <CheckupExaminationResultIndexesEditor
                examinationResultIndexes={editorStore.suggestions}
                editorStore={editorStore}
                onValueChange={editorStore.updateSuggestionIndexValue}
            />
        </EditorModal>
    )
}

export default observer(CheckupIndexValuesEditorModal);