import { makeObservable, action } from 'mobx';

import BaseFormStore from './baseFormStore';
import { handleResponseWithToasts } from '../utils/toasts';
import { requiredString } from './../utils/validation';
import { formatDateStringForDateInput } from './../utils/formatDate';


const reportDataTemplate = {
    reportDate: '',
    recommendations: ''
}

const validation = {
    reportDate: {
        isValid: requiredString,
        errorMessage: 'Укажите дату заключительного акта'
    }
}

class ReportDataEditorStore extends BaseFormStore {
    constructor(examinationEditorStore) {
        super(reportDataTemplate, validation);
        this.examinationEditorStore = examinationEditorStore;

        makeObservable(this, {
            loadInitialValues: action,
            onSubmit: action
        })
    }

    loadInitialValues = async () => {
        const examination = await this.examinationEditorStore.loadExamination();

        this.setInitialValues({
            reportDate: formatDateStringForDateInput(examination.reportDate),
            recommendations: examination.recommendations ?? ''
        })
    }

    onSubmit = async () => {
        const handler = () => this.examinationEditorStore.onUpdate(this.patchedData);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default ReportDataEditorStore;