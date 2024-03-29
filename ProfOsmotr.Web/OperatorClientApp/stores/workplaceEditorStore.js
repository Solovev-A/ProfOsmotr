import { makeObservable, observable, action, runInAction } from 'mobx';

import BaseFormStore from './baseFormStore';
import { handleResponseWithToasts } from '../utils/toasts';
import SimpleFilterListStore from './simpleFilterListStore';
import employerApiService from '../services/employerApiService';
import ProfessionAutocompleteStore from './professionAutocompleteStore';
import ModalStore from './modalStore';

const workPlaceTemplate = {
    employerId: null,
    employerDepartmentId: null,
    professionId: null
}

class WorkPlaceEditorStore extends BaseFormStore {
    constructor(checkupEditorStore) {
        super(workPlaceTemplate);

        this.checkupEditorStore = checkupEditorStore;
        this.employerDepartmentsList = new SimpleFilterListStore();
        this.professionList = new ProfessionAutocompleteStore(this);
        this.employerEditorModalStore = new ModalStore();
        this.employerDepartmentEditorModalStore = new ModalStore();
        this.professionEditorModalStore = new ModalStore();

        this.resetEditorView();
        makeObservable(this, {
            employer: observable,
            employerDepartment: observable,
            profession: observable,
            loadInitialValues: action,
            onSubmit: action,
            setEmployer: action,
            setEmployerDepartment: action,
            setProfession: action,
            resetEditorView: action
        })
    }

    loadInitialValues = async () => {
        const checkup = await this.checkupEditorStore.loadCheckupStatus();

        runInAction(() => {
            this.employer = checkup.workPlace.employer;
            this._loadEmployerDepartmentsList();
            this.employerDepartment = checkup.workPlace.employer?.department;
            this.profession = checkup.workPlace.profession
        });

        this.setInitialValues({
            employerId: this.employer?.id,
            employerDepartmentId: this.employerDepartment?.id,
            professionId: this.profession?.id
        });
    }

    onSubmit = async () => {
        const handler = () => this.checkupEditorStore.onUpdate(this.patchedData);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }

    setEmployer = (newEmployer) => {
        this.employer = newEmployer;
        this.updateProperty('employerId', newEmployer?.id ?? null);

        this.setEmployerDepartment(null);
        this.employerDepartmentsList.reset();
        this._loadEmployerDepartmentsList();
    }

    setEmployerDepartment = (newEmployerDepartment) => {
        this.employerDepartment = newEmployerDepartment;
        this.updateProperty('employerDepartmentId', newEmployerDepartment?.id ?? null);
    }

    setProfession = (newProfession) => {
        this.profession = newProfession;
        this.updateProperty('professionId', newProfession?.id ?? null);
    }

    resetEditorView = () => {
        this.employer = null;
        this.employerDepartment = null;
        this.profession = null;
        this.employerDepartmentsList.reset();
    }

    _loadEmployerDepartmentsList = async () => {
        if (!this.employer) return;

        if (this.employer.departments) {
            this.employerDepartmentsList.setOptions(this.employer.departments);
            return;
        }

        const response = await employerApiService.getEntity(this.employer.id)
        if (response && response.success !== false) {
            this.employerDepartmentsList.setOptions(response.departments);
        }
        handleResponseWithToasts(response);
    }
}

export default WorkPlaceEditorStore;