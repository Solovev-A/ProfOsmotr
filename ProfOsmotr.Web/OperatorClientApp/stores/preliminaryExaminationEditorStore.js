import WorkPlaceEditorStore from './workplaceEditorStore';
import CheckupIndexValuesEditorStore from './checkupIndexValuesEditorStore';
import CheckupStatusMedicalReportEditorStore from './checkupStatusMedicalReportEditorStore';
import preliminaryExaminationsApiService from '../services/preliminaryExaminationsApiService';

class PreliminaryExaminationEditorStore {
    constructor(rootStore) {
        this.rootStore = rootStore;
        this.workPlaceEditorStore = new WorkPlaceEditorStore(this);
        this.checkupIndexValuesEditorStore = new CheckupIndexValuesEditorStore(rootStore, this);
        this.checkupStatusMedicalReportEditorStore = new CheckupStatusMedicalReportEditorStore(this);
    }

    loadCheckupStatus = () => {
        return this.rootStore.preliminaryExaminationsStore.examination;
    }

    onUpdate = (data) => {
        if (!Object.entries(data).length) return Promise.resolve(true);

        const examinationId = this.rootStore.preliminaryExaminationsStore.examination.id;
        return preliminaryExaminationsApiService.updateEntity(examinationId, data);
    }
}

export default PreliminaryExaminationEditorStore;