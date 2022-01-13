/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./wwwroot/css/src/site.css":
/*!**********************************!*
  !*** ./wwwroot/css/src/site.css ***!
  \**********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
// extracted by mini-css-extract-plugin


/***/ }),

/***/ "./wwwroot/js/src/calculation-company.js":
/*!***********************************************!*
  !*** ./wwwroot/js/src/calculation-company.js ***!
  \***********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_simple_list__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/simple-list */ "./wwwroot/js/src/util/simple-list.js");
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
/* harmony import */ var _util_profession_input__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/profession-input */ "./wwwroot/js/src/util/profession-input.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿





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

    const professionsList = new _util_simple_list__WEBPACK_IMPORTED_MODULE_0__.default({
        target: SELECTOR_PROFESSIONS_LIST,
        itemTemplate: '<div class="col-sm-8">{0}</div><div class="col-sm-4">{1} чел.</div>',
        reverse: true,
        data: [
            {
                source: () => professionConstructor.ProfessionName.value,
                path: 'name',
                render: (source) => _util_common__WEBPACK_IMPORTED_MODULE_1__.default.escapeHTML(source)
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
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(companyNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.DefaultChecks.requiredText70);
    }

    function startConstructorValidation() {
        const professionNameInput = professionConstructor.ProfessionName;
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(professionNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.DefaultChecks.requiredText70);

        startValidateNumbers();
    }

    function startValidateNumbers() {
        const numberOfPersonsValidityCheck = new _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.ValidityCheck(
            'Должна быть больше нуля и не меньше численности входящих подгрупп',
            input => isInvalidNumberOfPersons(input.value));
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(professionConstructor.NumberOfPersons, numberOfPersonsValidityCheck);

        const numberOfPersonsOver40ValidityCheck = new _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.ValidityCheck(
            'Должна быть больше числа женщин старше 40 лет и меньше общей численности',
            input => isInvalidNumberOfPersonsOver40(input.value));
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(professionConstructor.NumberOfPersonsOver40, numberOfPersonsOver40ValidityCheck);

        const numberOfWomenValidityCheck = new _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.ValidityCheck(
            'Должна быть больше числа женщин старше 40 лет и меньше общей численности',
            input => isInvalidNumberOfWomen(input.value));
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(professionConstructor.NumberOfWomen, numberOfWomenValidityCheck);

        const numberOfWomenOver40ValidityCheck = new _util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.ValidityCheck(
            'Должна быть меньше всех остальных групп, но не меньше нуля',
            input => isInvalidNumberOfWomenOver40(input.value));
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(professionConstructor.NumberOfWomenOver40, numberOfWomenOver40ValidityCheck);

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
        numberInputs.forEach(value => value.addEventListener('keyup', (e) => (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.validate)(numberInputs)));
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
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.validate)(inputsToValidateOnAddProfession);
        if (!professionConstructor.checkValidity())
            return;
        professionsList.add();
        clearConstructor();
    }

    function clearConstructor() {
        professionConstructor.reset();
        $(SELECTOR_ORDER_ITEMS_SELECT).val(null).trigger('change');
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.resetValidation)(inputsToValidateOnAddProfession);
    }

    async function onCalculate(e) {
        e.preventDefault();

        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_2__.validate)(inputsToValidateOnCreateCalculation);
        if (!document.companyData.checkValidity())
            return;

        const nameElement = document.querySelector(SELECTOR_COMPANY_NAME_INPUT);

        let calculationSource = {
            name: nameElement.value,
            sources: professionsList.getData().map(source => ({
                profession: {
                    name: source.name,
                    orderItems: source.orderItems
                },
                numberOfPersons: source.numberOfPersons,
                numberOfWomen: source.numberOfWomen,
                numberOfPersonsOver40: source.numberOfPersonsOver40,
                numberOfWomenOver40: source.numberOfWomenOver40
            }))
        };

        if (calculationSource.sources.length === 0) {
            alert('Добавьте хотя бы одну профессию');
            return;
        }

        let calculation = await _util_common__WEBPACK_IMPORTED_MODULE_1__.default.postData(URI_CREATE_CALCULATION_API, calculationSource);
        if (calculation) {
            location = URI_CALCULATION_RESULT_BASE + calculation.id;
        }
    }


    await (0,_util_profession_input__WEBPACK_IMPORTED_MODULE_3__.initOrderItemsSelect)();
    startCompanyDataValidation();
    startConstructorValidation();
    addEventListeners();
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initCalculationCompany);

/***/ }),

/***/ "./wwwroot/js/src/calculation-edit.js":
/*!********************************************!*
  !*** ./wwwroot/js/src/calculation-edit.js ***!
  \********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
﻿



function initCalculationEdit() {
    const SELECTOR_RESULT_ITEM_BASE = '.js-result-';
    const NAME_RESULT_ITEM_PRICE = 'price';
    const NAME_RESULT_ITEM_AMOUNT = 'amount';
    const NAME_RESULT_ITEM_SUM = 'sum';
    const NAME_RESULT_ITEM_GROUP = 'group';

    const SELECTOR_CALCULATION_ID = '#CalculationId';
    const SELECTOR_RESULT_ITEM_ROW = '.form-group';
    const SELECTOR_TOTAL_SUM = '#TotalSum';
    const SELECTOR_SAVE_CALCULATION_BUTTON = '#SaveChanges';

    const URI_UPDATE_CALCULATION_API = '/api/calculation/update';
    const URI_CALCULATION_RESULT_BASE = '/Calculation/Result/';

    const priceInputs = document.querySelectorAll(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_PRICE);
    const amountInputs = document.querySelectorAll(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_AMOUNT);
    const sumElements = document.querySelectorAll(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_SUM);
    const groupSelects = document.querySelectorAll(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_GROUP);

    const AVAILABLE_SERVICE_OPTION_INDEX = 0;
    const RESULT_ITEM_ID_PREFIX = 'r-';

    processPriceInputs();
    processAmountInputs();
    processGroupSelects();
    calculateTotalSum();
    addSaveBtnEventListener();

    function processPriceInputs() {
        let arr = Array.from(priceInputs);
        arr.forEach(input => {
            input.addEventListener('keyup', onPriceChanged);
            (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(input, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.price);
        });
    }

    function processAmountInputs() {
        let arr = Array.from(amountInputs);
        arr.forEach(input => {
            input.addEventListener('keyup', onAmountChanged);
            (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(input, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.positiveInteger);
        });
    }

    function onPriceChanged(e) {
        let input = e.target;
        if (isPriceInputInvalid(input))
            return;
        let resultId = getResultId(input);

        let amountInput = getAmountInput(resultId);
        if (isAmountInputInvalid(amountInput))
            return;

        let sum = getSumElement(resultId);
        sum.value = _util_common__WEBPACK_IMPORTED_MODULE_0__.default.toCurrencyString(calculateSum(input, amountInput));

        calculateTotalSum();
    }

    function onAmountChanged(e) {
        let input = e.target;
        if (isAmountInputInvalid(input))
            return;
        let resultId = getResultId(input);

        let priceInput = getPriceInput(resultId);
        if (isPriceInputInvalid(priceInput))
            return;

        let sum = getSumElement(resultId);
        sum.value = _util_common__WEBPACK_IMPORTED_MODULE_0__.default.toCurrencyString(calculateSum(priceInput, input));

        calculateTotalSum();
    }

    function calculateSum(priceInput, amountInput) {
        return _util_common__WEBPACK_IMPORTED_MODULE_0__.default.customParseFloat(priceInput.value) * amountInput.value;
    }

    function getResultId(formInput) {
        return formInput.parentNode.parentNode.id;
    }

    function isAmountInputInvalid(input) {
        return _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.positiveInteger.isInvalid(input);
    }

    function isPriceInputInvalid(input) {
        return _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.price.isInvalid(input);
    }

    function processGroupSelects() {
        var arr = Array.from(groupSelects);
        arr.forEach(item => {
            item.addEventListener('change', onGroupChange);
        })
    }

    function onGroupChange(e) {
        const select = e.target;
        const resultId = getResultId(select);

        const priceInput = getPriceInput(resultId);
        const amountInput = getAmountInput(resultId);
        const sumInput = getSumElement(resultId);

        checkInputsValidityOnSelectChange(priceInput, amountInput);

        let inputs = [priceInput, amountInput, sumInput];

        if (select.selectedIndex === AVAILABLE_SERVICE_OPTION_INDEX) {
            showInputs(inputs);
        }
        else {
            hideInputs(inputs);
        }

        calculateTotalSum();
    }

    function checkInputsValidityOnSelectChange(priceInput, amountInput) {
        if (isPriceInputInvalid(priceInput)) {
            priceInput.value = '0';
        }

        if (isAmountInputInvalid(amountInput)) {
            amountInput.value = '0';
        }

        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.validate)([priceInput, amountInput]);
    }

    function hideInputs(inputs) {
        inputs.forEach(input => input.type = 'hidden');
    }

    function showInputs(inputs) {
        inputs.forEach(input => input.type = 'text');
    }

    function getPriceInput(resultId) {
        return getInput(resultId, NAME_RESULT_ITEM_PRICE);
    }

    function getAmountInput(resultId) {
        return getInput(resultId, NAME_RESULT_ITEM_AMOUNT);
    }

    function getSumElement(resultId) {
        return getInput(resultId, NAME_RESULT_ITEM_SUM);
    }

    function getInput(resultId, name) {
        let input = document.querySelector('#' + resultId + ' ' + SELECTOR_RESULT_ITEM_BASE + name);
        if (!input) {
            console.log('Не найдено поле ' + name);
            return null;
        }
        return input;
    }

    async function onSaveChanges(e) {
        e.preventDefault();
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.validate)(priceInputs);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.validate)(amountInputs);
        if (!document.Results.checkValidity())
            return;

        let data = createUpdateCalculationRequestData();

        const calculation = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_UPDATE_CALCULATION_API, data);
        if (calculation) {
            location = URI_CALCULATION_RESULT_BASE + calculation.id;
        }
    }

    function createUpdateCalculationRequestData() {
        const calculationId = parseInt(document.querySelector(SELECTOR_CALCULATION_ID).value);

        let resultItems = [];

        let rows = document.querySelectorAll(SELECTOR_RESULT_ITEM_ROW);
        for (var i = 0; i < rows.length; i++) {
            let item = getResultItem(rows[i]);
            resultItems.push(item);
        }

        return {
            calculationId: calculationId,
            resultItems: resultItems
        }
    }

    function getResultItem(formGroup) {
        let id = parseInt(formGroup.id.slice(RESULT_ITEM_ID_PREFIX.length));

        let price = _util_common__WEBPACK_IMPORTED_MODULE_0__.default.customParseFloat(formGroup.querySelector(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_PRICE).value);
        let amount = +formGroup.querySelector(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_AMOUNT).value;
        let groupId = +formGroup.querySelector(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_GROUP).value;

        return {
            id: id,
            price: price,
            amount: amount,
            groupId: groupId
        }
    }

    function calculateTotalSum() {
        let totalSum = 0;
        for (var i = 0; i < sumElements.length; i++) {
            if (sumElements[i].type === 'text') {
                totalSum += _util_common__WEBPACK_IMPORTED_MODULE_0__.default.customParseFloat(sumElements[i].value);
            }
        }
        document.querySelector(SELECTOR_TOTAL_SUM).innerText = _util_common__WEBPACK_IMPORTED_MODULE_0__.default.toCurrencyString(totalSum);
    }

    function addSaveBtnEventListener() {
        const saveBtn = document.querySelector(SELECTOR_SAVE_CALCULATION_BUTTON);
        saveBtn.addEventListener('click', onSaveChanges);
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initCalculationEdit);

/***/ }),

/***/ "./wwwroot/js/src/calculation-single.js":
/*!**********************************************!*
  !*** ./wwwroot/js/src/calculation-single.js ***!
  \**********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
/* harmony import */ var _util_profession_input__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/profession-input */ "./wwwroot/js/src/util/profession-input.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿




