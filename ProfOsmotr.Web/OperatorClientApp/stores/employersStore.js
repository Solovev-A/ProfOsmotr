import { action, makeObservable, observable, runInAction } from "mobx";

import employerApiService from './../services/employerApiService'
import BasePagedListStore from './basePagedListStore';

class EmployersStore extends BasePagedListStore {
    employerSlug = null;
    isEmployerLoading = true;
    employer = null;

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
            employer: observable,
            setEmployerSlug: action,
            loadEmployer: action,
            resetEmployer: action
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
}

export default EmployersStore;