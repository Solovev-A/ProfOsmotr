import api from './api';
import ApiServiceBase from './apiServiceBase';

class EmployerApiService extends ApiServiceBase {
    constructor() {
        super('/employers');
    }

    listActualEmployers = () => {
        return api.get(`${this.baseUrl}/actual`);
    }
}

export default new EmployerApiService();