async function initCalculationSingle() {
    const SELECTOR_ORDER_ITEMS_SELECT = '#OrderItems';
    const SELECTOR_CREATE_CALCULATION_BUTTON = '#createSingleCalculation';
    const SELECTOR_PROFESSION_NAME_INPUT = '#ProfessionName';
    const SELECTOR_IS_WOMAN_CHECKBOX = '#IsWoman';
    const SELECTOR_IS_OVER_40_CHECKBOX = '#IsOver40';

    const URI_CREATE_CALCULATION_API = '/api/calculation/create';
    const URI_CALCULATION_RESULT_BASE = '/Calculation/Result/';

    const professionNameInput = document.querySelector(SELECTOR_PROFESSION_NAME_INPUT);

    await (0,_util_profession_input__WEBPACK_IMPORTED_MODULE_2__.initOrderItemsSelect)();
    addFormValidation();
    addEventListeners();

    function addFormValidation() {
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(professionNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText70);
    }

    function addEventListeners() {
        var submitBtn = document.querySelector(SELECTOR_CREATE_CALCULATION_BUTTON);
        submitBtn.addEventListener('click', onSubmit);
    }

    async function onSubmit(e) {
        e.preventDefault();

        const inputs = [professionNameInput];
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.validate)(inputs);
        if (!document.singleCalc.checkValidity())
            return;

        const calculationSource = getCalculationSource();

        const calculation = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_CREATE_CALCULATION_API, calculationSource);
        if (calculation) {
            location = URI_CALCULATION_RESULT_BASE + calculation.id;
        }
    }

    function getCalculationSource() {
        let professionName = professionNameInput.value;

        let orderItems = $(SELECTOR_ORDER_ITEMS_SELECT).select2('data')
            .map((item) => parseInt(item.id));
        if (orderItems.length == 0) {
            alert('Выберите хотя бы один пункт приказа');
            return;
        }

        let isWoman = document.querySelector(SELECTOR_IS_WOMAN_CHECKBOX).checked;
        let womenCount = isWoman ? 1 : 0;

        let isOver40 = document.querySelector(SELECTOR_IS_OVER_40_CHECKBOX).checked;
        let over40Count = isOver40 ? 1 : 0;

        let womenOver40Count = isWoman && isOver40 ? 1 : 0;

        return {
            name: 'Индивидуальный расчет',
            sources: [{
                profession: {
                    name: professionName,
                    orderItems: orderItems
                },
                numberOfPersons: 1,
                numberOfWomen: womenCount,
                numberOfWomenOver40: womenOver40Count,
                numberOfPersonsOver40: over40Count
            }]
        };
    }
}


/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initCalculationSingle);

/***/ }),

/***/ "./wwwroot/js/src/catalog-list.js":
/*!****************************************!*
  !*** ./wwwroot/js/src/catalog-list.js ***!
  \****************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* harmony import */ var _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
﻿






function initCatalogList() {
    const URI_API_CATALOG_UPDATE = '/api/catalog/update';

    const SELECTOR_EXAMINATION_NAME = '.js-catalog-examination';
    const SELECTOR_SERVICE_CODE = '.js-catalog-code';
    const SELECTOR_SERVICE_FULL_NAME = '.js-catalog-name';
    const SELECTOR_SERVICE_PRICE = '.js-catalog-price';
    const SELECTOR_SERVICE_GROUP = '.js-catalog-group';
    const SELECTOR_CATALOG_UPDATE_TIME = '.js-catalog-update-time';

    const ID_TABLE_CATALOG = 'Catalog';

    const ATTR_DATA_EXAMINATION_ID = 'data-examination-id';

    const successToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_3__.default();

    const modalOptions = {
        title: (model) => `Изменение услуги для обследования: ${model.name}`,
        data: [
            {
                id: 'service-code',
                path: 'code',
                label: 'Код услуги',
                type: 'input-text',
                validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText20
            },
            {
                id: 'service-name',
                path: 'fullName',
                label: 'Наименование услуги',
                type: 'textarea',
                validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText500
            },
            {
                id: 'service-price',
                path: 'price',
                label: 'Цена',
                type: 'input-text',
                validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.price
            },
            {
                id: 'service-availability-group',
                path: 'serviceAvailabilityGroupId',
                label: 'Доступность',
                type: 'select',
                options: [
                    new Option('Доступна', '1'),
                    new Option('Недоступна', '2'),
                    new Option('Включена', '3')
                ]
            }
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: onUpdateCatalogItem
            }
        ]
    };
    const serviceModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(modalOptions);


    const dataTableConfig = {
        tableId: ID_TABLE_CATALOG,
        advanced: {
            buttons: [
                {
                    extend: 'selectedSingle',
                    text: 'Редактировать',
                    action: function (e, dt, button, config) {
                        let tableRow = dt.row({ selected: true }).node();
                        let model = getModel(tableRow);
                        serviceModal.show(model);
                    }
                }
            ]
        }
    };
    const table = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(dataTableConfig);


    function getModel(tableRow) {
        return {
            id: +tableRow.dataset.examinationId,
            name: tableRow.querySelector(SELECTOR_EXAMINATION_NAME).innerText,
            code: tableRow.querySelector(SELECTOR_SERVICE_CODE).innerText,
            fullName: tableRow.querySelector(SELECTOR_SERVICE_FULL_NAME).innerText,
            price: tableRow.querySelector(SELECTOR_SERVICE_PRICE).innerText,
            serviceAvailabilityGroupId: tableRow.querySelector(SELECTOR_SERVICE_GROUP).dataset.groupId
        };
    }

    function renderCatalogChanges(updated) {
        const tableRow = document.querySelector(`tr[${ATTR_DATA_EXAMINATION_ID}="${updated.orderExaminationId}"]`);

        tableRow.querySelector(SELECTOR_SERVICE_CODE).innerText = updated.code;
        tableRow.querySelector(SELECTOR_SERVICE_FULL_NAME).innerText = updated.fullName;
        tableRow.querySelector(SELECTOR_SERVICE_PRICE).innerText = _util_common__WEBPACK_IMPORTED_MODULE_0__.default.toCurrencyString(updated.price);
        tableRow.querySelector(SELECTOR_CATALOG_UPDATE_TIME).innerText = new Date(updated.updateTime).toLocaleString();

        const groupCell = tableRow.querySelector(SELECTOR_SERVICE_GROUP);
        groupCell.innerText = updated.serviceAvailabilityGroupName;
        groupCell.dataset.groupId = updated.serviceAvailabilityGroupId;
    }

    async function onUpdateCatalogItem(model) {
        const requestData = {
            orderExaminationId: model.id,
            fullName: model.fullName,
            code: model.code,
            price: _util_common__WEBPACK_IMPORTED_MODULE_0__.default.customParseFloat(model.price),
            serviceAvailabilityGroupId: +model.serviceAvailabilityGroupId
        }

        const service = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_CATALOG_UPDATE, requestData);
        if (service) {
            renderCatalogChanges(service);
            serviceModal.hide();
            successToast.show();
        }
        else {
            serviceModal.enableButtons();
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initCatalogList);

/***/ }),

/***/ "./wwwroot/js/src/clinic-list.js":
/*!***************************************!*
  !*** ./wwwroot/js/src/clinic-list.js ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* harmony import */ var _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
﻿





function initClinicList() {
    const ID_DATATABLE_CLINIC_LIST = 'ClinicsList';

    const URI_API_DATATABLE_CLINIC_LIST = '/api/clinic/list';
    const URI_API_CLINIC_MANAGE = '/api/clinic/manageClinic';

    const successToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_3__.default();

    const currentUserId = document.getElementById(ID_DATATABLE_CLINIC_LIST).dataset.clinic;

    // -----
    // Modal
    // -----
    const modalOptions = {
        title: 'Информация о медицинской организации',
        readonly: true,
        data: [
            {
                id: 'full-name',
                path: 'clinicDetails.fullName',
                type: 'input-text',
                label: 'Полное наименование'
            },
            {
                id: 'short-name',
                path: 'clinicDetails.shortName',
                type: 'input-text',
                label: 'Сокращенное наименование'
            },
            {
                id: 'address',
                path: 'clinicDetails.address',
                type: 'input-text',
                label: 'Адрес'
            },
            {
                id: 'phone',
                path: 'clinicDetails.phone',
                type: 'input-text',
                label: 'Телефон'
            },
            {
                id: 'email',
                path: 'clinicDetails.email',
                type: 'input-text',
                label: 'Электронная почта'
            },
            {
                id: 'block-status',
                path: 'isBlocked',
                type: 'input-text',
                label: 'Статус',
                render: (value) => value === true ? 'Заблокирована' : 'Активна'
            },
        ],
        buttons: [
            {
                text: 'Заблокировать',
                className: 'btn btn-danger',
                action: async (model) => await onManageClinic(model, true),
                visibility: (model) => !model.isBlocked && model.id != currentUserId
            },
            {
                text: 'Разблокировать',
                className: 'btn btn-success',
                action: async (model) => await onManageClinic(model, false),
                visibility: (model) => model.isBlocked && model.id != currentUserId
            }
        ]
    };
    const modal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(modalOptions);


    // ---------
    // Datatable
    // ---------
    const tableOptions = {
        tableId: ID_DATATABLE_CLINIC_LIST,
        ajaxUrl: URI_API_DATATABLE_CLINIC_LIST,
        serverSide: true,
        scroll: true,
        advanced: {
            buttons: [
                {
                    extend: 'selectedSingle',
                    text: 'Просмотр',
                    action: (e, dt, button, config) => {
                        let model = dt.row({ selected: true }).data();
                        modal.show(model);
                    }
                }
            ],
            columns: [
                {
                    data: 'id',
                    name: 'Id',
                    searchable: false
                },
                {
                    data: 'clinicDetails.shortName',
                    name: 'ClinicDetails.ShortName',
                    render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                },
                {
                    data: 'clinicDetails.phone',
                    name: 'ClinicDetails.Phone',
                    render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data),
                    orderable: false
                },
                {
                    data: 'clinicDetails.email',
                    name: 'ClinicDetails.Email',
                    render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data),
                    orderable: false
                },
                {
                    data: 'isBlocked',
                    name: 'IsBlocked',
                    render: (data, type, row) => data ? "Заблокирована" : "Активна",
                    searchable: false,
                    orderable: false
                }
            ]
        }

    };
    const table = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(tableOptions);

    async function onManageClinic(model, needBlock) {
        const data = {
            id: model.id,
            needBlock
        };
        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_CLINIC_MANAGE, data);

        if (result) {
            modal.hide();
            table.ajaxReload();
            successToast.show();
        }
        else {
            modal.enableButtons();
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initClinicList);

/***/ }),

/***/ "./wwwroot/js/src/clinic-settings.js":
/*!*******************************************!*
  !*** ./wwwroot/js/src/clinic-settings.js ***!
  \*******************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
﻿




function initClinicSettings() {
    const URI_API_CLINIC_UPDATE_DETAILS = '/api/clinic/updateDetails';

    const successToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_2__.default();

    const fullNameInput = document.getElementById('ClinicFullName');
    const shortNameInput = document.getElementById('ClinicShortName');
    const addressInput = document.getElementById('ClinicAddress');
    const phoneInput = document.getElementById('ClinicPhone');
    const emailInput = document.getElementById('ClinicEmail');

    addInputsValidation();

    const saveButton = document.getElementById('SaveClinicDetails');
    saveButton.addEventListener('click', onSaveClinicDetails);

    function addInputsValidation() {
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(fullNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText500);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(shortNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText500);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(addressInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText500);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(phoneInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.phone);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(emailInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.email);
    }

    async function onSaveClinicDetails(e) {
        e.preventDefault();
        saveButton.disabled = true;

        const inputs = document.querySelectorAll('input');
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.validate)(inputs);
        const form = document.ClinicDetails;
        if (!form.checkValidity()) {
            saveButton.disabled = false;
            return;
        }

        const formData = new FormData(form);
        const data = Object.fromEntries(formData);
        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_CLINIC_UPDATE_DETAILS, data);
        if (result) {
            (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.resetValidation)(inputs);
            successToast.show();
        }
        saveButton.disabled = false;
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initClinicSettings);

/***/ }),

/***/ "./wwwroot/js/src/main.js":
/*!********************************!*
  !*** ./wwwroot/js/src/main.js ***!
  \********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(jquery__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var bootstrap__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! bootstrap */ "./node_modules/bootstrap/dist/js/bootstrap.js");
