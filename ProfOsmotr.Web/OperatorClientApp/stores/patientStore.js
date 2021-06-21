import { makeAutoObservable, runInAction } from "mobx";

import patientApiService from './../services/patientApiService';

class PatientStore {
    patientId = null; // slug

    lastName = '';
    firstName = '';
    patronymicName = '';
    dateOfBirth = '';
    gender = null;
    address = '';
    preliminaryMedicalExaminations = [];
    contingentCheckupStatuses = [];
    isLoading = true;

    constructor() {
        makeAutoObservable(this);
    }

    setPatientId = (patientId) => {
        this.patientId = patientId;
    }

    loadPatient = async (cancellationToken) => {
        if (!this.patientId) return;

        this.isLoading = true;
        const response = await patientApiService.getPatient(this.patientId);
        if (!cancellationToken.isCancelled) {
            if (response.success === false) throw response.message
            runInAction(() => {
                this.firstName = response.firstName;
                this.lastName = response.lastName;
                this.patronymicName = response.patronymicName;
                this.dateOfBirth = response.dateOfBirth;
                this.gender = response.gender;
                this.address = response.address;
                this.preliminaryMedicalExaminations = response.preliminaryMedicalExaminations;
                this.contingentCheckupStatuses = response.contingentCheckupStatuses;

                this.isLoading = false;
            });
        }
    }

    reset = () => {
        this.isLoading = true;
    }

    get hasExaminations() {
        return this.preliminaryMedicalExaminations.length > 0 && this.contingentCheckupStatuses.length > 0;
    }

    get fullName() {
        return `${this.lastName} ${this.firstName} ${this.patronymicName}`;
    }
}

export default PatientStore;