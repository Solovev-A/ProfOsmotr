import api from './api';
import ApiServiceBase from './apiServiceBase';

class EmployerApiService extends ApiServiceBase {
    employerDepartmentBaseUrl = '/departments';

    constructor() {
        super('/employers');
    }

    listActualEmployers = () => {
        return api.get(`${this.baseUrl}/actual`);
    }

    createDepartment = (employerId, data) => {
        return api.post(`${this.baseUrl}/${employerId}${this.employerDepartmentBaseUrl}`, data);
    }

    updateDepartment = (employerDepartmentId, patchedData) => {
        return api.patch(`${this.employerDepartmentBaseUrl}/${employerDepartmentId}`, patchedData);
    }
}

export default new EmployerApiService();