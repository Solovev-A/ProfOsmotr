// Простой список. Предназначен для создания модели и представления данных, вводимых пользователем
//
// Options
// --- itemTemplate : string // например: <div>Название: {0}, автор: {1})</div>. Значение в фигурных скобках должно соответствовать индексу объекта, описывающего данные, в массиве data
// --- data : [] // массив объектов, описывающих свойства элемента списка. Значения объектов, индексы которых не представлены в шаблоне, не будут отображаться в документе, но сохранятся в модели
// ------ source : function () // функция, возвращающая значение для добавляемого элемента, например () => anyInput.value;
// ------ path : string // название свойства модели элемента списка
// ------ render : function(source) : string // функция, обрабатывающая отображаемые значения (опционально)
// --- target : string // селектор блока, в котором будет находиться список
// --- reverse : boolean // если true, новые элементы добавляются в список над последним

import Util from './common';

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
        this._buttonRemoveItem = Util.toElement(TEMPLATE_BUTTON_REMOVE_ITEM);

        this._buttonRemoveItem.addEventListener('click', (e) => {
            const button = e.target;
            button.disabled = true;
            this.remove(button.parentElement);
            button.disabled = false;
        })
    }

    _renderNewItem(itemTemplate) {
        let itemElement = Util.toElement(TEMPLATE_ITEM_CONTAINER);
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

export default SimpleList;