/* harmony import */ var bootstrap__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(bootstrap__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var jquery_mask_plugin__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! jquery-mask-plugin */ "./node_modules/jquery-mask-plugin/dist/jquery.mask.js");
/* harmony import */ var jquery_mask_plugin__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(jquery_mask_plugin__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var select2__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! select2 */ "./node_modules/select2/dist/js/select2.js");
/* harmony import */ var select2__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(select2__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var datatables_net_bs4__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! datatables.net-bs4 */ "./node_modules/datatables.net-bs4/js/dataTables.bootstrap4.js");
/* harmony import */ var datatables_net_bs4__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(datatables_net_bs4__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var datatables_net_buttons_bs4__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! datatables.net-buttons-bs4 */ "./node_modules/datatables.net-buttons-bs4/js/buttons.bootstrap4.js");
/* harmony import */ var datatables_net_buttons_bs4__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(datatables_net_buttons_bs4__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var datatables_net_fixedheader_bs4__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! datatables.net-fixedheader-bs4 */ "./node_modules/datatables.net-fixedheader-bs4/js/fixedHeader.bootstrap4.js");
/* harmony import */ var datatables_net_fixedheader_bs4__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(datatables_net_fixedheader_bs4__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var datatables_net_rowgroup_bs4__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! datatables.net-rowgroup-bs4 */ "./node_modules/datatables.net-rowgroup-bs4/js/rowGroup.bootstrap4.js");
/* harmony import */ var datatables_net_rowgroup_bs4__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(datatables_net_rowgroup_bs4__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var datatables_net_scroller_bs4__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! datatables.net-scroller-bs4 */ "./node_modules/datatables.net-scroller-bs4/js/scroller.bootstrap4.js");
/* harmony import */ var datatables_net_scroller_bs4__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(datatables_net_scroller_bs4__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var datatables_net_select_bs4__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! datatables.net-select-bs4 */ "./node_modules/datatables.net-select-bs4/js/select.bootstrap4.js");
/* harmony import */ var datatables_net_select_bs4__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(datatables_net_select_bs4__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _util_script_manager__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./util/script-manager */ "./wwwroot/js/src/util/script-manager.js");
/* harmony import */ var _calculation_company__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./calculation-company */ "./wwwroot/js/src/calculation-company.js");
/* harmony import */ var _calculation_edit__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./calculation-edit */ "./wwwroot/js/src/calculation-edit.js");
/* harmony import */ var _calculation_single__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./calculation-single */ "./wwwroot/js/src/calculation-single.js");
/* harmony import */ var _catalog_list__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./catalog-list */ "./wwwroot/js/src/catalog-list.js");
/* harmony import */ var _clinic_list__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./clinic-list */ "./wwwroot/js/src/clinic-list.js");
/* harmony import */ var _clinic_settings__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./clinic-settings */ "./wwwroot/js/src/clinic-settings.js");
/* harmony import */ var _order_examinations__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./order-examinations */ "./wwwroot/js/src/order-examinations.js");
/* harmony import */ var _order_items__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./order-items */ "./wwwroot/js/src/order-items.js");
/* harmony import */ var _register_create_request__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./register-create-request */ "./wwwroot/js/src/register-create-request.js");
/* harmony import */ var _register_requests__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ./register-requests */ "./wwwroot/js/src/register-requests.js");
/* harmony import */ var _user_list__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./user-list */ "./wwwroot/js/src/user-list.js");
/* harmony import */ var _user_login__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ./user-login */ "./wwwroot/js/src/user-login.js");
/* harmony import */ var bootstrap_dist_css_bootstrap_min_css__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! bootstrap/dist/css/bootstrap.min.css */ "./node_modules/bootstrap/dist/css/bootstrap.min.css");
/* harmony import */ var select2_dist_css_select2_min_css__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! select2/dist/css/select2.min.css */ "./node_modules/select2/dist/css/select2.min.css");
/* harmony import */ var _ttskch_select2_bootstrap4_theme_dist_select2_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! @ttskch/select2-bootstrap4-theme/dist/select2-bootstrap4.min.css */ "./node_modules/@ttskch/select2-bootstrap4-theme/dist/select2-bootstrap4.min.css");
/* harmony import */ var datatables_net_bs4_css_dataTables_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! datatables.net-bs4/css/dataTables.bootstrap4.min.css */ "./node_modules/datatables.net-bs4/css/dataTables.bootstrap4.min.css");
/* harmony import */ var datatables_net_fixedheader_bs4_css_fixedHeader_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_27__ = __webpack_require__(/*! datatables.net-fixedheader-bs4/css/fixedHeader.bootstrap4.min.css */ "./node_modules/datatables.net-fixedheader-bs4/css/fixedHeader.bootstrap4.min.css");
/* harmony import */ var datatables_net_buttons_bs4_css_buttons_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_28__ = __webpack_require__(/*! datatables.net-buttons-bs4/css/buttons.bootstrap4.min.css */ "./node_modules/datatables.net-buttons-bs4/css/buttons.bootstrap4.min.css");
/* harmony import */ var datatables_net_rowgroup_bs4_css_rowGroup_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_29__ = __webpack_require__(/*! datatables.net-rowgroup-bs4/css/rowGroup.bootstrap4.min.css */ "./node_modules/datatables.net-rowgroup-bs4/css/rowGroup.bootstrap4.min.css");
/* harmony import */ var datatables_net_scroller_bs4_css_scroller_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_30__ = __webpack_require__(/*! datatables.net-scroller-bs4/css/scroller.bootstrap4.min.css */ "./node_modules/datatables.net-scroller-bs4/css/scroller.bootstrap4.min.css");
/* harmony import */ var datatables_net_select_bs4_css_select_bootstrap4_min_css__WEBPACK_IMPORTED_MODULE_31__ = __webpack_require__(/*! datatables.net-select-bs4/css/select.bootstrap4.min.css */ "./node_modules/datatables.net-select-bs4/css/select.bootstrap4.min.css");
/* harmony import */ var _css_src_site_css__WEBPACK_IMPORTED_MODULE_32__ = __webpack_require__(/*! ../../css/src/site.css */ "./wwwroot/css/src/site.css");
﻿// Vendor











// Internal














// Styles












const namespace = {
    'calculation-company': _calculation_company__WEBPACK_IMPORTED_MODULE_11__.default,
    'calculation-edit': _calculation_edit__WEBPACK_IMPORTED_MODULE_12__.default,
    'calculation-single': _calculation_single__WEBPACK_IMPORTED_MODULE_13__.default,
    'catalog-list': _catalog_list__WEBPACK_IMPORTED_MODULE_14__.default,
    'clinic-list': _clinic_list__WEBPACK_IMPORTED_MODULE_15__.default,
    'clinic-settings': _clinic_settings__WEBPACK_IMPORTED_MODULE_16__.default,
    'order-examinations': _order_examinations__WEBPACK_IMPORTED_MODULE_17__.default.init,
    'order-items': _order_items__WEBPACK_IMPORTED_MODULE_18__.default.init,
    'register-create-request': _register_create_request__WEBPACK_IMPORTED_MODULE_19__.default,
    'register-requests': () => new _register_requests__WEBPACK_IMPORTED_MODULE_20__.default(),
    'user-list': () => (0,_user_list__WEBPACK_IMPORTED_MODULE_21__.default)(),
    'user-login': () => (0,_user_login__WEBPACK_IMPORTED_MODULE_22__.default)()
};

const scriptManager = new _util_script_manager__WEBPACK_IMPORTED_MODULE_10__.default(namespace);
document.addEventListener('DOMContentLoaded', event => scriptManager.load());

/***/ }),

/***/ "./wwwroot/js/src/order-examination-indexes.js":
/*!*****************************************************!*
  !*** ./wwwroot/js/src/order-examination-indexes.js ***!
  \*****************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
/* harmony import */ var _util_modal_modal_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/modal/modal-datatable */ "./wwwroot/js/src/util/modal/modal-datatable.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿





const URI_API_ORDER_INDEXES = '/api/order/getIndexes';
const URI_API_INDEX_CREATE = '/api/order/examination/{0}/index'; // POST
const URI_API_INDEX_DELETE = '/api/order/index'; // DELETE with /{id}
const URI_API_INDEX_UPDATE = '/api/order/index'; // POST with /{id}


class OrderExaminationIndexes {
    constructor(successToast) {
        this._indexModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_3__.default(this._getIndexConfig());
        this.mainModal = new _util_modal_modal_datatable__WEBPACK_IMPORTED_MODULE_2__.default(this._getMainConfig());
        this.successToast = successToast;
        this._fixMultipleModalsOverlay();
    }


    // Public

    show(examinationdId, examinationName) {
        const model = {
            sourceURL: `${URI_API_ORDER_INDEXES}/${examinationdId}`,
            examination: examinationName
        };
        this.mainModal.show(model);
        this.examinationdId = examinationdId;
    }


    // Private

    _fixMultipleModalsOverlay() {
        // Решение позаимствовано. Источник: https://stackoverflow.com/a/24914782/13467303

        $(document).on('show.bs.modal', '.modal', function () {
            const zIndex = 1040 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);
            setTimeout(function () {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);
        });
    }

    _getMainConfig() {
        const config = {
            title: (model) => `Показатели обследования: ${model.examination}`,
            columns: [
                {
                    title: 'Название'
                },
                {
                    title: 'Единицы измерения'
                },
            ],
            dataTableOptions: {
                advanced: {
                    columns: [
                        {
                            data: 'title',
                            render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                        },
                        {
                            data: 'unitOfMeasure',
                            render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                        }
                    ],
                    buttons: [
                        {
                            text: 'Добавить',
                            action: (e, dt, button, config) => this._indexModal.show()
                        },
                        {
                            text: 'Редактировать',
                            action: (e, dt, button, config) => {
                                let model = dt.row({ selected: true }).data();
                                model.editing = true;
                                this._indexModal.show(model);
                            },
                            extend: 'selectedSingle'
                        },
                        {
                            text: 'Удалить',
                            action: (e, dt, button, config) => {
                                let model = dt.row({ selected: true }).data();
                                this._removeIndex(model.id);
                            },
                            extend: 'selectedSingle'
                        }
                    ]
                }
            }
        };

        return config;
    }

    _getIndexConfig() {
        const config = {
            title: model => model.editing ? 'Редактирование показателя' : 'Добавление показателя',
            data: [
                {
                    id: 'title',
                    path: 'title',
                    label: 'Название',
                    type: 'input-text',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText70
                },
                {
                    id: 'unitOfMeasure',
                    path: 'unitOfMeasure',
                    label: 'Единицы измерения',
                    type: 'input-text',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText70
                }
            ],
            buttons: [
                {
                    text: 'Сохранить',
                    action: this._onIndexSave.bind(this)
                }
            ]
        };
        return config;
    }

    async _removeIndex(id) {
        const response = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.deleteResource(`${URI_API_INDEX_DELETE}/${id}`);
        if (response) {
            this.mainModal.seedTable();
        }
    }

    async _onIndexSave(model) {
        const url = model.editing ? `${URI_API_INDEX_UPDATE}/${model.id}` : getIndexCreateURL(this.examinationdId);
        const response = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(url, model);

        if (response) {
            this.successToast.show();
            this.mainModal.seedTable();
            this._indexModal.hide();
        }
        else {
            this._indexModal.enableButtons();
        }

        function getIndexCreateURL(examinationId) {
            return URI_API_INDEX_CREATE.replace('{0}', examinationId);
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (OrderExaminationIndexes);

/***/ }),

/***/ "./wwwroot/js/src/order-examinations.js":
/*!**********************************************!*
  !*** ./wwwroot/js/src/order-examinations.js ***!
  \**********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* harmony import */ var _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
/* harmony import */ var _order_examination_indexes__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./order-examination-indexes */ "./wwwroot/js/src/order-examination-indexes.js");
﻿







const URI_API_ORDER_EXAMINATIONS_DATA = '/api/order/getExaminations';
const URI_API_ORDER_EXAMINATION_CREATE = '/api/order/addExamination';
const URI_API_ORDER_EXAMINATION_UPDATE = '/api/order/updateExamination';

