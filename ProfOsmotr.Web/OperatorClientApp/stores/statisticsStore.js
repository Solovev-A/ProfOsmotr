import { makeAutoObservable, runInAction } from 'mobx';

import statisticsApiService from '../services/statisticsApiService'

class StatisticsStore {
    constructor() {
        this.clear();
        makeAutoObservable(this);
    }

    loadExaminationsStatistics = async () => {
        this.isLoading = true;

        const response = await statisticsApiService.getExaminationsStatistics();

        runInAction(() => {
            if (response && response.success !== false) {
                this.examinationsData = response;
            }
            this.isLoading = false;
        });

        return response;
    }

    clear = () => {
        this.examinationsData = null;
        this.isLoading = true;
    }
}

export default StatisticsStore;