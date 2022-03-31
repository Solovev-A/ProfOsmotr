import WorkPlaceEditorStore from './workplaceEditorStore';
import CheckupIndexValuesEditorStore from './checkupIndexValuesEditorStore';
import CheckupStatusMedicalReportEditorStore from './checkupStatusMedicalReportEditorStore';
import periodicExaminationsApiService from '../services/periodicExaminationsApiService';

class ContingentCheckupStatusEditorStore {
    constructor(rootStore) {
        this.rootStore = rootStore;
        this.workPlaceEditorStore = new WorkPlaceEditorStore(this);
        this.checkupIndexValuesEditorStore = new CheckupIndexValuesEditorStore(rootStore, this);
        this.checkupStatusMedicalReportEditorStore = new CheckupStatusMedicalReportEditorStore(this);
    }

    loadCheckupStatus = () => {
        return this.rootStore.contingentCheckupStatusStore.checkupStatus;
    }

    onUpdate = (data) => {
        if (!Object.entries(data).length) return Promise.resolve(true);

        const checkupStatusId = this.rootStore.contingentCheckupStatusStore.checkupStatus.id;
        return periodicExaminationsApiService.updateCheckupStatus(checkupStatusId, data);
    }
}

export default ContingentCheckupStatusEditorStore;