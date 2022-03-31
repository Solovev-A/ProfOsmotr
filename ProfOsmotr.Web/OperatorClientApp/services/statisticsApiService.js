import api from "./api"

class StatisticsApiService {
    getExaminationsStatistics = () => {
        return api.get('statistics/examinations');
    }
}

export default new StatisticsApiService();