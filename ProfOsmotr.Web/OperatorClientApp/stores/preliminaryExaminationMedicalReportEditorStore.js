import { makeObservable, action } from 'mobx';

import BaseFormStore from './baseFormStore';
import { handleResponseWithToasts } from '../utils/toasts';
import { requiredString } from './../utils/validation';
import { formatDateStringForDateInput } from './../utils/formatDate';


const medicalReportTemplate = {
    checkupResultId: 'empty',
    medicalReport: '',
    dateOfComplition: '',
    registrationJournalEntryNumber: ''
}

const validation = {
    checkupResultId: {
        isValid: (value) => value !== 'empty',
        errorMessage: 'Укажите результат медосмотра'
    },
    dateOfComplition: {
        isValid: requiredString,
        errorMessage: 'Укажите дату завершения осмотра'
    }
}

class PreliminaryExaminationMedicalReportEditorStore extends BaseFormStore {
    constructor(examinationEditorStore) {
        super(medicalReportTemplate, validation);
        this.examinationEditorStore = examinationEditorStore;

        makeObservable(this, {
            loadInitialValues: action,
            onSubmit: action
        })
    }

    loadInitialValues = async () => {
        const examination = await this.examinationEditorStore.loadExamination();

        this.setInitialValues({
            checkupResultId: examination.result?.id ?? 'empty',
            medicalReport: examination.medicalReport ?? '',
            dateOfComplition: formatDateStringForDateInput(examination.dateOfComplition),
            registrationJournalEntryNumber: examination.registrationJournalEntryNumber ?? ''
        })
    }

    onSubmit = async () => {
        const handler = () => this.examinationEditorStore.onUpdate(this.patchedData);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default PreliminaryExaminationMedicalReportEditorStore;