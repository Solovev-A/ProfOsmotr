import { makeObservable, override } from 'mobx';

import BaseExaminationsStore from './baseExaminationsStore';
import periodicExaminationsApiService from './../services/periodicExaminationsApiService';
import ModalStore from './modalStore';



class PeriodicExaminationsStore extends BaseExaminationsStore {
    constructor() {
        super(periodicExaminationsApiService);

        makeObservable(this, {
            resetExamination: override
        })
    }

    resetExamination() {
        super.resetExamination();
        this.reportDataModal = new ModalStore();
        this.employerDataModal = new ModalStore();
        this.createContingentCheckupStatusModal = new ModalStore();
        this.contingentGroupMedicalReportEditorModal = new ModalStore();
    }
}

export default PeriodicExaminationsStore;