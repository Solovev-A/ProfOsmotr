import { makeObservable, action } from 'mobx';

import BaseFormStore from './baseFormStore';
import { handleResponseWithToasts } from '../utils/toasts';


const employerDataTemplate = {
    employeesTotal: 0,
    employeesWomen: 0,
    employeesUnder18: 0,
    employeesPersistentlyDisabled: 0,
    workingWithHarmfulFactorsTotal: 0,
    workingWithHarmfulFactorsWomen: 0,
    workingWithHarmfulFactorsUnder18: 0,
    workingWithHarmfulFactorsPersistentlyDisabled: 0,
    workingWithJobTypesTotal: 0,
    workingWithJobTypesWomen: 0,
    workingWithJobTypesUnder18: 0,
    workingWithJobTypesPersistentlyDisabled: 0
}


class EmployerDataEditorStore extends BaseFormStore {
    constructor(examinationEditorStore) {
        super(employerDataTemplate);
        this.examinationEditorStore = examinationEditorStore;

        makeObservable(this, {
            loadInitialValues: action,
            onSubmit: action
        })
    }

    loadInitialValues = async () => {
        const examination = await this.examinationEditorStore.loadExamination();
        const employerData = examination.employerData;

        this.setInitialValues({
            ...employerData
        })
    }

    onSubmit = async () => {
        const hasChanges = !!Object.keys(this.patchedData).length;
        const data = hasChanges ? { employerData: this.patchedData } : {};

        const handler = () => this.examinationEditorStore.onUpdate(data);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default EmployerDataEditorStore;