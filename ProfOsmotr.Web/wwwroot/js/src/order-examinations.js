import Util from './util/common';
import CustomBootstrapModal from './util/custom-modal';
import CustomDataTable from './util/custom-datatable';
import SuccessToast from './util/success-toast';
import { DefaultChecks } from './util/custom-validation';


const URI_API_ORDER_EXAMINATIONS_DATA = '/api/order/getExaminations';
const URI_API_ORDER_EXAMINATION_CREATE = '/api/order/addExamination';
const URI_API_ORDER_EXAMINATION_UPDATE = '/api/order/updateExamination';

class OrderExaminationsPage {
    constructor() {
        this.succesToast = new SuccessToast();
    }


    // Static

    static init() {
        return (async () => {
            const _this = new OrderExaminationsPage();

            await _this._getExaminationsData();

            _this._createDataTable()
            _this._createExaminationModal();
        })();
    }


    // Private

    async _getExaminationsData() {
        this._examinationsData = await Util.getData(URI_API_ORDER_EXAMINATIONS_DATA);
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
                        render: (data, type, row) => Util.escapeHTML(data)
                    },
                    {
                        data: 'targetGroup.name',
                        render: (data, type, row) => Util.escapeHTML(data)
                    },
                    {
                        data: 'defaultServiceDetails.fullName',
                        render: (data, type, row) => Util.escapeHTML(data)
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
                    }
                ]
            }
        }

        this.examinationsTable = new CustomDataTable(config).getTable();
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
                    validityCheck: DefaultChecks.requiredText500
                },
                {
                    id: 'service-code',
                    path: 'defaultServiceDetails.code',
                    label: 'Код услуги по умолчанию',
                    type: 'input-text',
                    validityCheck: DefaultChecks.requiredText20
                },
                {
                    id: 'service-full-name',
                    path: 'defaultServiceDetails.fullName',
                    label: 'Полное наименование услуги по умолчанию',
                    type: 'textarea',
                    validityCheck: DefaultChecks.requiredText500
                }
            ],
            buttons: [
                {
                    text: 'Сохранить',
                    action: async (model) => await this._onSaveExamination(model)
                }
            ]
        }

        this._examinationModal = new CustomBootstrapModal(config);
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
            defaultServiceDetails: examination.defaultServiceDetails
        };
    }

    async _onSaveExamination(model) {
        const _this = this;

        let data = {
            name: model.name,
            defaultServiceCode: model.defaultServiceDetails.code,
            defaultServiceFullName: model.defaultServiceDetails.fullName,
            targetGroupId: +model.targetGroup.id
        };

        let response;

        if (model.editing) {
            response = await updateExamination();
        } else {
            response = await createExamination();
        }

        if (response) {
            this._examinationModal.hide();
            this.succesToast.show();
        }
        else {
            this._examinationModal.enableButtons();
        }

        async function updateExamination() {
            const updatedExamination = await Util.postData(
                URI_API_ORDER_EXAMINATION_UPDATE,
                Object.assign(data, { id: model.id })
            );

            if (updatedExamination) {
                _this.examinationsTable
                    .row((index, data, node) => data.id === updatedExamination.id)
                    .data(_this._convertToTableData(updatedExamination))
                    .draw();
                return updatedExamination;
            }
        }

        async function createExamination() {
            const newExamination = await Util.postData(URI_API_ORDER_EXAMINATION_CREATE, data);

            if (newExamination) {
                _this.examinationsTable
                    .row
                    .add(_this._convertToTableData(newExamination))
                    .draw();
                return newExamination;
            }
        }
    }
}

export default OrderExaminationsPage;