import api from './api';
import { successToast, errorToast } from './toasts';

const handleResponse = (response, showSuccessToast = false) => {
    if (response.success === false) {
        errorToast(response.message);
    }
    else if (showSuccessToast) {
        successToast();
    }
    return response;
}

class PatientApiService {
    baseUrl = '/patients';

    listPatients = async (search, page, itemsPerPage) => {
        const response = await api.get(this.baseUrl, {
            params: { search, page, itemsPerPage }
        });
        return handleResponse(response);
    }

    getPatient = async (id) => {
        const response = await api.get(`${this.baseUrl}/${id}`);
        return handleResponse(response);
    }

    createPatient = async ({ lastName, firstName, patronymicName, gender, dateOfBirth, address }) => {
        const response = await api.post(this.baseUrl, {
            lastName, firstName, patronymicName, gender, dateOfBirth, address
        });
        return handleResponse(response, true);
    }

    updatePatient = async (id, patchedData) => {
        const response = await api.patch(`${this.baseUrl}/${id}`, patchedData);
        return handleResponse(response, true);
    }

    deletePatient = async (id) => {
        const response = await api.delete(`${this.baseUrl}/${id}`);
        return handleResponse(response, true);
    }
}

export default new PatientApiService();