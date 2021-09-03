import { makeObservable, observable, action } from 'mobx';

import { requiredString } from '../utils/validation';
import BaseFormStore from './baseFormStore';
import employerApiService from '../services/employerApiService';
import { handleResponseWithToasts } from '../utils/toasts';

const employerDepartmentTemplate = {
    name: ''
}

const validation = {
    name: {
        isValid: requiredString,
        errorMessage: "Укажите название"
    }
}


class EmployerDepartmentEditorStore extends BaseFormStore {
    employerDepartmentId = null;
    parentId = null;

    constructor() {
        super(employerDepartmentTemplate, validation);

        makeObservable(this, {
            employerDepartmentId: observable,
            parentId: observable,
            setEmployerDepartmentId: action,
            loadInitialValues: action,
            onSubmit: action
        })
    }

    setEmployerDepartmentId = (newId) => {
        this.employerDepartmentId = newId;
    }

    setParentId = (newId) => {
        this.parentId = newId;
    }

    loadInitialValues = async () => {
        this.isLoading = false;

        // для modalEditorStore
        // инициализацию производить через setInitialValues
    }

    onSubmit = async () => {
        const handler = this.employerDepartmentId
            ? () => employerApiService.updateDepartment(this.employerDepartmentId, this.patchedData)
            : () => employerApiService.createDepartment(this.parentId, this.data);

        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default EmployerDepartmentEditorStore;