import BasePagedListStore from './basePagedListStore';
import preliminaryExaminationsApiService from './../services/preliminaryExaminationsApiService';
import { runInAction, makeObservable, action, observable } from 'mobx';
import { handleResponseWithToasts } from '../utils/toasts';
import ModalStore from './modalStore';

class PreliminaryExaminationsStore extends BasePagedListStore {
    constructor() {
        super({
            loader: preliminaryExaminationsApiService.listEntities,
            initialListLoader: preliminaryExaminationsApiService.listActualExaminations,
            minQueryLength: 3,
            notFoundErrorMessage: 'Медосмотр не найден'
        });

        this.resetExamination();
        makeObservable(this, {
            examination: observable,
            examinationSlug: observable,
            isExaminationLoading: observable,
            loadEmployerExaminations: action,
            loadExamination: action,
            resetExamination: action,
            removeExamination: action,
            setExaminationSlug: action
        })
    }

    resetExamination = () => {
        this.examination = null;
        this.examinationSlug = null;
        this.isExaminationLoading = true;
        this.workPlaceModal = new ModalStore();
        this.checkupIndexValuesModal = new ModalStore();
        this.medicalReportModal = new ModalStore();
    }

    loadExamination = async (cancellationToken) => {
        this.isExaminationLoading = true;
        const response = await preliminaryExaminationsApiService.getEntity(this.examinationSlug);

        if (!cancellationToken.isCancelled) {
            if (response.success === false) throw response.message;

            runInAction(() => {
                this.examination = response;
                this.isExaminationLoading = false;
            })
        }

        return response;
    }

    loadEmployerExaminations = async (employerId, page = 1) => {
        this.inProgress = true;
        this.page = page;
        const response = await preliminaryExaminationsApiService.listEmployerExaminations(employerId, this.page, this.itemsPerPage);

        if (response.success !== false) {
            runInAction(() => {
                this.items = response.items;
                this.totalCount = response.totalCount;
                this.inProgress = false;
            });
        }

        return handleResponseWithToasts(response);
    }

    removeExamination = async () => {
        const confirmation = confirm('Вы действительно хотите удалить медосмотр?');
        if (!confirmation) return null;

        const response = await preliminaryExaminationsApiService.deleteEntity(this.examinationSlug);
        return handleResponseWithToasts(response, true);
    }

    setExaminationSlug = (newSlug) => {
        this.examinationSlug = newSlug;
    }
}

export default PreliminaryExaminationsStore;