class OrderExaminationsPage {
    constructor() {
        this.succesToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_3__.default();
    }


    // Static

    static init() {
        return (async () => {
            const _this = new OrderExaminationsPage();

            await _this._getExaminationsData();

            _this._createDataTable();
            _this._createExaminationModal();
            _this._createExaminationIndexesModal();
        })();
    }


    // Private

    async _getExaminationsData() {
        this._examinationsData = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.getData(URI_API_ORDER_EXAMINATIONS_DATA);
    }

    _createDataTable() {
        const config = {
            tableId: 'Examinations',
            advanced: {
                data: this._getTableData(),
                ordering: false,
                columns: [
                    {
                        data: 'name',
                        render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                    },
                    {
                        data: 'targetGroup.name',
                        render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                    },
                    {
                        data: 'defaultServiceDetails.fullName',
                        render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                    }
                ],
                buttons: [
                    {
                        text: 'Добавить обследование',
                        action: (e, dt, button, config) => {
                            this._examinationModal.show(null);
                        }
                    },
                    {
                        extend: 'selectedSingle',
                        text: 'Редактировать',
                        action: (e, dt, button, config) => {
                            let model = dt.row({ selected: true }).data();
                            model.editing = true;
                            this._examinationModal.show(model);
                        }
                    },
                    {
                        extend: 'selectedSingle',
                        text: 'Показатели результата',
                        action: (e, dt, button, config) => {
                            let model = dt.row({ selected: true }).data();
                            this._examinationIndexes.show(model.id, model.name);
                        }
                    }
                ]
            }
        }

        this.examinationsTable = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(config).getTable();
    }

    _createExaminationModal() {
        const config = {
            title: (model) => model.editing ? `Редактирование обследования: ${model.name}` : "Создание нового обследования",
            data: [
                {
                    id: 'target-group',
                    path: 'targetGroup.id',
                    label: 'Целевая группа',
                    type: 'select',
                    options: this._examinationsData.targetGroups.map(group => new Option(group.name, group.id))
                },
                {
                    id: 'name',
                    path: 'name',
                    label: 'Название по приказу',
                    type: 'textarea',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText500
                },
                {
                    id: 'service-code',
                    path: 'defaultServiceDetails.code',
                    label: 'Код услуги по умолчанию',
                    type: 'input-text',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText20
                },
                {
                    id: 'service-full-name',
                    path: 'defaultServiceDetails.fullName',
                    label: 'Полное наименование услуги по умолчанию',
                    type: 'textarea',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText500
                },
                {
                    id: 'is-mandatory',
                    path: 'isMandatory',
                    label: 'Обязательное при любом осмотре',
                    type: 'input-checkbox'
                }
            ],
            buttons: [
                {
                    text: 'Сохранить',
                    action: this._onSaveExamination.bind(this)
                }
            ]
        }

        this._examinationModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(config);
    }

    _createExaminationIndexesModal() {
        this._examinationIndexes = new _order_examination_indexes__WEBPACK_IMPORTED_MODULE_5__.default(this.succesToast);
    }

    _getTableData() {
        return this._examinationsData.orderExaminations
            .map(examination => this._convertToTableData(examination));
    }

    _convertToTableData(examination) {
        return {
            id: examination.id,
            name: examination.name,
            targetGroup: {
                id: examination.targetGroupId,
                name: this._examinationsData.targetGroups
                    .find(group => group.id == examination.targetGroupId)
                    .name
            },
            defaultServiceDetails: examination.defaultServiceDetails,
            isMandatory: examination.isMandatory
        };
    }

    async _onSaveExamination(model) {
        const data = {
            name: model.name,
            defaultServiceCode: model.defaultServiceDetails.code,
            defaultServiceFullName: model.defaultServiceDetails.fullName,
            targetGroupId: +model.targetGroup.id,
            isMandatory: model.isMandatory
        };

        let response;

        if (model.editing) {
            response = await updateExamination.call(this);
        } else {
            response = await createExamination.call(this);
        }

        if (response) {
            this._examinationModal.hide();
            this.succesToast.show();
        }
        else {
            this._examinationModal.enableButtons();
        }

        async function updateExamination() {
            const updatedExamination = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(
                URI_API_ORDER_EXAMINATION_UPDATE,
                Object.assign(data, { id: model.id })
            );

            if (updatedExamination) {
                this.examinationsTable
                    .row((index, data, node) => data.id === updatedExamination.id)
                    .data(this._convertToTableData(updatedExamination))
                    .draw();
                return updatedExamination;
            }
        }

        async function createExamination() {
            const newExamination = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_ORDER_EXAMINATION_CREATE, data);

            if (newExamination) {
                this.examinationsTable
                    .row
                    .add(this._convertToTableData(newExamination))
                    .draw();
                return newExamination;
            }
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (OrderExaminationsPage);

/***/ }),

/***/ "./wwwroot/js/src/order-items.js":
/*!***************************************!*
  !*** ./wwwroot/js/src/order-items.js ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* harmony import */ var _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿






const URI_API_ORDER_ITEMS = '/api/order/getOrder?nocache=true';
const URI_API_ORDER_ITEM_CREATE = '/api/order/addItem';
const URI_API_ORDER_ITEM_UPDATE = '/api/order/updateItem';
const URI_API_ORDER_ITEM_REMOVE = '/api/order/deleteItem';
const URI_API_ORDER_EXAMINATIONS = '/api/order/getExaminationsMin';

const ID_SELECT_EXAMINATIONS = 'examinations';
const ID_KEY = 'key';

const SELECTOR_EXAMINATIONS = `select[data-custom-modal-id="${ID_SELECT_EXAMINATIONS}"`;
const SELECTOR_KEY = `input[data-custom-modal-id="${ID_KEY}"`;


class OrderItemsPage {
    constructor() {
        this.succesToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_3__.default();
    }


    // Static

    static init() {
        return (async () => {
            let _this = new OrderItemsPage();

            await _this._getOrderData();
            _this._createDataTable();
            _this._createModal();

            return _this;
        })();
    }


    // Private

    async _getOrderData() {
        this._orderItems = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.getData(URI_API_ORDER_ITEMS);

        let orderExaminations = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.getData(URI_API_ORDER_EXAMINATIONS);
        this._orderExaminationsMap = new Map(orderExaminations.map(i => [i.id, i.name]));
    }

    _createDataTable() {
        const config = {
            tableId: 'OrderItems',
            advanced: {
                data: this._getDataTableData(),
                ordering: false,
                columns: [
                    {
                        data: 'key',
                        render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                    },
                    {
                        data: 'name',
                        render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
                    },
                    {
                        data: 'examinations',
                        render: (data, type, row) => data.map(ex => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(ex.name)).join('<br>')
                    }
                ],
                buttons: [
                    {
                        text: 'Добавить пункт',
                        action: (e, dt, button, config) => {
                            this._showModal(null);
                        }
                    },
                    {
                        extend: 'selectedSingle',
                        text: 'Редактировать',
                        action: (e, dt, button, config) => {
                            let model = dt.row({ selected: true }).data();
                            model.editing = true;
                            this._showModal(model);
                        }
                    },
                    {
                        extend: 'selectedSingle',
                        text: 'Удалить',
                        action: async (e, dt, button, config) => {
                            let data = dt.row({ selected: true }).data();
                            await this._removeItem(data);
                        }
                    }
                ]
            }
        };

        this.orderTable = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(config).getTable();
    }

    _createModal() {
        let config = {
            title: (model) => model.editing ? `Редактирование пункта ${model.key}` : 'Создание нового пункта',
            data: [
                {
                    id: 'key',
                    path: 'key',
                    label: 'Пункт',
                    type: 'input-text',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText70
                },
                {
                    id: 'name',
                    path: 'name',
                    label: 'Название',
                    type: 'textarea',
                    validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText500
                },
                {
                    id: ID_SELECT_EXAMINATIONS,
                    path: 'examinations',
                    label: 'Обследования',
                    type: 'select',
                    options: [] // будет инициализирован через select2
                }
            ],
            buttons: [
                {
                    text: 'Сохранить',
                    action: this._onSave.bind(this)
                }
            ]
        };

        this.orderItemModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(config);
        this._initSelect2();
    }

    _getDataTableData() {
        return this._orderItems.map(item => this._convertToTableData(item));
    }

    _convertToTableData(orderItemResource) {
        return {
            id: orderItemResource.id,
            key: orderItemResource.key,
            name: orderItemResource.name,
            examinations: orderItemResource.orderExaminations.map(id => {
                return {
                    id: id,
                    name: this._orderExaminationsMap.get(id)
                }
            })
        }
    }

    _initSelect2() {
        const select2Data = Array.from(this._orderExaminationsMap.entries())
            .map(i => {
                return {
                    id: i[0],
                    text: i[1]
                }
            });

        $(SELECTOR_EXAMINATIONS).select2({
            data: select2Data,
            multiple: true,
            placeholder: 'Найти по названию',
            theme: 'bootstrap4'
        });
    }

    async _onSave(model) {
        const examinations = $(SELECTOR_EXAMINATIONS).select2('data').map(i => parseInt(i.id));
        let response;

        if (model.editing === true) {
            response = await updateOrderItem.call(this);
        } else {
            response = await createOrderItem.call(this);
        }

        if (response) {
            this.orderItemModal.hide();
            this.succesToast.show();
        }
        else {
            this.orderItemModal.enableButtons();
        }

        async function updateOrderItem() {
            const updatedItem = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_ORDER_ITEM_UPDATE, {
                id: model.id,
                name: model.name,
                examinations
            });

            if (updatedItem) {
                this.orderTable
                    .row((index, data, node) => data.id === updatedItem.id)
                    .data(this._convertToTableData(updatedItem))
                    .draw();
                return updatedItem;
            }
        }

        async function createOrderItem() {
            const newItem = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_ORDER_ITEM_CREATE, {
                key: model.key,
                name: model.name,
                examinations
            });

            if (newItem) {
                this.orderTable
                    .row
                    .add(this._convertToTableData(newItem))
                    .draw();
                return newItem;
            }
        }
    }

    async _removeItem(itemData) {
        if (confirm(`Вы действительно хотите удалить пункт "${itemData.key}"?`)) {
            const response = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_ORDER_ITEM_REMOVE, itemData.id);
            if (response.succeed) {
                this.orderTable
                    .row((index, data, node) => data.id === itemData.id)
                    .remove()
                    .draw();
                this.succesToast.show();
            }
        }
    }

    _showModal(model) {
        const elementsToDisableOnEditItem = [
            document.querySelector(SELECTOR_KEY)
        ];

        if (!model) {
            processModalElementsOnCreateItem();
        } else {
            processModalElementsOnEditItem();
        }

        this.orderItemModal.show(model);
        setSelectedExaminations();


        function setSelectedExaminations() {
            $(SELECTOR_EXAMINATIONS).val(model?.examinations.map(ex => ex.id)).trigger('change');
        }

        function processModalElementsOnEditItem() {
            elementsToDisableOnEditItem.forEach(element => element.disabled = true);
        }

        function processModalElementsOnCreateItem() {
            elementsToDisableOnEditItem.forEach(element => element.disabled = false);
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (OrderItemsPage);

/***/ }),

/***/ "./wwwroot/js/src/register-create-request.js":
/*!***************************************************!*
  !*** ./wwwroot/js/src/register-create-request.js ***!
  \***************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
﻿



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

        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.validate)(validatedRegisterRequestInputs);
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

        let result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_REGISTER_REQUEST_CREATE, data);

        if (result) {
            showSuccessMessage();
        }

        submitRegisterRequestBtn.disabled = false;
    }

    function addFormValidation() {
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateFullNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText500);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateShortNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText500);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateAddressInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText500);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidatePhoneInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.phone);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateEmailInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.email);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateModeratorNameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText70);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateModeratorPositionInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.requiredText70);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateModeratorUsernameInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.username);
        (0,_util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.addValidation)(candidateModeratorPasswordInput, _util_custom_validation__WEBPACK_IMPORTED_MODULE_1__.DefaultChecks.password);
    }

    function showSuccessMessage() {
        document.RegisterRequest.classList.add('d-none');
        document.querySelector('.alert-success').classList.remove('d-none');
    }
}


/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initRegisterCreateRequest);

/***/ }),

