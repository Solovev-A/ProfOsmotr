import { makeObservable, observable, action } from 'mobx';

import BaseFormStore from './baseFormStore';
import { requiredString } from './../utils/validation';
import patientApiService from './../services/patientApiService';
import { handleResponseWithToasts } from './../utils/toasts';
import { formatDateStringForDateInput } from './../utils/formatDate';

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
        if (!this.patientId) {
            this.isLoading = false;
            return;
        }
        const data = await patientApiService.getEntity(this.patientId);
        if (!cancellationToken || !cancellationToken.isCancelled) {
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
            ? () => patientApiService.updateEntity(this.patientId, this.patchedData)
            : () => patientApiService.createEntity(this.data);

        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default PatientEditorStore;
