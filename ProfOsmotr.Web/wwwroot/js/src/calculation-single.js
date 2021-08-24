import Util from './util/common';
import { addValidation, validate, DefaultChecks } from './util/custom-validation';
import { initOrderItemsSelect } from './util/profession-input';


async function initCalculationSingle() {
    const SELECTOR_ORDER_ITEMS_SELECT = '#OrderItems';
    const SELECTOR_CREATE_CALCULATION_BUTTON = '#createSingleCalculation';
    const SELECTOR_PROFESSION_NAME_INPUT = '#ProfessionName';
    const SELECTOR_IS_WOMAN_CHECKBOX = '#IsWoman';
    const SELECTOR_IS_OVER_40_CHECKBOX = '#IsOver40';

    const URI_CREATE_CALCULATION_API = '/api/calculation/create';
    const URI_CALCULATION_RESULT_BASE = '/Calculation/Result/';

    const professionNameInput = document.querySelector(SELECTOR_PROFESSION_NAME_INPUT);

    await initOrderItemsSelect();
    addFormValidation();
    addEventListeners();

    function addFormValidation() {
        addValidation(professionNameInput, DefaultChecks.requiredText70);
    }

    function addEventListeners() {
        var submitBtn = document.querySelector(SELECTOR_CREATE_CALCULATION_BUTTON);
        submitBtn.addEventListener('click', onSubmit);
    }

    async function onSubmit(e) {
        e.preventDefault();

        const inputs = [professionNameInput];
        validate(inputs);
        if (!document.singleCalc.checkValidity())
            return;

        const calculationSource = getCalculationSource();

        const calculation = await Util.postData(URI_CREATE_CALCULATION_API, calculationSource);
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


export default initCalculationSingle;