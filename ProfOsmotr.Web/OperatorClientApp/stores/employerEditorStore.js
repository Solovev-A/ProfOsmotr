import { makeObservable, observable, action } from 'mobx';

import BaseFormStore from "./baseFormStore";
import { requiredString } from "../utils/validation";
import employerApiService from '../services/employerApiService';
import { handleResponseWithToasts } from '../utils/toasts';

const employerTemplate = {
    name: '',
    headLastName: '',
    headFirstName: '',
    headPatronymicName: '',
    headPosition: ''
}

const validation = {
    name: {
        isValid: requiredString,
        errorMessage: "Укажите название"
    }
}

class EmployerEditorStore extends BaseFormStore {
    employerId = null;

    constructor() {
        super(employerTemplate, validation);

        makeObservable(this, {
            employerId: observable,
            setEmployerId: action,
            loadInitialValues: action,
            onSubmit: action
        })
    }

    setEmployerId = (newId) => {
        this.employerId = newId;
    }

    loadInitialValues = async (cancellationToken) => {
        if (!this.employerId) {
            this.isLoading = false;
            return;
        }
        const data = await employerApiService.getEntity(this.employerId);

        if (!cancellationToken.isCancelled) {
            if (data.success === false) throw data.message
            this.setInitialValues(data);
        }
    }

    onSubmit = async () => {
        const handler = this.employerId
            ? () => employerApiService.updateEntity(this.employerId, this.patchedData)
            : () => employerApiService.createEntity(this.data);

        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default EmployerEditorStore;