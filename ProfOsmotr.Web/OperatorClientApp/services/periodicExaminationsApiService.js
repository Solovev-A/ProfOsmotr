import api from './api';
import ApiServiceBase from './apiServiceBase';

class PeriodicExaminationsApiService extends ApiServiceBase {
    constructor() {
        super('/examinations/periodic');
    }

    listActualExaminations = () => {
        return api.get(`${this.baseUrl}/actual`);
    }

    createCheckupStatus = (examinationId, data) => {
        return api.post(`${this.baseUrl}/${examinationId}/checkup-statuses`, data);
    }
}

export default new PeriodicExaminationsApiService();