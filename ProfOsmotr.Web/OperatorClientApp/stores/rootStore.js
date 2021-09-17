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
import ProfessionEditorStore from './professionEditorStore';
import PeriodicExaminationsStore from './periodicExaminationsStore';
import PeriodicExaminationEditorStore from './periodicExaminationEditorStore';
import ContingentCheckupStatusCreatorStore from './contingentCheckupStatusCreatorStore';
import ContingentCheckupStatusStore from './contingentCheckupStatusStore';
import ContingentCheckupStatusEditorStore from './contingentCheckupStatusEditorStore';
import ICD10Store from './icd10Store';

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
        this.professionEditorStore = new ProfessionEditorStore(this);
        this.periodicExaminationsStore = new PeriodicExaminationsStore();
        this.periodicExaminationEditorStore = new PeriodicExaminationEditorStore(this);
        this.contingentCheckupStatusCreatorStore = new ContingentCheckupStatusCreatorStore(this);
        this.contingentCheckupStatusStore = new ContingentCheckupStatusStore();
        this.contingentCheckupStatusEditorStore = new ContingentCheckupStatusEditorStore(this);
        this.icd10Store = new ICD10Store();
    }
}

export default RootStore;