import BaseFormStore from './baseFormStore';
import { action, reaction } from 'mobx';
import { makeObservable } from 'mobx';
import { handleResponseWithToasts } from '../utils/toasts';

const template = {
    checkupResultId: 'empty',
    dateOfCompletion: '',
    checkupStarted: false
}

class ContingentGroupMedicalReportEditorStore extends BaseFormStore {
    constructor(periodicExaminationEditorStore) {
        super(template);
        this.periodicExaminationEditorStore = periodicExaminationEditorStore;

        makeObservable(this, {
            loadInitialValues: action,
            onSubmit: action
        })

        reaction(() => this.model.checkupResultId,
            (value) => {
                if (value === 'empty') {
                    this.updateProperty('dateOfCompletion', '');
                }
                else {
                    this.updateProperty('checkupStarted', true);
                }
            })
    }

    loadInitialValues = () => {
        this.setInitialValues(template);
        return Promise.resolve();
    }

    onSubmit = async () => {
        const contingentGroupMedicalReport = this.data;
        if (contingentGroupMedicalReport.checkupResultId === 'empty') contingentGroupMedicalReport.checkupResultId = null;
        contingentGroupMedicalReport.checkupStatuses = this.periodicExaminationEditorStore.checkedCheckupStatusesId;


        const data = {
            contingentGroupMedicalReport
        }

        const handler = () => this.periodicExaminationEditorStore.onUpdate(data);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default ContingentGroupMedicalReportEditorStore;