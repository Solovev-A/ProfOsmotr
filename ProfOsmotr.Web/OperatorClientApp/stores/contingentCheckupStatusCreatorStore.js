import { makeAutoObservable } from 'mobx';

import WorkPlaceEditorStore from './workplaceEditorStore';
import PatientAutocompleteStore from './patientAutocompleteStore';
import { errorToast, handleResponseWithToasts } from '../utils/toasts';
import periodicExaminationsApiService from '../services/periodicExaminationsApiService';
import ModalStore from './modalStore';
import { runInAction } from 'mobx';

// для createContingentCheckupStatusModal

class ContingentCheckupStatusCreatorStore {
    patient = null;

    constructor(rootStore) {
        this.rootStore = rootStore;

        const getEmployerId = () => {
            return this.rootStore.periodicExaminationsStore.examination.employer.id;
        }

        this.workPlace = new WorkPlaceEditorStore();
        this.patientAutocompleteStore = new PatientAutocompleteStore({ getEmployerId });
        this.patientEditorModal = new ModalStore();

        makeAutoObservable(this);
    }

    setPatient = (newPatient) => {
        this.patient = newPatient;
    }

    loadInitialValues = async () => {
        const employer = this.rootStore.periodicExaminationsStore.examination.employer;
        this.workPlace.setEmployer(employer);
        this.workPlace.isLoading = false;
    }

    clear = () => {
        this.patient = null;
        this.workPlace.clear();
        this.workPlace.resetEditorView();
        this.patientEditorModal = new ModalStore();
    }

    onSubmit = async () => {
        if (!this.patient) {
            errorToast('Выберите работника');
            return { success: false };
        }

        const examinationId = this.rootStore.periodicExaminationsStore.examinationSlug;
        const data = {
            patientId: this.patient.id
        }
        if (this.workPlace.employerDepartment) {
            data.employerDepartmentId = this.workPlace.employerDepartment.id;
        }
        if (this.workPlace.profession) {
            data.professionId = this.workPlace.profession.id;
        }

        const handler = () => periodicExaminationsApiService.createCheckupStatus(examinationId, data);
        // позаимствуем обработку отправления запроса
        const response = await this.workPlace.onSendingData(handler);

        if (response.success !== false) {
            runInAction(() => {
                // очистка формы
                this.patient = null;
                this.workPlace.employerDepartment = null;
                this.workPlace.profession = null;

                // == костыль для очистки полей автокомплитов в форме
                this.workPlace.isLoading = true;
                setTimeout(() => this.workPlace.isLoading = false, 10);
                // == удалить после исправления автокомплита

                this.workPlace.isProcessing = false;
            })
        }

        if (!response) return;
        return handleResponseWithToasts(response, true);
    }

    get isProcessing() {
        return this.workPlace.isProcessing;
    }

    get isLoading() {
        return this.workPlace.isLoading;
    }
}

export default ContingentCheckupStatusCreatorStore;