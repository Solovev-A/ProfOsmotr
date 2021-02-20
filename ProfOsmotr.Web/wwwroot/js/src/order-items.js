import Util from './util/common';
import ModalForm from './util/modal/modal-form';
import CustomDataTable from './util/custom-datatable';
import SuccessToast from './util/success-toast';
import { DefaultChecks } from './util/custom-validation';


const URI_API_ORDER_ITEMS = '/api/order/getOrder';
const URI_API_ORDER_ITEM_CREATE = '/api/order/addItem';
const URI_API_ORDER_ITEM_UPDATE = '/api/order/updateItem';
const URI_API_ORDER_ITEM_REMOVE = '/api/order/deleteItem';
const URI_API_ORDER_EXAMINATIONS = '/api/order/getExaminationsMin';

const ID_SELECT_EXAMINATIONS = 'examinations';
const ID_ANNEX_ID = 'annexId';
const ID_KEY = 'key';

const SELECTOR_EXAMINATIONS = `select[data-custom-modal-id="${ID_SELECT_EXAMINATIONS}"`;
const SELECTOR_ANNEX_ID = `select[data-custom-modal-id="${ID_ANNEX_ID}"`;
const SELECTOR_KEY = `input[data-custom-modal-id="${ID_KEY}"`;


class OrderItemsPage {
    constructor() {
        this.succesToast = new SuccessToast();
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
        this._orderItems = await Util.getData(URI_API_ORDER_ITEMS);

        let orderExaminations = await Util.getData(URI_API_ORDER_EXAMINATIONS);
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
                        render: (data, type, row) => Util.escapeHTML(data)
                    },
                    {
                        data: 'name',
                        render: (data, type, row) => Util.escapeHTML(data)
                    },
                    {
                        data: 'examinations',
                        render: (data, type, row) => data.map(ex => Util.escapeHTML(ex.name)).join('<br>')
                    }
                ],
                rowGroup: {
                    dataSrc: 'annex.name'
                },
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

        this.orderTable = new CustomDataTable(config).getTable();
    }

    _createModal() {
        let config = {
            title: (model) => model.editing ? `Редактирование пункта ${model.key}` : 'Создание нового пункта',
            data: [
                {
                    id: 'annexId',
                    path: 'annex.id',
                    label: 'Приложение',
                    type: 'select',
                    options: this._orderItems.annexes.map(annex => new Option(`Приложение ${annex.id}`, annex.id))
                },
                {
                    id: 'key',
                    path: 'key',
                    label: 'Пункт',
                    type: 'input-text',
                    validityCheck: DefaultChecks.requiredText70
                },
                {
                    id: 'name',
                    path: 'name',
                    label: 'Название',
                    type: 'textarea',
                    validityCheck: DefaultChecks.requiredText500
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

        this.orderItemModal = new ModalForm(config);
        this._initSelect2();
    }

    _getDataTableData() {
        return this._orderItems.annexes
            .map(annex => annex.orderItems.map(item => this._convertToTableData(item)))
            .flat();
    }

    _convertToTableData(orderItemResource) {
        return {
            id: orderItemResource.id,
            annex: {
                id: orderItemResource.annexId,
                name: `Приложение ${orderItemResource.annexId}`
            },
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
            const updatedItem = await Util.postData(URI_API_ORDER_ITEM_UPDATE, {
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
            const newItem = await Util.postData(URI_API_ORDER_ITEM_CREATE, {
                annexId: parseInt(model.annex.id),
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
            const response = await Util.postData(URI_API_ORDER_ITEM_REMOVE, itemData.id);
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
            document.querySelector(SELECTOR_ANNEX_ID),
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

export default OrderItemsPage;