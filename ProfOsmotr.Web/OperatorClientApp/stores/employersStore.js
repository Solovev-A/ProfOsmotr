import { action, makeObservable, observable, runInAction } from "mobx";
import periodicExaminationsApiService from "../services/periodicExaminationsApiService";
import { handleResponseWithToasts } from "../utils/toasts";

import employerApiService from './../services/employerApiService'
import BasePagedListStore from './basePagedListStore';

class EmployersStore extends BasePagedListStore {
    employerSlug = null;
    isEmployerLoading = true;
    employer = null;
    isExaminationCreating = false;

    constructor() {
        super({
            loader: employerApiService.listEntities,
            initialListLoader: employerApiService.listActualEmployers,
            minQueryLength: 3,
            notFoundErrorMessage: 'Организация не найдена'
        });

        makeObservable(this, {
            employerSlug: observable,
            isEmployerLoading: observable,
            isExaminationCreating: observable,
            employer: observable,
            setEmployerSlug: action,
            loadEmployer: action,
            resetEmployer: action,
            onAddPeriodicExamination: action
        })
    }

    setEmployerSlug = (newSlug) => {
        this.employerSlug = newSlug;
    }

    loadEmployer = async (cancellationToken) => {
        if (!this.employerSlug) return;

        this.isEmployerLoading = true;
        const response = await employerApiService.getEntity(this.employerSlug);

        if (!cancellationToken.isCancelled) {
            if (response.success === false) throw response.message;

            runInAction(() => {
                this.employer = response;
                this.isEmployerLoading = false;
            })
        }

        return response;
    }

    resetEmployer = () => {
        this.isEmployerLoading = true;
        this.employerSlug = null;
        this.employer = null;
    }

    onAddPeriodicExamination = (year) => {
        if (this.isExaminationCreating) return Promise.resolve({ success: false });
        this.isExaminationCreating = true;

        const data = {
            employerId: this.employerSlug,
            examinationYear: year
        };

        return periodicExaminationsApiService.createEntity(data)
            .then(response => {
                return handleResponseWithToasts(response, true)
            })
            .finally(() => {
                runInAction(() => {
                    this.isExaminationCreating = false;
                });
            })
    }
}

export default EmployersStore;