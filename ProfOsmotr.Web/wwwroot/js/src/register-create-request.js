import Util from './util/common';
import { addValidation, validate, DefaultChecks } from './util/custom-validation';


function initRegisterCreateRequest() {
    const URI_API_REGISTER_REQUEST_CREATE = '/api/clinic/addRegisterRequest';

    const candidateFullNameInput = document.getElementById('CandidateFullName');
    const candidateShortNameInput = document.getElementById('CandidateShortName');
    const candidateAddressInput = document.getElementById('CandidateAddress');
    const candidatePhoneInput = document.getElementById('CandidatePhone');
    const candidateEmailInput = document.getElementById('CandidateEmail');
    const candidateModeratorNameInput = document.getElementById('CandidateModeratorName');
    const candidateModeratorPositionInput = document.getElementById('CandidateModeratorPosition');
    const candidateModeratorUsernameInput = document.getElementById('CandidateModeratorUsername');
    const candidateModeratorPasswordInput = document.getElementById('CandidateModeratorPassword');

    const validatedRegisterRequestInputs = document.querySelectorAll('input');

    const submitRegisterRequestBtn = document.getElementById('SubmitRegisterRequest');
    submitRegisterRequestBtn.addEventListener('click', onSubmitRegisterRequest);

    addFormValidation();

    async function onSubmitRegisterRequest(e) {
        e.preventDefault();
        submitRegisterRequestBtn.disabled = true;

        validate(validatedRegisterRequestInputs);
        if (!document.RegisterRequest.checkValidity()) {
            submitRegisterRequestBtn.disabled = false;
            return;
        }

        let data = {
            fullName: candidateFullNameInput.value,
            shortName: candidateShortNameInput.value,
            address: candidateAddressInput.value,
            phone: candidatePhoneInput.value,
            email: candidateEmailInput.value,
            user: {
                name: candidateModeratorNameInput.value,
                position: candidateModeratorPositionInput.value,
                username: candidateModeratorUsernameInput.value,
                password: candidateModeratorPasswordInput.value
            }
        };

        let result = await Util.postData(URI_API_REGISTER_REQUEST_CREATE, data);

        if (result) {
            showSuccessMessage();
        }

        submitRegisterRequestBtn.disabled = false;
    }

    function addFormValidation() {
        addValidation(candidateFullNameInput, DefaultChecks.requiredText500);
        addValidation(candidateShortNameInput, DefaultChecks.requiredText500);
        addValidation(candidateAddressInput, DefaultChecks.requiredText500);
        addValidation(candidatePhoneInput, DefaultChecks.phone);
        addValidation(candidateEmailInput, DefaultChecks.email);
        addValidation(candidateModeratorNameInput, DefaultChecks.requiredText70);
        addValidation(candidateModeratorPositionInput, DefaultChecks.requiredText70);
        addValidation(candidateModeratorUsernameInput, DefaultChecks.username);
        addValidation(candidateModeratorPasswordInput, DefaultChecks.password);
    }

    function showSuccessMessage() {
        document.RegisterRequest.classList.add('d-none');
        document.querySelector('.alert-success').classList.remove('d-none');
    }
}


export default initRegisterCreateRequest;