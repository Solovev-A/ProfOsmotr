import api from './api';
import ApiServiceBase from './apiServiceBase';

class PeriodicExaminationsApiService extends ApiServiceBase {
    checkupStatusesPath = 'checkup-statuses';

    constructor() {
        super('/examinations/periodic');
    }

    listActualExaminations = () => {
        return api.get(`${this.baseUrl}/actual`);
    }

    loadJournal = (year, page, itemsPerPage) => {
        return api.get(`${this.baseUrl}/journal`, {
            params: { year, page, itemsPerPage }
        })
    }

    createCheckupStatus = (examinationId, data) => {
        return api.post(`${this.baseUrl}/${examinationId}/${this.checkupStatusesPath}`, data);
    }

    getCheckupStatus = (id) => {
        const url = this.getCheckupStatusUrl(id);
        return api.get(url);
    }

    updateCheckupStatus = (id, data) => {
        const url = this.getCheckupStatusUrl(id);
        return api.patch(url, data)
    }

    removeCheckupStatus = (id) => {
        const url = this.getCheckupStatusUrl(id);
        return api.delete(url);
    }

    getCheckupStatusUrl = (checkupStatusId) => {
        return `${this.baseUrl}/${this.checkupStatusesPath}/${checkupStatusId}`
    }
}

export default new PeriodicExaminationsApiService();