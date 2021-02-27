import Util from '../common';
import CustomBootstrapModalBase from './custom-modal-base';
import { addValidation, validate, resetValidation } from '../custom-validation';


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


class ModalForm extends CustomBootstrapModalBase {
    constructor(options) {
        super(options);
    };


    // Overrides

    _getContentValidationResult() {
        this._dataElements.forEach(element => element.dispatchEvent(new Event('change')));
        validate(this._dataElements);
        return this._form.checkValidity()
    }

    _getModalBodyContent() {
        return this._createForm();
    }

    _onModalHidden() {
        super._onModalHidden();

        this._form.reset();
        resetValidation(this._dataElements);
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
            return Util.toElement(html);
        };

        function _getFormRow() {
            return Util.toElement(TEMPLATE_FORM_ROW);
        };

        function _getCheckbox() {
            const template = `<div class="form-group form-check">    
                                <label class="form-check-label">${data.label}</label>
                              </div>`;
            const container = Util.toElement(template);

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
            addValidation(element, validityCheck);
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

export default ModalForm;