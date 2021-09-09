import { runInAction, makeObservable, action, override } from 'mobx';

import BaseExaminationsStore from './baseExaminationsStore';
import preliminaryExaminationsApiService from './../services/preliminaryExaminationsApiService';
import { handleResponseWithToasts } from '../utils/toasts';
import ModalStore from './modalStore';


class PreliminaryExaminationsStore extends BaseExaminationsStore {
    constructor() {
        super(preliminaryExaminationsApiService);

        makeObservable(this, {
            loadEmployerExaminations: action,
            resetExamination: override
        })
    }

    resetExamination() {
        super.resetExamination();
        this.workPlaceModal = new ModalStore();
        this.checkupIndexValuesModal = new ModalStore();
        this.medicalReportModal = new ModalStore();
    }

    loadEmployerExaminations = async (employerId, page = 1) => {
        this.inProgress = true;
        this.page = page;
        const response = await this.apiService.listEmployerExaminations(employerId, this.page, this.itemsPerPage);

        if (response.success !== false) {
            runInAction(() => {
                this.items = response.items;
                this.totalCount = response.totalCount;
                this.inProgress = false;
            });
        }

        return handleResponseWithToasts(response);
    }
}

export default PreliminaryExaminationsStore;