/***/ "./wwwroot/js/src/register-requests.js":
/*!*********************************************!*
  !*** ./wwwroot/js/src/register-requests.js ***!
  \*********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* harmony import */ var _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
﻿





const URI_API_REGISTER_REQUESTS_NEW = '/api/clinic/newRequests';
const URI_API_REGISTER_REQUESTS_PROCESSED = '/api/clinic/processedRequests';
const URI_API_REGISTER_REQUEST_MANAGE = '/api/clinic/manageRequest';


class RegisterRequestsListPage {
    constructor() {
        this.successToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_3__.default();
        this._createDataTables();
        this._createRegisterRequestsModal();
    }

    // Private 

    _createDataTables() {
        const columnsBase = [
            {
                data: 'creationTime',
                name: 'CreationTime',
                render: (data, type, row) => new Date(data).toLocaleString(),
                searchable: false
            },
            {
                data: 'shortName',
                name: 'ShortName',
                render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
            },
            {
                data: 'sender.name',
                name: 'Sender.Name',
                render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data)
            }
        ];

        const approvedStatusColumn = {
            data: 'approved',
            name: 'Approved',
            render: (data, type, row) => data ? 'Одобрена' : 'Отклонена',
            searchable: false
        };

        const tableButtons = [
            {
                extend: 'selectedSingle',
                text: 'Просмотр',
                action: (e, dt, button, config) => {
                    let model = dt.row({ selected: true }).data();
                    this.registerRequestModal.show(model);
                }
            }
        ];

        const newRequestsTableConfig = {
            tableId: 'NewRegisterRequests',
            ajaxUrl: URI_API_REGISTER_REQUESTS_NEW,
            serverSide: true,
            advanced: {
                columns: columnsBase,
                buttons: tableButtons
            }
        }

        this.newRequestsDataTable = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(newRequestsTableConfig).getTable();

        const processedRequestsTableColumns = columnsBase.slice();
        processedRequestsTableColumns.push(approvedStatusColumn);

        const processedRequestTableConfig = {
            tableId: 'ProcessedRegisterRequests',
            ajaxUrl: URI_API_REGISTER_REQUESTS_PROCESSED,
            serverSide: true,
            advanced: {
                columns: processedRequestsTableColumns,
                buttons: tableButtons
            }
        }

        this.processedRequestsDataTable = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(processedRequestTableConfig).getTable();
    }

    _createRegisterRequestsModal() {
        const config = {
            title: 'Информация о заявке',
            readonly: true,
            data: [
                {
                    id: 'date',
                    path: 'creationTime',
                    label: 'Дата',
                    type: 'input-text',
                    render: (value) => new Date(value).toLocaleString()
                },
                {
                    id: 'full-name',
                    path: 'fullName',
                    label: 'Полное наименование',
                    type: 'input-text'
                },
                {
                    id: 'short-name',
                    path: 'shortName',
                    label: 'Сокращенное наименование',
                    type: 'input-text'
                },
                {
                    id: 'address',
                    path: 'address',
                    label: 'Адрес',
                    type: 'input-text'
                },
                {
                    id: 'phone',
                    path: 'phone',
                    label: 'Телефон',
                    type: 'input-text'
                },
                {
                    id: 'email',
                    path: 'email',
                    label: 'Электронная почта',
                    type: 'input-text'
                },
                {
                    id: 'contact-person',
                    path: 'sender.name',
                    label: 'Контактное лицо',
                    type: 'input-text',
                    render: (data) => `${data.name}, ${data.position}`
                },
                {
                    id: 'username',
                    path: 'sender.userName',
                    label: 'Username',
                    type: 'input-text'
                }
            ],
            buttons: [
                {
                    text: 'Одобрить',
                    action: model => this._manageRegisterRequest(model, true),
                    className: 'btn btn-success',
                    visibility: model => !model.approved
                },
                {
                    text: 'Отклонить',
                    action: model => this._manageRegisterRequest(model, false),
                    className: 'btn btn-danger',
                    visibility: model => !model.processed
                }
            ]
        }

        this.registerRequestModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(config);
    }

    async _manageRegisterRequest(model, approved) {
        const data = {
            id: model.id,
            approved
        };

        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_REGISTER_REQUEST_MANAGE, data);
        if (result) {
            this.registerRequestModal.hide();
            this.successToast.show();
            this.newRequestsDataTable.ajax.reload();
            this.processedRequestsDataTable.ajax.reload();
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (RegisterRequestsListPage);

/***/ }),

/***/ "./wwwroot/js/src/user-list.js":
/*!*************************************!*
  !*** ./wwwroot/js/src/user-list.js ***!
  \*************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./util/modal/modal-form */ "./wwwroot/js/src/util/modal/modal-form.js");
/* harmony import */ var _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./util/custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _util_success_toast__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./util/success-toast */ "./wwwroot/js/src/util/success-toast.js");
/* harmony import */ var _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./util/custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
﻿






function initUserList() {
    const URI_API_USERS_LIST = '/api/user/list';
    const URI_API_USER_CREATE = '/api/user/create';
    const URI_API_USER_UPDATE_BASE = '/api/user/update';

    const isGlobal = document.getElementById('UsersList').dataset.global == 'True';
    const successToast = new _util_success_toast__WEBPACK_IMPORTED_MODULE_3__.default();


    // Data объекты для Modal

    const usernameData = {
        id: 'username',
        path: 'username',
        type: 'input-text',
        label: 'Имя пользователя',
        validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.username
    };

    const passwordData = {
        id: 'password',
        path: 'password',
        type: 'input-password',
        label: 'Пароль',
        validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.password
    };

    const confirmPasswordData = getConfirmPasswordData('#custom-modal-1 input[data-custom-modal-id="password"]');
    const confirmPasswordData2 = getConfirmPasswordData('#custom-modal-3  input[data-custom-modal-id="password"]');

    const nameData = {
        id: 'name',
        path: 'name',
        type: 'input-text',
        label: 'ФИО',
        validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText70
    };
    const positionData = {
        id: 'position',
        path: 'position',
        type: 'input-text',
        label: 'Должность',
        validityCheck: _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.DefaultChecks.requiredText70
    };


    //
    // Создание пользователя
    //
    const createUserModalConfig = {
        title: 'Создание нового пользователя',
        data: [
            usernameData,
            passwordData,
            confirmPasswordData,
            nameData,
            positionData,
            getRoleData()
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: onUserCreate
            }
        ]
    }
    const createUserModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(createUserModalConfig);


    //
    // Редактирование профиля
    //
    const editProfileModalConfig = {
        title: (model) => `Редактирование профиля пользователя ${model.username}`,
        data: [
            nameData,
            positionData,
            getRoleData()
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: onUserProfileSave
            }
        ]
    }
    const editProfileModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(editProfileModalConfig);


    //
    // Изменение пароля
    //
    const changePasswordModalConfig = {
        title: (model) => `Задать новый пароль для пользователя ${model.username}`,
        data: [
            passwordData,
            confirmPasswordData2
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: onPasswordChange
            }
        ]
    }
    const changePasswordModal = new _util_modal_modal_form__WEBPACK_IMPORTED_MODULE_1__.default(changePasswordModalConfig);


    //
    // Таблица
    //
    const dataTableConfig = {
        tableId: 'UsersList',
        ajaxUrl: URI_API_USERS_LIST,
        serverSide: true,
        scroll: true,
        advanced: {
            columns: [
                {
                    data: 'id',
                    name: 'Id',
                    searchable: false,
                    orderable: true
                },
                {
                    data: 'name',
                    name: 'Name',
                    render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data),
                    searchable: true,
                    orderable: true
                },
                {
                    data: 'role.name',
                    name: 'Role.Name',
                    render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data),
                    searchable: true,
                    orderable: true
                }
            ],
            buttons: [
                {
                    text: 'Создать',
                    action: function (e, dt, button, config) {
                        createUserModal.show(null);
                    }
                },
                {
                    extend: 'selectedSingle',
                    text: 'Изменить профиль',
                    action: function (e, dt, button, config) {
                        let model = dt.row({ selected: true }).data();
                        editProfileModal.show(model);
                    }
                },
                {
                    extend: 'selectedSingle',
                    text: 'Изменить пароль',
                    action: function (e, dt, button, config) {
                        let model = dt.row({ selected: true }).data();
                        changePasswordModal.show(model);
                    }
                }
            ]
        }
    };
    const clinicCol = {
        data: 'clinicShortName',
        name: 'ClinicShortName',
        render: (data, type, row) => _util_common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(data),
        searchable: true,
        orderable: true
    };

    if (isGlobal)
        dataTableConfig.advanced.columns.push(clinicCol);

    const table = new _util_custom_datatable__WEBPACK_IMPORTED_MODULE_2__.default(dataTableConfig);


    // Обработчики кнопок модальных окон

    async function onUserCreate(model) {
        const data = {
            name: model.name,
            position: model.position,
            username: model.username,
            password: model.password,
            roleId: +model.role.id
        };

        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_USER_CREATE, data);
        processResult(createUserModal, result);
    }

    async function onUserProfileSave(model) {
        const data = {
            name: model.name,
            position: model.position,
            roleId: +model.role.id
        };
        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(`${URI_API_USER_UPDATE_BASE}/${model.id}`, data);
        processResult(editProfileModal, result);
    }

    async function onPasswordChange(model) {
        const data = {
            password: model.password
        }
        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(`${URI_API_USER_UPDATE_BASE}/${model.id}`, data);
        processResult(changePasswordModal, result);
    }


    // Helpers

    function getConfirmPasswordData(inputToConfirmSelector) {
        return {
            id: 'password-confirmation',
            path: null,
            type: 'input-password',
            label: 'Повторите пароль',
            validityCheck: getConfirmPasswordValidityCheck(inputToConfirmSelector)
        }

        function getConfirmPasswordValidityCheck(inputToConfirmSelector) {
            return new _util_custom_validation__WEBPACK_IMPORTED_MODULE_4__.ValidityCheck(
                'Пароли не совпадают',
                (input) => input.value !== document.querySelector(inputToConfirmSelector).value);
        }
    }

    function getRoleOptions() {
        let options = [
            new Option('Сотрудник', '3'),
            new Option('Модератор клиники', '2'),
            new Option('Заблокированный', '4')
        ];
        if (isGlobal)
            options.push(new Option('Администратор сайта', '1'));
        return options;
    }

    function getRoleData() {
        return {
            id: 'roleId',
            path: 'role.id',
            type: 'select',
            label: 'Тип аккаунта',
            options: getRoleOptions()
        };
    }

    function processResult(modal, result) {
        if (result) {
            modal.hide();
            successToast.show();
            table.ajaxReload();
        }
        else {
            modal.enableButtons();
        }
    }
}


/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initUserList);

/***/ }),

/***/ "./wwwroot/js/src/user-login.js":
/*!**************************************!*
  !*** ./wwwroot/js/src/user-login.js ***!
  \**************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _util_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./util/common */ "./wwwroot/js/src/util/common.js");
﻿


function initUserLogin() {
    const URI_API_USER_LOGIN = '/api/user/login';
    const CLASSNAME_DISPLAY_NONE = 'd-none';

    const form = document.login;
    const loginErrorMessage = document.querySelector('.alert-danger');
    const loginButton = document.getElementById('LoginButton');
    loginButton.addEventListener('click', onLogin);

    async function onLogin(e) {
        e.preventDefault();
        loginButton.disabled = true;
        loginErrorMessage.classList.add(CLASSNAME_DISPLAY_NONE);

        const formData = new FormData(form);
        const data = Object.fromEntries(formData);

        const result = await _util_common__WEBPACK_IMPORTED_MODULE_0__.default.postData(URI_API_USER_LOGIN, data);
        if (result) {
            if (result.succeed === true) {
                location.replace('/');
            }
            else {
                showErrorMessage();
            }
        }
        loginButton.disabled = false;
    }

    function showErrorMessage() {
        loginErrorMessage.classList.remove(CLASSNAME_DISPLAY_NONE);       
    }
}


