import PatientEditorStore from './patientEditorStore';
import PatientStore from './patientStore';
import PatientsListStore from './patientsListStore';
import EmployerEditorStore from './employerEditorStore';
import EmployersStore from './employersStore';
import PreliminaryExaminationsStore from './preliminaryExaminationsStore';
import PreliminaryExaminationEditorStore from './preliminaryExaminationEditorStore';
import CheckupResultsStore from './checkupResultsStore';
import OrderStore from './orderStore';
import EmployerDepartmentEditorStore from './employerDepartmentEditorStore';

class RootStore {
    constructor() {
        this.patientEditorStore = new PatientEditorStore();
        this.patientStore = new PatientStore();
        this.patientsListStore = new PatientsListStore();
        this.employerEditorStore = new EmployerEditorStore();
        this.employersStore = new EmployersStore();
        this.preliminaryExaminationsStore = new PreliminaryExaminationsStore();
        this.preliminaryExaminationEditorStore = new PreliminaryExaminationEditorStore(this);
        this.checkupResultsStore = new CheckupResultsStore();
        this.orderStore = new OrderStore();
        this.employerDepartmentEditorStore = new EmployerDepartmentEditorStore();
    }
}

export default RootStore;