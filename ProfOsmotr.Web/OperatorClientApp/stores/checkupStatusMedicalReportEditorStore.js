import { makeObservable, action, override, reaction } from 'mobx';

import BaseFormStore from './baseFormStore';
import { handleResponseWithToasts } from '../utils/toasts';
import { formatDateStringForDateInput } from '../utils/formatDate';
import NewlyDiagnosedDiseasesEditorStore from './newlyDiagnosedDiseasesEditorStore';

const emptyCheckupResultId = 'empty';

const medicalReportTemplate = {
    checkupResultId: emptyCheckupResultId,
    medicalReport: '',
    dateOfComplition: '',
    registrationJournalEntryNumber: '',
    checkupStarted: false,
    isDisabled: false,
    needExaminationAtOccupationalPathologyCenter: false,
    needOutpatientExamunationAndTreatment: false,
    needInpatientExamunationAndTreatment: false,
    needSpaTreatment: false,
    needDispensaryObservation: false
}


class CheckupStatusMedicalReportEditorStore extends BaseFormStore {
    constructor(checkupStatusEditorStore) {
        super(medicalReportTemplate);
        this.checkupStatusEditorStore = checkupStatusEditorStore;
        this.chronicSomaticDiseasesEditorStore = new NewlyDiagnosedDiseasesEditorStore();
        this.occupationalDiseasesEditorStore = new NewlyDiagnosedDiseasesEditorStore();

        makeObservable(this, {
            loadInitialValues: action,
            onSubmit: action,
            clear: override
        })

        reaction(() => this.model.checkupResultId,
            (value) => {
                if (value === emptyCheckupResultId) {
                    this.updateProperty('dateOfComplition', '');
                }
                else {
                    this.updateProperty('checkupStarted', true);
                }
            })
    }

    loadInitialValues = async () => {
        const checkup = await this.checkupStatusEditorStore.loadCheckupStatus();

        this.setInitialValues({
            checkupResultId: checkup.result?.id ?? emptyCheckupResultId,
            medicalReport: checkup.medicalReport ?? '',
            dateOfComplition: formatDateStringForDateInput(checkup.dateOfComplition),
            registrationJournalEntryNumber: checkup.registrationJournalEntryNumber ?? '',
            checkupStarted: checkup.checkupStarted,
            isDisabled: checkup.isDisabled,
            needExaminationAtOccupationalPathologyCenter: checkup.needExaminationAtOccupationalPathologyCenter,
            needOutpatientExamunationAndTreatment: checkup.needOutpatientExamunationAndTreatment,
            needInpatientExamunationAndTreatment: checkup.needInpatientExamunationAndTreatment,
            needSpaTreatment: checkup.needSpaTreatment,
            needDispensaryObservation: checkup.needDispensaryObservation,
        })

        this.chronicSomaticDiseasesEditorStore.setInitialValues(checkup.newlyDiagnosedChronicSomaticDiseases);
        this.occupationalDiseasesEditorStore.setInitialValues(checkup.newlyDiagnosedOccupationalDiseases);
    }

    clear() {
        super.clear();
        this.chronicSomaticDiseasesEditorStore?.clear();
        this.occupationalDiseasesEditorStore?.clear();
    }

    onSubmit = async () => {
        const data = this.patchedData;
        if (this.chronicSomaticDiseasesEditorStore.patchedData.length) {
            data.newlyDiagnosedChronicSomaticDiseases = this.chronicSomaticDiseasesEditorStore.patchedData;
        }
        if (this.occupationalDiseasesEditorStore.patchedData.length) {
            data.newlyDiagnosedOccupationalDiseases = this.occupationalDiseasesEditorStore.patchedData;
        }
        if (data.checkupResultId === emptyCheckupResultId) data.checkupResultId = null;

        const handler = () => this.checkupStatusEditorStore.onUpdate(data);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default CheckupStatusMedicalReportEditorStore;