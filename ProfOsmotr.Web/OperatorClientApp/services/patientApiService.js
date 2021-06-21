import api from './api';

class PatientApiService {
    baseUrl = '/patients';

    listPatients = (search, page, itemsPerPage) => {
        return api.get(this.baseUrl, {
            params: { search, page, itemsPerPage }
        });
    }

    listActualPatients = () => {
        return api.get(`${this.baseUrl}/actual`);
    }

    getPatient = (id) => {
        return api.get(`${this.baseUrl}/${id}`);
    }

    createPatient = ({ lastName, firstName, patronymicName, gender, dateOfBirth, address }) => {
        return api.post(this.baseUrl, {
            lastName, firstName, patronymicName, gender, dateOfBirth, address
        });
    }

    updatePatient = (id, patchedData) => {
        return api.patch(`${this.baseUrl}/${id}`, patchedData);
    }

    deletePatient = (id) => {
        return api.delete(`${this.baseUrl}/${id}`);
    }
}

export default new PatientApiService();