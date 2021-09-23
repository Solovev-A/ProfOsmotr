import { runInAction, makeObservable, action, observable, computed } from 'mobx';

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
        this.journal = new BasePagedListStore({
            loader: (search, page, itemsPerPage) => this.apiService.loadJournal(this.journalYear, page, itemsPerPage),
            initialListLoader: () => this.apiService.loadJournal(new Date().getFullYear(), 1, 20),
            minQueryLength: 0
        })
        this.resetExamination();
        this.resetJournal();
        makeObservable(this, {
            journalYear: observable,
            examination: observable,
            examinationSlug: observable,
            isExaminationLoading: observable,
            loadExamination: action,
            resetExamination: action.bound,
            resetJournal: action.bound,
            removeExamination: action,
            setExaminationSlug: action,
            setJournalYear: action
        })
    }

    resetExamination() {
        this.examination = null;
        this.examinationSlug = null;
        this.isExaminationLoading = true;
    }

    resetJournal() {
        this.journalYear = new Date().getFullYear();
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

    setJournalYear = (value) => {
        this.journalYear = value;
        this.journal.loadPage(1);
    }

    get journalYearsRange() {
        const start = 2020;
        const end = new Date().getFullYear(); // текущий год

        const result = [];

        for (let i = start; i <= end; i++) {
            result.push(i);
        }

        return result;
    }
}

export default BaseExaminationsStore;