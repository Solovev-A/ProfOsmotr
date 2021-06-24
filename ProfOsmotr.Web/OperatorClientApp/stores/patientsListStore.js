import patientApiService from './../services/patientApiService';
import BaseListStore from './baseListStore';

class PatientsListStore extends BaseListStore {
    constructor() {
        super({
            loader: patientApiService.listEntities,
            initialListLoader: patientApiService.listActualPatients,
            minQueryLength: 3,
            notFoundErrorMessage: 'Пациент не найден'
        });
    }
}

export default PatientsListStore;