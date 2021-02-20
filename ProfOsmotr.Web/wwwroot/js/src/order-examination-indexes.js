import Util from './util/common';
import { DefaultChecks } from './util/custom-validation';
import ModalDataTable from './util/modal/modal-datatable';
import ModalForm from './util/modal/modal-form';


const URI_API_ORDER_INDEXES = '/api/order/getIndexes';
const URI_API_INDEX_CREATE = '/api/order/examination/{0}/index'; // POST
const URI_API_INDEX_DELETE = '/api/order/index'; // DELETE with /{id}
const URI_API_INDEX_UPDATE = '/api/order/index'; // POST with /{id}


class OrderExaminationIndexes {
    constructor(successToast) {
        this._indexModal = new ModalForm(this._getIndexConfig());
        this.mainModal = new ModalDataTable(this._getMainConfig());
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
                            render: (data, type, row) => Util.escapeHTML(data)
                        },
                        {
                            data: 'unitOfMeasure',
                            render: (data, type, row) => Util.escapeHTML(data)
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
                    validityCheck: DefaultChecks.requiredText70
                },
                {
                    id: 'unitOfMeasure',
                    path: 'unitOfMeasure',
                    label: 'Единицы измерения',
                    type: 'input-text',
                    validityCheck: DefaultChecks.requiredText70
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
        const response = await Util.deleteResource(`${URI_API_INDEX_DELETE}/${id}`);
        if (response) {
            this.mainModal.seedTable();
        }
    }

    async _onIndexSave(model) {
        const url = model.editing ? `${URI_API_INDEX_UPDATE}/${model.id}` : getIndexCreateURL(this.examinationdId);
        const response = await Util.postData(url, model);

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

export default OrderExaminationIndexes;