import api from './api';
import ApiServiceBase from './apiServiceBase';

class PatientApiService extends ApiServiceBase {
    constructor() {
        super('/patients');
    }

    listActualPatients = () => {
        return api.get(`${this.baseUrl}/actual`);
    }

    findWithSuggestions = (search, employerId) => {
        return api.get(`${this.baseUrl}`, {
            params: { search, employerId }
        });
    }
}

export default new PatientApiService();