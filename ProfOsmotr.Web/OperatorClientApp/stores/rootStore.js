import PatientEditorStore from './patientEditorStore';
import PatientStore from './patientStore';
import PatientsListStore from './patientsListStore';
import EmployerEditorStore from './employerEditorStore';
import EmployersStore from './employersStore';

class RootStore {
    constructor() {
        this.patientEditorStore = new PatientEditorStore();
        this.patientStore = new PatientStore();
        this.patientsListStore = new PatientsListStore();
        this.employerEditorStore = new EmployerEditorStore();
        this.employersStore = new EmployersStore();
    }
}

export default RootStore;