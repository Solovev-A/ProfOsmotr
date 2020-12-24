import Util from './util/common';
import CustomBootstrapModal from './util/custom-modal';
import CustomDataTable from './util/custom-datatable';
import SuccessToast from './util/success-toast';


const URI_API_REGISTER_REQUESTS_NEW = '/api/clinic/newRequests';
const URI_API_REGISTER_REQUESTS_PROCESSED = '/api/clinic/processedRequests';
const URI_API_REGISTER_REQUEST_MANAGE = '/api/clinic/manageRequest';


class RegisterRequestsListPage {
    constructor() {
        this.successToast = new SuccessToast();
        this._createDataTables();
        this._createRegisterRequestsModal();
    }

    // Private 

    _createDataTables() {
        const columnsBase = [
            {
                data: 'creationTime',
                name: 'CreationTime',
                render: (data, type, row) => new Date(data).toLocaleString(),
                searchable: false
            },
            {
                data: 'shortName',
                name: 'ShortName',
                render: (data, type, row) => Util.escapeHTML(data)
            },
            {
                data: 'sender.name',
                name: 'Sender.Name',
                render: (data, type, row) => Util.escapeHTML(data)
            }
        ];

        const approvedStatusColumn = {
            data: 'approved',
            name: 'Approved',
            render: (data, type, row) => data ? 'Одобрена' : 'Отклонена',
            searchable: false
        };

        const tableButtons = [
            {
                extend: 'selectedSingle',
                text: 'Просмотр',
                action: (e, dt, button, config) => {
                    let model = dt.row({ selected: true }).data();
                    this.registerRequestModal.show(model);
                }
            }
        ];

        const newRequestsTableConfig = {
            tableId: 'NewRegisterRequests',
            ajaxUrl: URI_API_REGISTER_REQUESTS_NEW,
            serverSide: true,
            advanced: {
                columns: columnsBase,
                buttons: tableButtons
            }
        }

        this.newRequestsDataTable = new CustomDataTable(newRequestsTableConfig).getTable();

        const processedRequestsTableColumns = columnsBase.slice();
        processedRequestsTableColumns.push(approvedStatusColumn);

        const processedRequestTableConfig = {
            tableId: 'ProcessedRegisterRequests',
            ajaxUrl: URI_API_REGISTER_REQUESTS_PROCESSED,
            serverSide: true,
            advanced: {
                columns: processedRequestsTableColumns,
                buttons: tableButtons
            }
        }

        this.processedRequestsDataTable = new CustomDataTable(processedRequestTableConfig).getTable();
    }

    _createRegisterRequestsModal() {
        const config = {
            title: 'Информация о заявке',
            readonly: true,
            data: [
                {
                    id: 'date',
                    path: 'creationTime',
                    label: 'Дата',
                    type: 'input-text',
                    render: (value) => new Date(value).toLocaleString()
                },
                {
                    id: 'full-name',
                    path: 'fullName',
                    label: 'Полное наименование',
                    type: 'input-text'
                },
                {
                    id: 'short-name',
                    path: 'shortName',
                    label: 'Сокращенное наименование',
                    type: 'input-text'
                },
                {
                    id: 'address',
                    path: 'address',
                    label: 'Адрес',
                    type: 'input-text'
                },
                {
                    id: 'phone',
                    path: 'phone',
                    label: 'Телефон',
                    type: 'input-text'
                },
                {
                    id: 'email',
                    path: 'email',
                    label: 'Электронная почта',
                    type: 'input-text'
                },
                {
                    id: 'contact-person',
                    path: 'sender.name',
                    label: 'Контактное лицо',
                    type: 'input-text',
                    render: (data) => `${data.name}, ${data.position}`
                },
                {
                    id: 'username',
                    path: 'sender.userName',
                    label: 'Username',
                    type: 'input-text'
                }
            ],
            buttons: [
                {
                    text: 'Одобрить',
                    action: (model) => this._manageRegisterRequest(model, true),
                    className: 'btn btn-success',
                    visibility: (model) => !model.approved
                },
                {
                    text: 'Отклонить',
                    action: (model) => this._manageRegisterRequest(model, false),
                    className: 'btn btn-danger',
                    visibility: (model) => !model.processed
                }
            ]
        }

        this.registerRequestModal = new CustomBootstrapModal(config);
    }

    async _manageRegisterRequest(model, approved) {
        const data = {
            id: model.id,
            approved
        };

        const result = await Util.postData(URI_API_REGISTER_REQUEST_MANAGE, data);
        if (result) {
            this.registerRequestModal.hide();
            this.successToast.show();
            this.newRequestsDataTable.ajax.reload();
            this.processedRequestsDataTable.ajax.reload();
        }
    }
}

export default RegisterRequestsListPage;