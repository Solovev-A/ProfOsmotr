import Util from './common';
import { addValidation, validate, resetValidation } from './custom-validation';

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

const CLASSNAME_DISPLAY_NONE = 'd-none';
const CLASSNAME_CUSTOM_MODAL = 'js-custom-modal';
const CLASSNAME_MODAL_TITLE = 'modal-title';
const CLASSNAME_MODAL_BODY = 'modal-body';
const CLASSNAME_MODAL_FOOTER = 'modal-footer';
const CLASSNAME_BUTTON_DEFAULT = 'btn btn-primary';
const CLASSNAME_FORM_CONTROL = 'form-control';

const EVENT_MODAL_SHOW = 'show';
const EVENT_MODAL_HIDE = 'hide';
const EVENT_MODAL_HIDDEN = 'hidden.bs.modal';

const ID_DATA_ATTR = 'data-custom-modal-id';
const ID_DATA_ATTR_DATASET = 'customModalId';
const RENDER_DATA_ATTR_DATASET = 'customModalRendered';
const RENDER_DATA_ATTR_VALUE = 'true';
const BASE_MODAL_ID = 'custom-modal-';

const TEMPLATE_MODAL = `<div class="modal" data-backdrop="static" tabindex="-1" role="dialog" aria-hidden="true">
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
const TEMPLATE_FORM_ROW = '<div class="form-group"></div>';

class CustomBootstrapModal {
    constructor(options) {
        this._modalElement = null;
        this._form = null;
        this._dataElements = null;
        this.model = null;
        this.buttonsData = [];
        this.options = options;

        this._build();
    };


    // Public

    enableButtons() {
        for (let buttonData of this.buttonsData) {
            buttonData.element.disabled = false;
            if (buttonData.visibility === true || Util.isEmpty(this.model) || buttonData.visibility(this.model))
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
        this.model = $.extend(true, {}, model);
        this._setTitle();
        if (model)
            this._seedData();
        this.enableButtons();
        $(`#${this.getId()}`).modal(EVENT_MODAL_SHOW);
    };


    // Private

    _build() {
        this._createModalElement();
        this._createDataInputs();
        this._createButtons();
        this._addModalHiddenEventListener();
    };

    _addModalHiddenEventListener() {
        let _this = this;
        $(`#${this.getId()}`).on(EVENT_MODAL_HIDDEN, function () {
            _this._resetForm();
            resetValidation(_this._dataElements);
            _this.model = null;
        });
    };

    _createModalElement() {
        this._modalElement = Util.toElement(TEMPLATE_MODAL);
        this._modalElement.id = this.getId();
        document.body.appendChild(this._modalElement);
    };

    _createDataInputs() {
        if (!this.options.data)
            return;

        this._form = document.createElement('form');
        for (let dataObj of this.options.data) {
            let dataElement = this._createDataElement(dataObj);
            this._form.appendChild(dataElement);
        }

        this._addFormEventListeners();

        let modalBody = this._modalElement.querySelector('.' + CLASSNAME_MODAL_BODY);
        modalBody.appendChild(this._form);
    };

    _addFormEventListeners() {
        let _this = this;
        this._dataElements = this._form.querySelectorAll(`.${CLASSNAME_CUSTOM_MODAL}`);
        for (let item of this._dataElements) {
            item.addEventListener('keyup', onElementValueChanged);
            item.addEventListener('change', onElementValueChanged);
        }

        function onElementValueChanged(e) {
            let element = e.target;
            let id = element.dataset[ID_DATA_ATTR_DATASET];
            let path = _this._getPathById(id);
            if (path && element.dataset[RENDER_DATA_ATTR_DATASET] !== RENDER_DATA_ATTR_VALUE) {
                _this._updateModel(path, element.value);
            }
        };
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

        let _this = this;

        button.addEventListener('click', async function (e) {
            e.preventDefault();
            e.target.disabled = true;
            _triggerChange();
            validate(_this._dataElements);
            if (!_this._form.checkValidity()) {
                e.target.disabled = false;
                return;
            }
            await config.action(_this.model);
        });
        return button;

        function _triggerChange() {
            let change = new Event('change');
            _this._dataElements.forEach((val, i, arr) => val.dispatchEvent(change));
        }
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

    _seedData() {
        for (let dataObj of this.options.data) {
            let value = this._getValue(dataObj.path);
            if (value === null || value === undefined)
                continue;
            let element = this._modalElement.querySelector(`[${ID_DATA_ATTR}="${dataObj.id}"]`);
            element.value = dataObj.render ? dataObj.render(value) : value;
        }
    };

    _resetForm() {
        this._form.reset();
    };
}

export default CustomBootstrapModal;