/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (initUserLogin);

/***/ }),

/***/ "./wwwroot/js/src/util/common.js":
/*!***************************************!*
  !*** ./wwwroot/js/src/util/common.js ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
﻿const Util = {
    customParseFloat: function (str) {
        return parseFloat(str.replace(',', '.').replace(/\s/g, ''));
    },

    toCurrencyString: function (number) {
        return number.toLocaleString(undefined,
            {
                'minimumFractionDigits': 2,
                'maximumFractionDigits': 2,
                'useGrouping': false
            });
    },

    escapeHTML: function (str) {
        if (typeof (str) !== 'string')
            return null;
        return str
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    },

    isEmpty: function (obj) {
        for (let i in obj)
            return false;
        return true;
    },

    toElement: function (html) {
        let div = document.createElement('div');
        div.innerHTML = html;
        return div.firstElementChild;
    },

    deleteResource: async function (url) {
        return await fetchDecorator(url, {
            method: 'DELETE',
            credentials: 'same-origin'
        });
    },

    getData: async function (url) {
        return await fetchDecorator(url, {
            credentials: 'same-origin'
        });
    },

    postData: async function (url, dataObject) {
        return await fetchDecorator(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            credentials: 'same-origin',
            body: JSON.stringify(dataObject)
        });
    },

    removeAllChildren: function (node) {
        while (node.firstChild) {
            node.removeChild(node.lastChild);
        }
    }
};

async function fetchDecorator(url, config) {
    const response = await fetch(url, config);
    if (response.ok) {
        return await response.json();
    }
    await processBadRequestResult(response);
    return undefined;
}

async function processBadRequestResult(response) {
    if (response.status === 400) {
        let errorResponse = await response.json();
        if (errorResponse.success === false) {
            let message = 'Во время обработки запроса возникла ошибка. Обратитесь к администратору.\n';
            for (let error of errorResponse.errors) {
                message += error + '\n'
            }
            alert(message);
        }
    }
    else {
        alert('Во время обработки запроса произошла ошибка. Код ошибки: ' + response.status);
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (Util);

/***/ }),

/***/ "./wwwroot/js/src/util/custom-datatable.js":
/*!*************************************************!*
  !*** ./wwwroot/js/src/util/custom-datatable.js ***!
  \*************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿const DOM_FLAG_BUTTON = 'B';
const DOM_FLAG_PAGINATION = 'p';

const PLACEHOLDER_DOM_BUTTON = '{BUTTON}';
const PLACEHOLDER_PAGINATION = '{PAGINATION}';

const localisation = {
    "decimal": "",
    "emptyTable": "Нет данных для отображения",
    "info": "Всего элементов: _TOTAL_",
    "infoEmpty": "",
    "infoFiltered": "(отфильтровано из _MAX_ элементов)",
    "infoPostFix": "",
    "thousands": ",",
    "lengthMenu": "_MENU_ элементов на страницу",
    "loadingRecords": "Загрузка...",
    "processing": "Загрузка...",
    "search": "Поиск:",
    "zeroRecords": "Ничего не найдено",
    "paginate": {
        "first": "Начало",
        "last": "Конец",
        "next": "Вперед",
        "previous": "Назад"
    },
    "aria": {
        "sortAscending": ": сортировка по возрастанию",
        "sortDescending": ": сортировка по убыванию"
    },
    select: {
        rows: {
            _: ''
        }
    }
};

const DefaultDt = {
    config: {
        select: {
            style: 'single'
        },
        language: localisation,
        order: [[0, 'asc']],
        orderMulti: false,
        processing: true
    },
    scrollerConfig: {
        scrollY: 450,
        scrollCollapse: true,
        deferRender: true,
        scroller: true
    },
    serverSideAjax: {
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: (d) => JSON.stringify(d)
    },
    dom: `<'row'<'col-sm-12 col-md-6'${PLACEHOLDER_DOM_BUTTON}><'col-sm-12 col-md-6'f>>
		<'row'<'col-sm-12'tr>>
		<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'${PLACEHOLDER_PAGINATION}>>`
}

// Options:
// --- tableId, // (обязательно) id целевой таблицы
// --- ajaxUrl, // URL для запроса данных в формате dataTables
// --- serverSide : Boolean // Если true, то таблица будет загружаться генерируемыми на сервере фрагментами (требуется настройка на сервере - см. DataTables)
// --- scroll : Boolean // если true, применяются настройки скролла из конфига по умолчанию
// --- advanced : {} // Обычный конфиг dataTables, который будет наложен поверх настроенного, если необходима детальная настройка
// ------ (например, можно переопределить order или задать columns, buttons)
// ------ columns, // (обязательно для ajax) см. dataTables
// --------- (коротко)
// --------- data : string // путь к свойству с данными
// --------- name : string // имя свойства с данными, которое будет отправлено с запросом при serverSide обработке
// --------- render : function(data, type, row) : string // функция обработки данных для отображения
// --------- searchable : Boolean // Если true, данные из колонки будет возможно найти поиском
// --------- orderable : Boolean // Если true, возможна сортировка
// ------ buttons // см. dataTables, коротко:
// --------- text : string // Текст кнопки
// --------- action: function(e, dt, button, config) // действие при клике
// --------- extend: 'selectedSingle' // кнопка активна только, если выделена одна строка в таблице

class CustomDataTable {
    constructor(options) {
        this._options = options;
        this._applyOptions();
        this._table = $('#' + this._options.tableId).DataTable(this._config);
    };

    getTable() {
        return this._table;
    };

    ajaxReload() {
        if (!this._config.ajax) {
            console.log('CustomDataTable: Невозможно перезагрузить данные для этой таблицы посредствам ajax');
            return;
        }
        this._table.ajax.reload();
    }

    _applyOptions() {
        this._config = $.extend(true, {}, DefaultDt.config);

        if (this._options.ajaxUrl) {
            this._config.ajax = {
                url: this._options.ajaxUrl
            }
        }

        if (this._options.scroll) {
            this._config = { ...this._config, ...DefaultDt.scrollerConfig };
        }

        if (this._options.serverSide) {
            if (this._config.ajax) {
                this._config.serverSide = true;
                Object.assign(this._config.ajax, DefaultDt.serverSideAjax);
            } else {
                console.log('CustomDataTable: Для обработки таблиц на сервере, задайте ajaxUrl');
            }
        }

        if (this._options.advanced) {
            Object.assign(this._config, this._options.advanced);
        }

        this._applyDomConfig();
    };

    _applyDomConfig() {
        let buttonDomFlag = '';
        let paginationDomFlag = '';

        if (this._config.buttons) {
            buttonDomFlag = DOM_FLAG_BUTTON;
        }
        if (!this._scroller) {
            paginationDomFlag = DOM_FLAG_PAGINATION;
        }

        this._config.dom = DefaultDt.dom.replace(PLACEHOLDER_DOM_BUTTON, buttonDomFlag)
                                      .replace(PLACEHOLDER_PAGINATION, paginationDomFlag);
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (CustomDataTable);

/***/ }),

/***/ "./wwwroot/js/src/util/custom-validation.js":
/*!**************************************************!*
  !*** ./wwwroot/js/src/util/custom-validation.js ***!
  \**************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "CustomValidation": () => /* binding */ CustomValidation,
/* harmony export */   "ValidityCheck": () => /* binding */ ValidityCheck,
/* harmony export */   "validate": () => /* binding */ validate,
/* harmony export */   "addValidation": () => /* binding */ addValidation,
/* harmony export */   "resetValidation": () => /* binding */ resetValidation,
/* harmony export */   "DefaultChecks": () => /* binding */ DefaultChecks
/* harmony export */ });
﻿function CustomValidation(input, validityCheck) {
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




/***/ }),

/***/ "./wwwroot/js/src/util/modal/custom-modal-base.js":
/*!********************************************************!*
  !*** ./wwwroot/js/src/util/modal/custom-modal-base.js ***!
  \********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../common */ "./wwwroot/js/src/util/common.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿


// Options
// --- title : string | function (model) : void (required) // Заголовок модального окна
// --- buttons : [] // Кнопки модального окна
// ------ text : string (required) // Текст
// ------ action : async function (model) : void (required) // Действие, выполняемое при клике
// ------ className : string // CSS классы
// ------ visibility : function (model) : boolean // Условие видимости кнопки, в зависимости от состояния модели


const CLASSNAME_DISPLAY_NONE = 'd-none';
const CLASSNAME_MODAL_TITLE = 'modal-title';
const CLASSNAME_MODAL_BODY = 'modal-body';
const CLASSNAME_MODAL_FOOTER = 'modal-footer';
const CLASSNAME_BUTTON_DEFAULT = 'btn btn-primary';

const EVENT_MODAL_SHOW = 'show';
const EVENT_MODAL_HIDE = 'hide';
const EVENT_MODAL_HIDDEN = 'hidden.bs.modal';

const BASE_MODAL_ID = 'custom-modal-';

const TEMPLATE_MODAL = `<div class="modal fade" data-backdrop="static" tabindex="-1" role="dialog" aria-hidden="true">
<div class="modal-dialog  modal-dialog-centered modal-lg" role="document">
<div class="modal-content">
<div class="modal-header">
<h5 class="${CLASSNAME_MODAL_TITLE}"></h5>
<button type="button" class="close" data-dismiss="modal" aria-label="Close">
<span aria-hidden="true">&times;</span>
</button>
</div>
<div class="${CLASSNAME_MODAL_BODY}">
</div>
<div class="${CLASSNAME_MODAL_FOOTER}">
</div>
</div>
</div>
</div>`;


class CustomBootstrapModalBase {
    constructor(options) {
        this._modalElement = null;
        this.model = null;
        this.buttonsData = [];
        this.options = options;

        this._build();
    };


    // Public

    enableButtons() {
        for (let buttonData of this.buttonsData) {
            buttonData.element.disabled = false;
            if (buttonData.visibility === true || _common__WEBPACK_IMPORTED_MODULE_0__.default.isEmpty(this.model) || buttonData.visibility(this.model))
                buttonData.element.classList.remove(CLASSNAME_DISPLAY_NONE);
            else
                buttonData.element.classList.add(CLASSNAME_DISPLAY_NONE);
        }
    };

    getId() {
        if (!this._id) {
            let counter = 0;
            let id = null;
            let existing = null;
            do {
                id = BASE_MODAL_ID + ++counter;
                existing = document.getElementById(id);
            } while (existing);
            this._id = id;
        }
        return this._id;
    };

    hide() {
        $(`#${this.getId()}`).modal(EVENT_MODAL_HIDE);
    };

    show() {
        this.show(null);
    }

    show(model) {
        this._prepareToShow(model);
        $(`#${this.getId()}`).modal(EVENT_MODAL_SHOW);
    };


    // "Protected"

    _getContentValidationResult() {
        return true;
    }

    _getModalBodyContent() {
        // "Abstract"
    }

    _onModalHidden() {

    }

    _prepareToShow(model) {
        this.model = $.extend(true, {}, model);
        this._setTitle();
        this.enableButtons();
    }


    // Private

    _build() {
        this._createModalElement();
        this._createModalBody();
        this._createButtons();
        this._addModalHiddenEventListener();
    };

    _addModalHiddenEventListener() {
        $(`#${this.getId()}`).on(EVENT_MODAL_HIDDEN, this._onModalHidden.bind(this));
    };

    _createModalElement() {
        this._modalElement = _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(TEMPLATE_MODAL);
        this._modalElement.id = this.getId();
        document.body.appendChild(this._modalElement);
    };

    _createModalBody() {
        const bodyContent = this._getModalBodyContent();
        if (!bodyContent)
            throw new Error('Содержимое модального окна не определено');
        this.modalBody = this._modalElement.querySelector('.' + CLASSNAME_MODAL_BODY);
        this.modalBody.appendChild(bodyContent);
    };

    _createButtons() {
        if (!this.options.buttons)
            return;
        let parent = this._modalElement.querySelector('.' + CLASSNAME_MODAL_FOOTER);

        for (let buttonConfig of this.options.buttons) {
            let button = this._getButton(buttonConfig);
            parent.appendChild(button);

            this.buttonsData.push({
                element: button,
                visibility: typeof buttonConfig.visibility == 'function' ? buttonConfig.visibility : true
            });
        }
    };

