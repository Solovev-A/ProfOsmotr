import { makeObservable, action } from 'mobx';

import BaseFormStore from './baseFormStore';
import { handleResponseWithToasts } from '../utils/toasts';
import { requiredString } from '../utils/validation';
import { formatDateStringForDateInput } from '../utils/formatDate';


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

class CheckupStatusMedicalReportEditorStore extends BaseFormStore {
    constructor(checkupStatusEditorStore) {
        super(medicalReportTemplate, validation);
        this.checkupStatusEditorStore = checkupStatusEditorStore;

        makeObservable(this, {
            loadInitialValues: action,
            onSubmit: action
        })
    }

    loadInitialValues = async () => {
        const checkup = await this.checkupStatusEditorStore.loadCheckupStatus();

        this.setInitialValues({
            checkupResultId: checkup.result?.id ?? 'empty',
            medicalReport: checkup.medicalReport ?? '',
            dateOfComplition: formatDateStringForDateInput(checkup.dateOfComplition),
            registrationJournalEntryNumber: checkup.registrationJournalEntryNumber ?? ''
        })
    }

    onSubmit = async () => {
        const handler = () => this.checkupStatusEditorStore.onUpdate(this.patchedData);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default CheckupStatusMedicalReportEditorStore;