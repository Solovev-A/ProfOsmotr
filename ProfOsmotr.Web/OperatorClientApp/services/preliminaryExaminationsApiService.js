import api from './api';
import ApiServiceBase from './apiServiceBase';

class PreliminaryExaminationsApiService extends ApiServiceBase {
    constructor() {
        super('/examinations/preliminary');
    }

    listActualExaminations = () => {
        return api.get(`${this.baseUrl}/actual`);
    }

    listEmployerExaminations = (employerId, page, itemsPerPage) => {
        return api.get(this.baseUrl, {
            params: { employerId, page, itemsPerPage }
        });
    }

    loadJournal = (year, page, itemsPerPage) => {
        return api.get(`${this.baseUrl}/journal`, {
            params: { year, page, itemsPerPage }
        })
    }
}

export default new PreliminaryExaminationsApiService();