import React from 'react';

import InputField from './../../../components/forms/observers/inputField';
import useStore from './../../../hooks/useStore';
import EditorModal from './../../../components/editorModal';

const ReportDataEditorModal = () => {
    const {
        periodicExaminationEditorStore: { reportDataEditorStore },
        periodicExaminationsStore: { reportDataModal }
    } = useStore();

    return (
        <EditorModal title="Заключительный акт"
            editorStore={reportDataEditorStore}
            modalStore={reportDataModal}
        >
            <InputField
                label="Дата акта"
                name="reportDate"
                formStore={reportDataEditorStore}
                type="date"
                style={{ width: '200px' }}
            />
            <InputField
                label="Рекомендации"
                name="recommendations"
                formStore={reportDataEditorStore}
                maxLength="500"
            />
        </EditorModal>
    );
}

export default ReportDataEditorModal;