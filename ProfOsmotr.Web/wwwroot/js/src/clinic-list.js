import Util from './util/common';
import ModalForm from './util/modal/modal-form';
import CustomDataTable from './util/custom-datatable';
import SuccessToast from './util/success-toast';


function initClinicList() {
    const ID_DATATABLE_CLINIC_LIST = 'ClinicsList';

    const URI_API_DATATABLE_CLINIC_LIST = '/api/clinic/list';
    const URI_API_CLINIC_MANAGE = '/api/clinic/manageClinic';

    const successToast = new SuccessToast();

    const currentUserId = document.getElementById(ID_DATATABLE_CLINIC_LIST).dataset.clinic;

    // -----
    // Modal
    // -----
    const modalOptions = {
        title: 'Информация о медицинской организации',
        readonly: true,
        data: [
            {
                id: 'full-name',
                path: 'clinicDetails.fullName',
                type: 'input-text',
                label: 'Полное наименование'
            },
            {
                id: 'short-name',
                path: 'clinicDetails.shortName',
                type: 'input-text',
                label: 'Сокращенное наименование'
            },
            {
                id: 'address',
                path: 'clinicDetails.address',
                type: 'input-text',
                label: 'Адрес'
            },
            {
                id: 'phone',
                path: 'clinicDetails.phone',
                type: 'input-text',
                label: 'Телефон'
            },
            {
                id: 'email',
                path: 'clinicDetails.email',
                type: 'input-text',
                label: 'Электронная почта'
            },
            {
                id: 'block-status',
                path: 'isBlocked',
                type: 'input-text',
                label: 'Статус',
                render: (value) => value === true ? 'Заблокирована' : 'Активна'
            },
        ],
        buttons: [
            {
                text: 'Заблокировать',
                className: 'btn btn-danger',
                action: async (model) => await onManageClinic(model, true),
                visibility: (model) => !model.isBlocked && model.id != currentUserId
            },
            {
                text: 'Разблокировать',
                className: 'btn btn-success',
                action: async (model) => await onManageClinic(model, false),
                visibility: (model) => model.isBlocked && model.id != currentUserId
            }
        ]
    };
    const modal = new ModalForm(modalOptions);


    // ---------
    // Datatable
    // ---------
    const tableOptions = {
        tableId: ID_DATATABLE_CLINIC_LIST,
        ajaxUrl: URI_API_DATATABLE_CLINIC_LIST,
        serverSide: true,
        scroll: true,
        advanced: {
            buttons: [
                {
                    extend: 'selectedSingle',
                    text: 'Просмотр',
                    action: (e, dt, button, config) => {
                        let model = dt.row({ selected: true }).data();
                        modal.show(model);
                    }
                }
            ],
            columns: [
                {
                    data: 'id',
                    name: 'Id',
                    searchable: false
                },
                {
                    data: 'clinicDetails.shortName',
                    name: 'ClinicDetails.ShortName',
                    render: (data, type, row) => Util.escapeHTML(data)
                },
                {
                    data: 'clinicDetails.phone',
                    name: 'ClinicDetails.Phone',
                    render: (data, type, row) => Util.escapeHTML(data),
                    orderable: false
                },
                {
                    data: 'clinicDetails.email',
                    name: 'ClinicDetails.Email',
                    render: (data, type, row) => Util.escapeHTML(data),
                    orderable: false
                },
                {
                    data: 'isBlocked',
                    name: 'IsBlocked',
                    render: (data, type, row) => data ? "Заблокирована" : "Активна",
                    searchable: false,
                    orderable: false
                }
            ]
        }

    };
    const table = new CustomDataTable(tableOptions);

    async function onManageClinic(model, needBlock) {
        const data = {
            id: model.id,
            needBlock
        };
        const result = await Util.postData(URI_API_CLINIC_MANAGE, data);

        if (result) {
            modal.hide();
            table.ajaxReload();
            successToast.show();
        }
        else {
            modal.enableButtons();
        }
    }
}

export default initClinicList;