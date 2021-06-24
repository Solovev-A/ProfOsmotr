import api from './api';
import ApiServiceBase from './apiServiceBase';

class PatientApiService extends ApiServiceBase {
    constructor() {
        super('/patients');
    }

    listActualPatients = () => {
        return api.get(`${this.baseUrl}/actual`);
    }
}

export default new PatientApiService();