import periodicExaminationsApiService from "../services/periodicExaminationsApiService";
import ReportDataEditorStore from './reportDataEditorStore';
import EmployerDataEditorStore from './employerDataEditorStore';
import ContingentGroupMedicalReportEditorStore from './contingentGroupMedicalReportEditorStore';


class PeriodicExaminationEditorStore {
    constructor(rootStore) {
        this.rootStore = rootStore;
        this.reportDataEditorStore = new ReportDataEditorStore(this);
        this.employerDataEditorStore = new EmployerDataEditorStore(this);
        this.contingentGroupMedicalReportEditorStore = new ContingentGroupMedicalReportEditorStore(this);
    }

    loadExamination = () => {
        return this.rootStore.periodicExaminationsStore.examination;
    }

    onUpdate = (data) => {
        if (!Object.entries(data).length) return Promise.resolve(true);

        const examinationId = this.rootStore.periodicExaminationsStore.examination.id;
        return periodicExaminationsApiService.updateEntity(examinationId, data);
    }

    get checkedCheckupStatusesId() {
        return this.rootStore.periodicExaminationsStore.examination.checkupStatuses
            .filter(s => s.checked === true)
            .map(s => s.id);
    }
}

export default PeriodicExaminationEditorStore;