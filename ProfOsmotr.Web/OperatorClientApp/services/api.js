import axios from 'axios';

const instance = axios.create({
    baseURL: '/api',
    withCredentials: true
});

const errorHandler = (error) => {
    const errors = error.response.data.errors;
    const message = Array.isArray(errors)
        ? errors.join('\n')
        : error.message;

    return { success: false, message }
}

class Api {
    get = async (url, config = {}) => {
        try {
            const response = await instance.get(url, config);
            return response.data;
        }
        catch (error) {
            return errorHandler(error);
        }
    }

    post = async (url, data, config = {}) => {
        try {
            const response = await instance.post(url, data, config);
            return response.data;
        }
        catch (error) {
            return errorHandler(error);
        }
    }

    patch = async (url, data, config = {}) => {
        try {
            const response = await instance.patch(url, data, config);
            return { success: true };
        }
        catch (error) {
            return errorHandler(error);
        }
    }

    delete = async (url, config = {}) => {
        try {
            const response = await instance.delete(url, config);
            return { success: true };
        }
        catch (error) {
            return errorHandler(error);
        }
    }
}

export default new Api();