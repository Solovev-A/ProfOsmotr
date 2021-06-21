import { makeObservable, observable, action } from 'mobx';

import BaseFormStore from './baseFormStore';
import { requiredString } from './../utils/validation';
import patientApiService from './../services/patientApiService';
import { handleResponseWithToasts } from './../utils/toasts';

const validation = {
    lastName: {
        isValid: requiredString,
        errorMessage: "Укажите фамилию"
    },
    firstName: {
        isValid: requiredString,
        errorMessage: "Укажите имя"
    },
    gender: {
        isValid: (value) => value === 'male' || value === 'female',
        errorMessage: "Укажите пол"
    },
    dateOfBirth: {
        isValid: requiredString,
        errorMessage: "Укажите дату рождения"
    }
}

const patientTemplate = {
    lastName: '',
    firstName: '',
    patronymicName: '',
    dateOfBirth: '',
    gender: null,
    address: ''
};

class PatientEditorStore extends BaseFormStore {
    patientId = null;

    constructor() {
        super(patientTemplate, validation);

        makeObservable(this, {
            patientId: observable,
            setPatientId: action,
            onSubmit: action,
            loadInitialValues: action
        })
    }

    loadInitialValues = async (cancellationToken) => {
        if (!this.patientId) return;

        this.isLoading = true;

        const data = await patientApiService.getPatient(this.patientId);
        if (!cancellationToken.isCancelled) {
            if (data.success === false) throw data.message

            data.dateOfBirth = formatDateStringForDateInput(data.dateOfBirth);
            this.setInitialValues(data);
        }
    }

    setPatientId = (id) => {
        this.patientId = id;
    }

    onSubmit = async () => {
        const handler = this.patientId
            ? () => patientApiService.updatePatient(this.patientId, this.patchedData)
            : () => patientApiService.createPatient(this.data);

        const response = await this.onSendingData(handler);
        return handleResponseWithToasts(response, true);
    }
}

function formatDateStringForDateInput(value) {
    const date = value.split('.');

    const dd = date[0];
    const MM = date[1];
    const yyyy = date[2];

    return `${yyyy}-${MM}-${dd}`;
}

export default PatientEditorStore;
