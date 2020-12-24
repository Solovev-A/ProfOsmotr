function CustomValidation(input, validityCheck) {
    this.invalidityMessage;
    this.inputNode = input;
    this.validityCheck = validityCheck;

    this.registerListener();
}

function ValidityCheck(invalidityMessage, isInvalid) {
    this.invalidityMessage = invalidityMessage;
    this.isInvalid = isInvalid;
}

CustomValidation.prototype = {
    checkValidity: function (input) {
        let isInvalid = this.validityCheck.isInvalid(input);
        if (isInvalid) {
            this.invalidityMessage = this.validityCheck.invalidityMessage;
        }
        else {
            this.invalidityMessage = null;
        }
        this.rerenderValidity(input, isInvalid);
    },
    checkInput: function () {
        this.inputNode.CustomValidation.invalidityMessage = null;
        this.checkValidity(this.inputNode);

        if (this.inputNode.CustomValidation.invalidityMessage == null) {
            this.inputNode.setCustomValidity('');
        }
        else {
            var message = this.inputNode.CustomValidation.invalidityMessage;
            this.inputNode.setCustomValidity(message);
        }
    },
    registerListener: function () {
        var CustomValidation = this;
        this.inputNode.addEventListener('keyup', function () {
            CustomValidation.checkInput();
        });
    },
    rerenderValidity: function (input, isInvalid) {
        const VALID_CLASSNAME = 'is-valid';
        const INVALID_CLASSNAME = 'is-invalid';
        const MESSAGE_BLOCK_CLASSNAME = 'invalid-feedback';

        let nextNode = input.nextSibling;
        if (isInvalid) {
            input.classList.add(INVALID_CLASSNAME);
            input.classList.remove(VALID_CLASSNAME);
            if (!nextNode || !nextNode.classList || !nextNode.classList.contains(MESSAGE_BLOCK_CLASSNAME)) {
                let invalidityMessageBlock = document.createElement('div');
                invalidityMessageBlock.classList.add(MESSAGE_BLOCK_CLASSNAME);
                invalidityMessageBlock.innerText = this.invalidityMessage;
                input.parentNode.insertBefore(invalidityMessageBlock, nextNode);
            }
        }
        else {
            input.classList.add(VALID_CLASSNAME);
            input.classList.remove(INVALID_CLASSNAME);
            if (nextNode && nextNode.classList && nextNode.classList.contains(MESSAGE_BLOCK_CLASSNAME))
                nextNode.remove();
        }
    }
};

function validate(inputs) {
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].CustomValidation)
            inputs[i].CustomValidation.checkInput();
    }
}

function addValidation(input, validityCheck) {
    input.CustomValidation = new CustomValidation(input, validityCheck);
}

function resetValidation(inputs) {
    for (let input of inputs) {
        input.classList.remove('is-valid');
        input.classList.remove('is-invalid');
    }
}

const DefaultChecks = {
    phone: new ValidityCheck(
        'Должен соответствовать шаблону: +7 123 4567890',
        input => !input.value.match(/^\+7 \d{3} \d{7}$/)),

    email: new ValidityCheck(
        'Должно быть похоже на xxx@yyyy.zz',
        input => !input.value.match(/\S+@\S+\.\S+/) || input.value.length > 300),

    username: new ValidityCheck(
        'Может содержать только буквы латинского алфавита и цифры. От 3 до 20 символов',
        input => !input.value.match(/^[0-9A-Za-z]{3,20}$/)),

    password: new ValidityCheck(
        'Может содержать только буквы латинского алфавита, цифры и спецсимволы. От 8 до 20 символов',
        input => !input.value.match(/^[A-Za-z\d!@#$%^&*_+-?]{8,20}$/)),

    requiredText500: new ValidityCheck(
        'Обязательное. До 500 символов',
        input => !input.value.match(/^.{1,500}$/)),

    requiredText70: new ValidityCheck(
        'Обязательное. До 70 символов',
        input => !input.value.match(/^.{1,70}$/)),

    requiredText20: new ValidityCheck(
        'Обязательное. До 20 символов',
        input => !input.value.match(/^.{1,20}$/)),

    positiveInteger: new ValidityCheck(
        'Число должно быть целым и неотрицательным',
        input => input.value === '' || !Number.isInteger(+input.value) || +input.value < 0),

    price: new ValidityCheck(
        'Значение должно быть неотрицательным, целым либо с двумя знаками после точки',
        input => !input.value.match(/^\d+([,\.]\d\d)?$/))
}


export { CustomValidation, ValidityCheck, validate, addValidation, resetValidation, DefaultChecks };