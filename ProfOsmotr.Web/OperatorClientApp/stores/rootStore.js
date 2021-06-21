import PatientEditorStore from './patientEditorStore';
import PatientStore from './patientStore';
import PatientsListStore from './patientsListStore';

class RootStore {
    constructor() {
        this.patientEditorStore = new PatientEditorStore();
        this.patientStore = new PatientStore();
        this.patientsListStore = new PatientsListStore();
    }
}

export default RootStore;