// Всплывающее в правом нижнем углу сообщение об успешности операции. Основано на Bootstrap toast.

import Util from './common';

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

        let element = Util.toElement(TEMPLATE);
        document.body.appendChild(element);
    }

    _initToast() {
        $('#' + Default.id).toast();
    }
}

export default SuccessToast;