import api from "./api";

class ProfessionApiService {
    constructor() {
        this.baseUrl = '/professions';
    }

    create = (profession) => {
        return api.post(this.baseUrl, profession);
    }

    find = (search, employerId) => {
        return api.get(this.baseUrl, {
            params: { search, employerId }
        });
    }
}

export default new ProfessionApiService();