    _getButton(config) {
        let button = document.createElement('button');
        button.className = config.className ? config.className : CLASSNAME_BUTTON_DEFAULT;
        button.innerText = config.text;
        button.addEventListener('click', onBtnClick.bind(this));

        return button;

        async function onBtnClick(event) {
            event.preventDefault();
            event.target.disabled = true;
            const canExecute = this._getContentValidationResult();
            if (!canExecute) {
                event.target.disabled = false;
                return;
            }
            await config.action(this.model);
        }
    };

    _updateModel(path, value) {
        let props = path.split('.');
        let obj = this.model;

        for (let i = 0; i < props.length; i++) {
            if (i === props.length - 1)
                obj[props[i]] = value;
            else {
                if (!obj[props[i]])
                    obj[props[i]] = {};
                obj = obj[props[i]];
            }
        }
    };

    _getValue(path) {
        if (path === null)
            return null;

        let obj = this.model;
        let props = path.split('.');

        for (let i = 0; i < props.length; i++) {
            if (i === props.length - 1)
                return obj[props[i]];
            else
                obj = obj[props[i]];
        }
    };

    _setTitle() {
        const title = this.options.title;
        const titleElement = this._modalElement.querySelector('.' + CLASSNAME_MODAL_TITLE);
        switch (typeof (title)) {
            case 'string':
                titleElement.innerText = title;
                break;
            case 'function':
                titleElement.innerText = title(this.model);
                break;
        }
    };
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (CustomBootstrapModalBase);

/***/ }),

/***/ "./wwwroot/js/src/util/modal/modal-datatable.js":
/*!******************************************************!*
  !*** ./wwwroot/js/src/util/modal/modal-datatable.js ***!
  \******************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _custom_datatable__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../custom-datatable */ "./wwwroot/js/src/util/custom-datatable.js");
/* harmony import */ var _custom_modal_base__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./custom-modal-base */ "./wwwroot/js/src/util/modal/custom-modal-base.js");
﻿




// Options
// --- title : string | function (model) : void (required) // Заголовок модального окна
// --- columns : [] // Столбцы таблицы
// ------ title : string // Название столбца
// --- buttons : [] // Кнопки модального окна
// ------ text : string (required) // Текст
// ------ action : async function (model) : void (required) // Действие, выполняемое при клике
// ------ className : string // CSS классы
// ------ visibility : function (model) : boolean // Условие видимости кнопки, в зависимости от состояния модели
// --- dataTableOptions : Object // Конфигурация DataTables


const CLASSNAME_TABLE = "table table-bordered table-striped w-100";


class ModalDataTable extends _custom_modal_base__WEBPACK_IMPORTED_MODULE_2__.default {
    constructor(options) {
        super(options);

        this.options.dataTableOptions.tableId = this._getDataTableId();
        this.dataTable = new _custom_datatable__WEBPACK_IMPORTED_MODULE_1__.default(this.options.dataTableOptions).getTable();
    }


    seedTable() { // конфигурация ajax dataTables не используется из-за сложностей замены url
        _common__WEBPACK_IMPORTED_MODULE_0__.default.getData(this.model.sourceURL)
            .then(newData => {
                if (newData) {
                    this.dataTable.clear();
                    this.dataTable.rows.add(newData);
                    this.dataTable.draw();
                }
            });
    }


    // Overrides

    //_getContentValidationResult() {
    //    return true;
    //}

    _getModalBodyContent() {
        return this._createTable();
    }

    _onModalHidden() {
        super._onModalHidden();

        this.dataTable.clear();
    }

    _prepareToShow(model) {
        super._prepareToShow(model);

        this.seedTable();
    }


    // Private

    _createTable() {
        const tr = document.createElement('tr');

        for (let column of this.options.columns) {
            const th = document.createElement('th');
            th.innerText = column.title;

            tr.appendChild(th);
        }

        const thead = document.createElement('thead');
        thead.appendChild(tr);

        const table = document.createElement('table');
        table.id = this._getDataTableId();
        table.className = CLASSNAME_TABLE;
        table.appendChild(thead);

        return table;
    }

    _getDataTableId() {
        if (this._dataTableId) {
            return this._dataTableId;
        }
        this._dataTableId = this.getId() + '-table';
        return this._dataTableId;
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (ModalDataTable);

/***/ }),

/***/ "./wwwroot/js/src/util/modal/modal-form.js":
/*!*************************************************!*
  !*** ./wwwroot/js/src/util/modal/modal-form.js ***!
  \*************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../common */ "./wwwroot/js/src/util/common.js");
/* harmony import */ var _custom_modal_base__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./custom-modal-base */ "./wwwroot/js/src/util/modal/custom-modal-base.js");
/* harmony import */ var _custom_validation__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../custom-validation */ "./wwwroot/js/src/util/custom-validation.js");
﻿




// Options
// --- title : string | function (model) : void (required) // Заголовок модального окна
// --- data : [] (required) // Элементы данных модального окна
// ------ id : string (required, unique) // Уникальный идентификатор
// ------ path : string (required) // Путь к свойству объекта модели
// ------ label : string (required) // Label (название) для соответствующего html-тега
// ------ type : string ('input-text' | 'input-password' | 'textarea' | 'select') (required) // Тип элемента формы
// ------ render : function (any) : string // Функция, преобразующая отображаемое значение текстового поля. Редактирование данных не будет отражаться в модели
// ------ readonly : boolean // Если true, значение будет недоступно для редактирования
// ------ validityCheck : ValidityCheck // Объект, описывающий условие валидности элемента формы
// ------ options : [] of Option (for 'select' type only)
// --- buttons : [] // Кнопки модального окна
// ------ text : string (required) // Текст
// ------ action : async function (model) : void (required) // Действие, выполняемое при клике
// ------ className : string // CSS классы
// ------ visibility : function (model) : boolean // Условие видимости кнопки, в зависимости от состояния модели
// --- readonly : boolean // Если true, все элементы будут недоступны для редактирования


const CLASSNAME_CUSTOM_MODAL = 'js-custom-modal';
const CLASSNAME_FORM_CONTROL = 'form-control';

const ID_DATA_ATTR = 'data-custom-modal-id';
const ID_DATA_ATTR_DATASET = 'customModalId';
const RENDER_DATA_ATTR_DATASET = 'customModalRendered';
const RENDER_DATA_ATTR_VALUE = 'true';

const TEMPLATE_FORM_ROW = '<div class="form-group"></div>';


class ModalForm extends _custom_modal_base__WEBPACK_IMPORTED_MODULE_1__.default {
    constructor(options) {
        super(options);
    };


    // Overrides

    _getContentValidationResult() {
        this._dataElements.forEach(element => element.dispatchEvent(new Event('change')));
        (0,_custom_validation__WEBPACK_IMPORTED_MODULE_2__.validate)(this._dataElements);
        return this._form.checkValidity()
    }

    _getModalBodyContent() {
        return this._createForm();
    }

    _onModalHidden() {
        super._onModalHidden();

        this._form.reset();
        (0,_custom_validation__WEBPACK_IMPORTED_MODULE_2__.resetValidation)(this._dataElements);
        this.model = null;
    }

    _prepareToShow(model) {
        super._prepareToShow(model);

        if (model)
            this._seedData();
    }


    // Private

    _createForm() {
        this._form = document.createElement('form');
        for (let dataObj of this.options.data) {
            let dataElement = this._createDataElement(dataObj);
            this._form.appendChild(dataElement);
        }

        this._addFormEventListeners();

        return this._form;
    };

    _addFormEventListeners() {
        this._dataElements = this._form.querySelectorAll(`.${CLASSNAME_CUSTOM_MODAL}`);
        for (let item of this._dataElements) {
            item.addEventListener('keyup', onElementValueChanged.bind(this));
            item.addEventListener('change', onElementValueChanged.bind(this));
        }

        function onElementValueChanged(event) {
            let element = event.target;
            let id = element.dataset[ID_DATA_ATTR_DATASET];
            let path = this._getPathById(id);
            if (path && element.dataset[RENDER_DATA_ATTR_DATASET] !== RENDER_DATA_ATTR_VALUE) {
                if (element.type === 'checkbox') {
                    this._updateModel(path, element.checked);
                } else {
                    this._updateModel(path, element.value);
                }
            }
        };
    };

    _createDataElement(data) {
        const isDisabled = data.readonly || this.options.readonly;
        let control = null;
        switch (data.type) {
            case 'input-text':
                control = _getInput('text');
                break;
            case 'input-password':
                control = _getInput('password');
                break;
            case 'input-checkbox':
                return _getCheckbox();
            case 'textarea':
                control = _getFormControl('textarea');
                break;
            case 'select':
                return _getVisibleDataElement(_getSelect());
        }

        if (data.validityCheck && !data.readonly)
            this._addValidation(data.validityCheck, control);

        if (data.render) {
            control.dataset[RENDER_DATA_ATTR_DATASET] = RENDER_DATA_ATTR_VALUE;
        }

        return _getVisibleDataElement(control);

        function _getVisibleDataElement(dataElement) {
            let container = _getDataContainer();
            container.appendChild(dataElement);

            let row = _getFormRow();
            row.appendChild(container);
            return row;
        };

        function _getInput(type) {
            let input = _getFormControl('input');
            input.type = type;
            return input;
        };

        function _getSelect() {
            let select = _getFormControl('select');
            for (let option of data.options) {
                select.options.add(option);
            }
            return select;
        };

        function _getFormControl(tagName) {
            let control = document.createElement(tagName);
            control.className = CLASSNAME_CUSTOM_MODAL + ' ' + CLASSNAME_FORM_CONTROL;
            control.dataset[ID_DATA_ATTR_DATASET] = data.id;
            control.disabled = isDisabled;
            return control;
        };

        function _getDataContainer() {
            let html = `<label style="display:block;"><span style="display:block; margin-bottom: .5rem;">${data.label}</span></label>`;
            return _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(html);
        };

        function _getFormRow() {
            return _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(TEMPLATE_FORM_ROW);
        };

        function _getCheckbox() {
            const template = `<div class="form-group form-check">    
                                <label class="form-check-label">${data.label}</label>
                              </div>`;
            const container = _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(template);

            const checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.classList.add(CLASSNAME_CUSTOM_MODAL, 'form-check-input');
            checkbox.dataset[ID_DATA_ATTR_DATASET] = data.id;
            checkbox.disabled = isDisabled;

            container.insertBefore(checkbox, container.firstChild);
            return container;
        }
    };

    _addValidation(validityCheck, element) {
        if (element.tagName !== 'select') {
            (0,_custom_validation__WEBPACK_IMPORTED_MODULE_2__.addValidation)(element, validityCheck);
        }
    };

    _getPathById(id) {
        for (let dataObj of this.options.data) {
            if (dataObj.id === id)
                return dataObj.path;
        }
        return null;
    };

    _seedData() {
        for (let dataObj of this.options.data) {
            let value = this._getValue(dataObj.path);
            if (value === null || value === undefined)
                continue;
            let element = this._modalElement.querySelector(`[${ID_DATA_ATTR}="${dataObj.id}"]`);
            if (element.type === 'checkbox') {
                element.checked = value ? true : false;
            } else {
                element.value = dataObj.render ? dataObj.render(value) : value;
            }
        }
    };
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (ModalForm);

/***/ }),

/***/ "./wwwroot/js/src/util/profession-input.js":
/*!*************************************************!*
  !*** ./wwwroot/js/src/util/profession-input.js ***!
  \*************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "initOrderItemsSelect": () => /* binding */ initOrderItemsSelect
/* harmony export */ });
/* harmony import */ var _common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./common */ "./wwwroot/js/src/util/common.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿

/*
 *  Добавление опций в селекты пунктов приказа
 */
async function initOrderItemsSelect() {
    const orderData = await _common__WEBPACK_IMPORTED_MODULE_0__.default.getData('/api/order/getOrder');
    if (orderData) {
        workWithSelect('#OrderItems', orderData);
    }
}

