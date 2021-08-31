import WorkPlaceEditorStore from './workplaceEditorStore';
import CheckupIndexValuesEditorStore from './checkupIndexValuesEditorStore';
import PreliminaryExaminationMedicalReportEditorStore from './preliminaryExaminationMedicalReportEditorStore';
import preliminaryExaminationsApiService from '../services/preliminaryExaminationsApiService';

class PreliminaryExaminationEditorStore {
    constructor(rootStore) {
        this.rootStore = rootStore;
        this.workPlaceEditorStore = new WorkPlaceEditorStore(this);
        this.checkupIndexValuesEditorStore = new CheckupIndexValuesEditorStore(rootStore, this);
        this.preliminaryExaminationMedicalReportEditorStore = new PreliminaryExaminationMedicalReportEditorStore(this);
    }

    loadExamination = () => {
        return this.rootStore.preliminaryExaminationsStore.examination;
    }

    onUpdate = (data) => {
        if (!Object.entries(data).length) return Promise.resolve(true);

        const examinationId = this.rootStore.preliminaryExaminationsStore.examination.id;
        return preliminaryExaminationsApiService.updateEntity(examinationId, data);
    }
}

export default PreliminaryExaminationEditorStore;