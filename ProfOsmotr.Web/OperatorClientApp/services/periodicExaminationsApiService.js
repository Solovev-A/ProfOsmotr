import api from './api';
import ApiServiceBase from './apiServiceBase';

class PeriodicExaminationsApiService extends ApiServiceBase {
    constructor() {
        super('/examinations/periodic');
    }

    listActualExaminations = () => {
        return api.get(`${this.baseUrl}/actual`);
    }
}

export default new PeriodicExaminationsApiService();