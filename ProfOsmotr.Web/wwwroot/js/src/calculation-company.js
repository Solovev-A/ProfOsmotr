import SimpleList from './util/simple-list';
import Util from './util/common';
import { addValidation, resetValidation, validate, ValidityCheck, DefaultChecks } from './util/custom-validation';
import { initOrderItemsSelect } from './util/profession-input';


async function initCalculationCompany() {
    const professionConstructor = document.professionConstructor;
    const inputsToValidateOnAddProfession = document.querySelectorAll('#Constructor input:not([type="submit"])');
    const inputsToValidateOnCreateCalculation = document.querySelectorAll('.js-validate');

    // Selectors
    const SELECTOR_ORDER_ITEMS_SELECT = '#OrderItems';
    const SELECTOR_PROFESSIONS_LIST = '#ProfessionsList';
    const SELECTOR_COMPANY_NAME_INPUT = '#CompanyName';
    const SELECTOR_ADD_PROFESSION_BUTTON = '#AddProfession';
    const SELECTOR_CALCULATE_BUTTON = '#CreateCompanyCalculation';

    const CLASSNAME_NUMBERS = 'js-number';

    // URI
    const URI_CREATE_CALCULATION_API = '/api/calculation/create';
    const URI_CALCULATION_RESULT_BASE = '/Calculation/Result/';

    const professionsList = new SimpleList({
        target: SELECTOR_PROFESSIONS_LIST,
        itemTemplate: '<div class="col-sm-8">{0}</div><div class="col-sm-4">{1} чел.</div>',
        reverse: true,
        data: [
            {
                source: () => professionConstructor.ProfessionName.value,
                path: 'name',
                render: (source) => Util.escapeHTML(source)
            },
            {
                source: () => parseInt(professionConstructor.NumberOfPersons.value),
                path: 'numberOfPersons'
            },
            {
                source: () => parseInt(professionConstructor.NumberOfWomen.value),
                path: 'numberOfWomen'
            },
            {
                source: () => parseInt(professionConstructor.NumberOfWomenOver40.value),
                path: 'numberOfWomenOver40'
            },
            {
                source: () => parseInt(professionConstructor.NumberOfPersonsOver40.value),
                path: 'numberOfPersonsOver40'
            },
            {
                source: () => {
                    return $(SELECTOR_ORDER_ITEMS_SELECT).select2('data')
                        .map((item) => parseInt(item.id));
                },
                path: 'orderItems'
            }
        ]
    })


    /* ------------------
     * Валидация форм
     * -----------------*/
    function startCompanyDataValidation() {
        const companyNameInput = document.querySelector(SELECTOR_COMPANY_NAME_INPUT);
        addValidation(companyNameInput, DefaultChecks.requiredText70);
    }

    function startConstructorValidation() {
        const professionNameInput = professionConstructor.ProfessionName;
        addValidation(professionNameInput, DefaultChecks.requiredText70);

        startValidateNumbers();
    }

    function startValidateNumbers() {
        const numberOfPersonsValidityCheck = new ValidityCheck(
            'Должна быть больше нуля и не меньше численности входящих подгрупп',
            input => isInvalidNumberOfPersons(input.value));
        addValidation(professionConstructor.NumberOfPersons, numberOfPersonsValidityCheck);

        const numberOfPersonsOver40ValidityCheck = new ValidityCheck(
            'Должна быть больше числа женщин старше 40 лет и меньше общей численности',
            input => isInvalidNumberOfPersonsOver40(input.value));
        addValidation(professionConstructor.NumberOfPersonsOver40, numberOfPersonsOver40ValidityCheck);

        const numberOfWomenValidityCheck = new ValidityCheck(
            'Должна быть больше числа женщин старше 40 лет и меньше общей численности',
            input => isInvalidNumberOfWomen(input.value));
        addValidation(professionConstructor.NumberOfWomen, numberOfWomenValidityCheck);

        const numberOfWomenOver40ValidityCheck = new ValidityCheck(
            'Должна быть меньше всех остальных групп, но не меньше нуля',
            input => isInvalidNumberOfWomenOver40(input.value));
        addValidation(professionConstructor.NumberOfWomenOver40, numberOfWomenOver40ValidityCheck);

        checkAllNumbersOnOneChange();
    }

    function isInvalidNumberOfPersons(value) {
        let number = +value;
        return !Number.isInteger(number)
            || number < 1
            || number < +professionConstructor.NumberOfPersonsOver40.value
            || number < +professionConstructor.NumberOfWomen.value
            || number < +professionConstructor.NumberOfWomenOver40.value;
    }

    function isInvalidNumberOfPersonsOver40(value) {
        let number = +value;
        return !Number.isInteger(number)
            || number < 0
            || number > +professionConstructor.NumberOfPersons.value
            || number < +professionConstructor.NumberOfWomenOver40.value;
    }

    function isInvalidNumberOfWomen(value) {
        let number = +value;
        return !Number.isInteger(number)
            || number < 0
            || number > +professionConstructor.NumberOfPersons.value
            || number < +professionConstructor.NumberOfWomenOver40.value;
    }

    function isInvalidNumberOfWomenOver40(value) {
        let number = +value;
        return !Number.isInteger(number)
            || number < 0
            || number > +professionConstructor.NumberOfPersons.value
            || number > +professionConstructor.NumberOfPersonsOver40.value
            || number > +professionConstructor.NumberOfWomen.value;
    }

    function checkAllNumbersOnOneChange() {
        const numberInputs = Array.from(document.getElementsByClassName(CLASSNAME_NUMBERS));
        numberInputs.forEach(value => value.addEventListener('keyup', (e) => validate(numberInputs)));
    }


    // Обработка событий

    function addEventListeners() {
        const addProfessionBtn = document.querySelector(SELECTOR_ADD_PROFESSION_BUTTON);
        addProfessionBtn.addEventListener('click', onAddProfession);

        const calculateBtn = document.querySelector(SELECTOR_CALCULATE_BUTTON);
        calculateBtn.addEventListener('click', onCalculate);
    }

    function onAddProfession(e) {
        e.preventDefault();
        validate(inputsToValidateOnAddProfession);
        if (!professionConstructor.checkValidity())
            return;
        professionsList.add();
        clearConstructor();
    }

    function clearConstructor() {
        professionConstructor.reset();
        $(SELECTOR_ORDER_ITEMS_SELECT).val(null).trigger('change');
        resetValidation(inputsToValidateOnAddProfession);
    }

    async function onCalculate(e) {
        e.preventDefault();

        validate(inputsToValidateOnCreateCalculation);
        if (!document.companyData.checkValidity())
            return;

        const nameElement = document.querySelector(SELECTOR_COMPANY_NAME_INPUT);

        let calculationSource = {
            name: nameElement.value,
            professions: professionsList.getData()
        };

        if (calculationSource.professions.length === 0) {
            alert('Добавьте хотя бы одну профессию');
            return;
        }

        let calculation = await Util.postData(URI_CREATE_CALCULATION_API, calculationSource);
        if (calculation) {
            location = URI_CALCULATION_RESULT_BASE + calculation.id;
        }
    }


    await initOrderItemsSelect();
    startCompanyDataValidation();
    startConstructorValidation();
    addEventListeners();
}

export default initCalculationCompany;