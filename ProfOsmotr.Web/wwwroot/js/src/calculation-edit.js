import Util from './util/common';
import { addValidation, validate, DefaultChecks } from './util/custom-validation';


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
            addValidation(input, DefaultChecks.price);
        });
    }

    function processAmountInputs() {
        let arr = Array.from(amountInputs);
        arr.forEach(input => {
            input.addEventListener('keyup', onAmountChanged);
            addValidation(input, DefaultChecks.positiveInteger);
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
        sum.value = Util.toCurrencyString(calculateSum(input, amountInput));

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
        sum.value = Util.toCurrencyString(calculateSum(priceInput, input));

        calculateTotalSum();
    }

    function calculateSum(priceInput, amountInput) {
        return Util.customParseFloat(priceInput.value) * amountInput.value;
    }

    function getResultId(formInput) {
        return formInput.parentNode.parentNode.id;
    }

    function isAmountInputInvalid(input) {
        return DefaultChecks.positiveInteger.isInvalid(input);
    }

    function isPriceInputInvalid(input) {
        return DefaultChecks.price.isInvalid(input);
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

        validate([priceInput, amountInput]);
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
        validate(priceInputs);
        validate(amountInputs);
        if (!document.Results.checkValidity())
            return;

        let data = createUpdateCalculationRequestData();

        const calculation = await Util.postData(URI_UPDATE_CALCULATION_API, data);
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

        let price = Util.customParseFloat(formGroup.querySelector(SELECTOR_RESULT_ITEM_BASE + NAME_RESULT_ITEM_PRICE).value);
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
                totalSum += Util.customParseFloat(sumElements[i].value);
            }
        }
        document.querySelector(SELECTOR_TOTAL_SUM).innerText = Util.toCurrencyString(totalSum);
    }

    function addSaveBtnEventListener() {
        const saveBtn = document.querySelector(SELECTOR_SAVE_CALCULATION_BUTTON);
        saveBtn.addEventListener('click', onSaveChanges);
    }
}

export default initCalculationEdit;