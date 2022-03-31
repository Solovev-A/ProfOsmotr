import api from "./api";

class ApiServiceBase {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    listEntities = (search, page, itemsPerPage) => {
        return api.get(this.baseUrl, {
            params: { search, page, itemsPerPage }
        });
    }

    getEntity = (id) => {
        return api.get(`${this.baseUrl}/${id}`);
    }

    createEntity = (entity) => {
        return api.post(this.baseUrl, entity);
    }

    updateEntity = (id, patchedData) => {
        return api.patch(`${this.baseUrl}/${id}`, patchedData);
    }

    deleteEntity = (id) => {
        return api.delete(`${this.baseUrl}/${id}`);
    }
}

export default ApiServiceBase;