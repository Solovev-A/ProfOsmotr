import patientApiService from './../services/patientApiService';
import BasePagedListStore from './basePagedListStore';

class PatientsListStore extends BasePagedListStore {
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