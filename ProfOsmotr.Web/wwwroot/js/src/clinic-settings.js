import Util from './util/common';
import { addValidation, resetValidation, validate, DefaultChecks } from './util/custom-validation';
import SuccessToast from './util/success-toast';


function initClinicSettings() {
    const URI_API_CLINIC_UPDATE_DETAILS = '/api/clinic/updateDetails';

    const successToast = new SuccessToast();

    const fullNameInput = document.getElementById('ClinicFullName');
    const shortNameInput = document.getElementById('ClinicShortName');
    const addressInput = document.getElementById('ClinicAddress');
    const phoneInput = document.getElementById('ClinicPhone');
    const emailInput = document.getElementById('ClinicEmail');

    addInputsValidation();

    const saveButton = document.getElementById('SaveClinicDetails');
    saveButton.addEventListener('click', onSaveClinicDetails);

    function addInputsValidation() {
        addValidation(fullNameInput, DefaultChecks.requiredText500);
        addValidation(shortNameInput, DefaultChecks.requiredText500);
        addValidation(addressInput, DefaultChecks.requiredText500);
        addValidation(phoneInput, DefaultChecks.phone);
        addValidation(emailInput, DefaultChecks.email);
    }

    async function onSaveClinicDetails(e) {
        e.preventDefault();
        saveButton.disabled = true;

        const inputs = document.querySelectorAll('input');
        validate(inputs);
        const form = document.ClinicDetails;
        if (!form.checkValidity()) {
            saveButton.disabled = false;
            return;
        }

        const formData = new FormData(form);
        const data = Object.fromEntries(formData);
        const result = await Util.postData(URI_API_CLINIC_UPDATE_DETAILS, data);
        if (result) {
            resetValidation(inputs);
            successToast.show();
        }
        saveButton.disabled = false;
    }
}

export default initClinicSettings;