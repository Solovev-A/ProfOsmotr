import Util from '../common';


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
        this._modalElement = Util.toElement(TEMPLATE_MODAL);
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

export default CustomBootstrapModalBase;