function workWithSelect(selector, orderData) {
    const _formatOption = option => {
        const text = _common__WEBPACK_IMPORTED_MODULE_0__.default.escapeHTML(option.text);
        const html = `<span style="display: block; overflow: hidden; white-space: nowrap;" title="${text}">${text}</span>`;
        return $(html);
    };
    const _formatSelection = option => option.key;


    $(selector).select2({
        data: orderData.map(getSelect2Data),
        multiple: true,
        placeholder: 'Найти по номеру или названию пункта...',
        theme: 'bootstrap4',
        width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
        templateResult: _formatOption,
        templateSelection: _formatSelection,
        language: {
            noResults: () => 'Совпадений не найдено'
        }
    });
}

function getSelect2Data(orderItem) {
    return {
        id: orderItem.id,
        text: `${orderItem.key}. ${orderItem.name}`,
        key: orderItem.key
    }
}



/***/ }),

/***/ "./wwwroot/js/src/util/script-manager.js":
/*!***********************************************!*
  !*** ./wwwroot/js/src/util/script-manager.js ***!
  \***********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
﻿// Запускает определенные функции на заданных страницах.

class ScriptManager {

    constructor(namespace) {
        this.namespace = namespace;
    }

    load() {
        const pageId = this._getPageId();

        this._fire('common');
        this._fire(pageId);
    }

    _getPageId() {
        return document.body.dataset.page;
    }

    _fire(funcName) {
        if (funcName !== '' && this.namespace[funcName] && typeof this.namespace[funcName] == 'function') {
            this.namespace[funcName]();
        }
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (ScriptManager);

/***/ }),

/***/ "./wwwroot/js/src/util/simple-list.js":
/*!********************************************!*
  !*** ./wwwroot/js/src/util/simple-list.js ***!
  \********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./common */ "./wwwroot/js/src/util/common.js");
﻿// Простой список. Предназначен для создания модели и представления данных, вводимых пользователем
//
// Options
// --- itemTemplate : string // например: <div>Название: {0}, автор: {1})</div>. Значение в фигурных скобках должно соответствовать индексу объекта, описывающего данные, в массиве data
// --- data : [] // массив объектов, описывающих свойства элемента списка. Значения объектов, индексы которых не представлены в шаблоне, не будут отображаться в документе, но сохранятся в модели
// ------ source : function () // функция, возвращающая значение для добавляемого элемента, например () => anyInput.value;
// ------ path : string // название свойства модели элемента списка
// ------ render : function(source) : string // функция, обрабатывающая отображаемые значения (опционально)
// --- target : string // селектор блока, в котором будет находиться список
// --- reverse : boolean // если true, новые элементы добавляются в список над последним



const TEMPLATE_ITEM_CONTAINER = '<div class="row align-items-center mb-2 position-relative"></div>';
const TEMPLATE_BUTTON_REMOVE_ITEM = '<button class="btn btn-danger btn-sm" style="position: absolute; right: 0; top: 0; opacity: 0.8">Удалить</button>';

class SimpleList {
    constructor(options) {
        this._options = options;
        this._modelMap = new Map();
        this._listElement = document.querySelector(this._options.target);
        this._processButtonRemoveItem();
    }


    // Public

    // Добавляет новый элемент в список. Сохраняет его модель и рендерит, в соответствии с заданным шаблоном
    add() {
        let itemTemplate = this._options.itemTemplate;
        let model = {};

        this._options.data.forEach((item, index, array) => {
            // сохраняем свойство модели
            let value = item.source();
            model[item.path] = value;

            // если требуется, вставляем его в шаблон
            let placeholder = '{' + index + '}';
            if (itemTemplate.includes(placeholder)) {
                if (item.render)
                    value = item.render(value);
                itemTemplate = itemTemplate.replace(placeholder, value);
            }
        });

        let itemElement = this._renderNewItem(itemTemplate);
        this._addEventListeners(itemElement);
        this._modelMap.set(itemElement, model);
    }

    // Возвращает модель данных списка
    getData() {
        return Array.from(this._modelMap.values());
    }

    // Удаляет элемент списка
    remove(itemElement) {
        this._modelMap.delete(itemElement);
        itemElement.remove();
    }


    // Private

    _addEventListeners(itemElement) {
        itemElement.addEventListener('mouseenter', (e) => e.target.appendChild(this._buttonRemoveItem));
        itemElement.addEventListener('mouseleave', (e) => e.target.removeChild(this._buttonRemoveItem));
    }

    _processButtonRemoveItem() {
        this._buttonRemoveItem = _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(TEMPLATE_BUTTON_REMOVE_ITEM);

        this._buttonRemoveItem.addEventListener('click', (e) => {
            const button = e.target;
            button.disabled = true;
            this.remove(button.parentElement);
            button.disabled = false;
        })
    }

    _renderNewItem(itemTemplate) {
        let itemElement = _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(TEMPLATE_ITEM_CONTAINER);
        itemElement.innerHTML = itemTemplate;

        if (this._options.reverse) {
            this._listElement.insertBefore(itemElement, this._listElement.firstChild);
        }
        else {
            this._listElement.appendChild(itemElement);
        }
        return itemElement;
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (SimpleList);

/***/ }),

/***/ "./wwwroot/js/src/util/success-toast.js":
/*!**********************************************!*
  !*** ./wwwroot/js/src/util/success-toast.js ***!
  \**********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "default": () => __WEBPACK_DEFAULT_EXPORT__
/* harmony export */ });
/* harmony import */ var _common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./common */ "./wwwroot/js/src/util/common.js");
/* provided dependency */ var $ = __webpack_require__(/*! jquery */ "./node_modules/jquery/dist/jquery.js");
﻿// Всплывающее в правом нижнем углу сообщение об успешности операции. Основано на Bootstrap toast.



const Default = {
    id: 'SuccessToast', // id элемента toast
    millisecondsDelay: 3000, // задержка перед исчезнованием сообщения
    className: 'toast-body alert-success font-weight-bold' // классы оформления блока сообщения
}

const TEMPLATE = `<div class="toast" id="${Default.id}" role="alert" data-delay="${Default.millisecondsDelay}" 
style="position: fixed; bottom: 15px; right: 20px; min-width: 250px;">
    <div class="${Default.className}">
        Операция выполнена успешно
    </div>
</div>`

class SuccessToast {
    constructor() {
        this._createToastElement();

    }

    show() {
        $('#' + Default.id).toast('show');
    }

    _createToastElement() {
        let existing = document.getElementById(Default.id);
        if (existing)
            return;

        let element = _common__WEBPACK_IMPORTED_MODULE_0__.default.toElement(TEMPLATE);
        document.body.appendChild(element);
    }

    _initToast() {
        $('#' + Default.id).toast();
    }
}

/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (SuccessToast);

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		if(__webpack_module_cache__[moduleId]) {
/******/ 			return __webpack_module_cache__[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = __webpack_modules__;
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/compat get default export */
/******/ 	(() => {
/******/ 		// getDefaultExport function for compatibility with non-harmony modules
/******/ 		__webpack_require__.n = (module) => {
/******/ 			var getter = module && module.__esModule ?
/******/ 				() => module['default'] :
/******/ 				() => module;
/******/ 			__webpack_require__.d(getter, { a: getter });
/******/ 			return getter;
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/define property getters */
/******/ 	(() => {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = (exports, definition) => {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/global */
/******/ 	(() => {
/******/ 		__webpack_require__.g = (function() {
/******/ 			if (typeof globalThis === 'object') return globalThis;
/******/ 			try {
/******/ 				return this || new Function('return this')();
/******/ 			} catch (e) {
/******/ 				if (typeof window === 'object') return window;
/******/ 			}
/******/ 		})();
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	(() => {
/******/ 		__webpack_require__.o = (obj, prop) => Object.prototype.hasOwnProperty.call(obj, prop)
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	(() => {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = (exports) => {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/jsonp chunk loading */
/******/ 	(() => {
/******/ 		// no baseURI
/******/ 		
/******/ 		// object to store loaded and loading chunks
/******/ 		// undefined = chunk not loaded, null = chunk preloaded/prefetched
/******/ 		// Promise = chunk loading, 0 = chunk loaded
/******/ 		var installedChunks = {
/******/ 			"main": 0
/******/ 		};
/******/ 		
/******/ 		var deferredModules = [
/******/ 			["./wwwroot/js/src/main.js","vendor"]
/******/ 		];
/******/ 		// no chunk on demand loading
/******/ 		
/******/ 		// no prefetching
/******/ 		
/******/ 		// no preloaded
/******/ 		
/******/ 		// no HMR
/******/ 		
/******/ 		// no HMR manifest
/******/ 		
/******/ 		var checkDeferredModules = () => {
/******/ 		
/******/ 		};
/******/ 		function checkDeferredModulesImpl() {
/******/ 			var result;
/******/ 			for(var i = 0; i < deferredModules.length; i++) {
/******/ 				var deferredModule = deferredModules[i];
/******/ 				var fulfilled = true;
/******/ 				for(var j = 1; j < deferredModule.length; j++) {
/******/ 					var depId = deferredModule[j];
/******/ 					if(installedChunks[depId] !== 0) fulfilled = false;
/******/ 				}
/******/ 				if(fulfilled) {
/******/ 					deferredModules.splice(i--, 1);
/******/ 					result = __webpack_require__(__webpack_require__.s = deferredModule[0]);
/******/ 				}
/******/ 			}
/******/ 			if(deferredModules.length === 0) {
/******/ 				__webpack_require__.x();
/******/ 				__webpack_require__.x = () => {
/******/ 		
/******/ 				}
/******/ 			}
/******/ 			return result;
/******/ 		}
/******/ 		__webpack_require__.x = () => {
/******/ 			// reset startup function so it can be called again when more startup code is added
/******/ 			__webpack_require__.x = () => {
/******/ 		
/******/ 			}
/******/ 			chunkLoadingGlobal = chunkLoadingGlobal.slice();
/******/ 			for(var i = 0; i < chunkLoadingGlobal.length; i++) webpackJsonpCallback(chunkLoadingGlobal[i]);
/******/ 			return (checkDeferredModules = checkDeferredModulesImpl)();
/******/ 		};
/******/ 		
/******/ 		// install a JSONP callback for chunk loading
/******/ 		var webpackJsonpCallback = (data) => {
/******/ 			var [chunkIds, moreModules, runtime, executeModules] = data;
/******/ 			// add "moreModules" to the modules object,
/******/ 			// then flag all "chunkIds" as loaded and fire callback
/******/ 			var moduleId, chunkId, i = 0, resolves = [];
/******/ 			for(;i < chunkIds.length; i++) {
/******/ 				chunkId = chunkIds[i];
/******/ 				if(__webpack_require__.o(installedChunks, chunkId) && installedChunks[chunkId]) {
/******/ 					resolves.push(installedChunks[chunkId][0]);
/******/ 				}
/******/ 				installedChunks[chunkId] = 0;
/******/ 			}
/******/ 			for(moduleId in moreModules) {
/******/ 				if(__webpack_require__.o(moreModules, moduleId)) {
/******/ 					__webpack_require__.m[moduleId] = moreModules[moduleId];
/******/ 				}
/******/ 			}
/******/ 			if(runtime) runtime(__webpack_require__);
/******/ 			parentChunkLoadingFunction(data);
/******/ 			while(resolves.length) {
/******/ 				resolves.shift()();
/******/ 			}
/******/ 		
/******/ 			// add entry modules from loaded chunk to deferred list
/******/ 			if(executeModules) deferredModules.push.apply(deferredModules, executeModules);
/******/ 		
/******/ 			// run deferred modules when all chunks ready
/******/ 			return checkDeferredModules();
/******/ 		}
/******/ 		
/******/ 		var chunkLoadingGlobal = globalThis["webpackChunkProfOsmotr_Web"] = globalThis["webpackChunkProfOsmotr_Web"] || [];
/******/ 		var parentChunkLoadingFunction = chunkLoadingGlobal.push.bind(chunkLoadingGlobal);
/******/ 		chunkLoadingGlobal.push = webpackJsonpCallback;
/******/ 	})();
/******/ 	
/************************************************************************/
/******/ 	// run startup
/******/ 	return __webpack_require__.x();
/******/ })()
;
//# sourceMappingURL=main.js.map