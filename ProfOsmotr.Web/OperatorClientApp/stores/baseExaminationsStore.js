import { runInAction, makeObservable, action, observable } from 'mobx';

import BasePagedListStore from './basePagedListStore';
import { handleResponseWithToasts } from '../utils/toasts';

class BaseExaminationsStore extends BasePagedListStore {
    constructor(apiService) {
        super({
            loader: apiService.listEntities,
            initialListLoader: apiService.listActualExaminations,
            minQueryLength: 3,
            notFoundErrorMessage: 'Медосмотр не найден'
        });

        this.apiService = apiService;
        this.resetExamination();
        makeObservable(this, {
            examination: observable,
            examinationSlug: observable,
            isExaminationLoading: observable,
            loadExamination: action,
            resetExamination: action.bound,
            removeExamination: action,
            setExaminationSlug: action
        })
    }

    resetExamination() {
        this.examination = null;
        this.examinationSlug = null;
        this.isExaminationLoading = true;
    }

    loadExamination = async (cancellationToken) => {
        this.isExaminationLoading = true;
        const response = await this.apiService.getEntity(this.examinationSlug);

        if (!cancellationToken.isCancelled) {
            if (response.success === false) throw response.message;

            runInAction(() => {
                this.examination = response;
                this.isExaminationLoading = false;
            })
        }

        return response;
    }

    removeExamination = async () => {
        const confirmation = confirm('Вы действительно хотите удалить медосмотр?');
        if (!confirmation) return null;

        const response = await this.apiService.deleteEntity(this.examinationSlug);
        return handleResponseWithToasts(response, true);
    }

    setExaminationSlug = (newSlug) => {
        this.examinationSlug = newSlug;
    }
}

export default BaseExaminationsStore;