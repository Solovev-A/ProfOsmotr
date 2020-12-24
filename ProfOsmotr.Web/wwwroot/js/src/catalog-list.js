import Util from './util/common';
import CustomBootstrapModal from './util/custom-modal';
import CustomDataTable from './util/custom-datatable';
import SuccessToast from './util/success-toast';
import { DefaultChecks } from './util/custom-validation';


function initCatalogList() {
    const URI_API_CATALOG_UPDATE = '/api/catalog/update';

    const SELECTOR_EXAMINATION_NAME = '.js-catalog-examination';
    const SELECTOR_SERVICE_CODE = '.js-catalog-code';
    const SELECTOR_SERVICE_FULL_NAME = '.js-catalog-name';
    const SELECTOR_SERVICE_PRICE = '.js-catalog-price';
    const SELECTOR_SERVICE_GROUP = '.js-catalog-group';
    const SELECTOR_CATALOG_UPDATE_TIME = '.js-catalog-update-time';

    const ID_TABLE_CATALOG = 'Catalog';

    const ATTR_DATA_EXAMINATION_ID = 'data-examination-id';

    const successToast = new SuccessToast();

    const modalOptions = {
        title: (model) => `Изменение услуги для обследования: ${model.name}`,
        data: [
            {
                id: 'service-code',
                path: 'code',
                label: 'Код услуги',
                type: 'input-text',
                validityCheck: DefaultChecks.requiredText20
            },
            {
                id: 'service-name',
                path: 'fullName',
                label: 'Наименование услуги',
                type: 'textarea',
                validityCheck: DefaultChecks.requiredText500
            },
            {
                id: 'service-price',
                path: 'price',
                label: 'Цена',
                type: 'input-text',
                validityCheck: DefaultChecks.price
            },
            {
                id: 'service-availability-group',
                path: 'serviceAvailabilityGroupId',
                label: 'Доступность',
                type: 'select',
                options: [
                    new Option('Доступна', '1'),
                    new Option('Недоступна', '2'),
                    new Option('Включена', '3')
                ]
            }
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: async (model) => await onUpdateCatalogItem(model)
            }
        ]
    };
    const serviceModal = new CustomBootstrapModal(modalOptions);


    const dataTableConfig = {
        tableId: ID_TABLE_CATALOG,
        advanced: {
            buttons: [
                {
                    extend: 'selectedSingle',
                    text: 'Редактировать',
                    action: function (e, dt, button, config) {
                        let tableRow = dt.row({ selected: true }).node();
                        let model = getModel(tableRow);
                        serviceModal.show(model);
                    }
                }
            ]
        }
    };
    const table = new CustomDataTable(dataTableConfig);


    function getModel(tableRow) {
        return {
            id: +tableRow.dataset.examinationId,
            name: tableRow.querySelector(SELECTOR_EXAMINATION_NAME).innerText,
            code: tableRow.querySelector(SELECTOR_SERVICE_CODE).innerText,
            fullName: tableRow.querySelector(SELECTOR_SERVICE_FULL_NAME).innerText,
            price: tableRow.querySelector(SELECTOR_SERVICE_PRICE).innerText,
            serviceAvailabilityGroupId: tableRow.querySelector(SELECTOR_SERVICE_GROUP).dataset.groupId
        };
    }

    function renderCatalogChanges(updated) {
        const tableRow = document.querySelector(`tr[${ATTR_DATA_EXAMINATION_ID}="${updated.orderExaminationId}"]`);

        tableRow.querySelector(SELECTOR_SERVICE_CODE).innerText = updated.code;
        tableRow.querySelector(SELECTOR_SERVICE_FULL_NAME).innerText = updated.fullName;
        tableRow.querySelector(SELECTOR_SERVICE_PRICE).innerText = Util.toCurrencyString(updated.price);
        tableRow.querySelector(SELECTOR_CATALOG_UPDATE_TIME).innerText = new Date(updated.updateTime).toLocaleString();

        const groupCell = tableRow.querySelector(SELECTOR_SERVICE_GROUP);
        groupCell.innerText = updated.serviceAvailabilityGroupName;
        groupCell.dataset.groupId = updated.serviceAvailabilityGroupId;
    }

    async function onUpdateCatalogItem(model) {
        const requestData = {
            orderExaminationId: model.id,
            fullName: model.fullName,
            code: model.code,
            price: Util.customParseFloat(model.price),
            serviceAvailabilityGroupId: +model.serviceAvailabilityGroupId
        }

        const service = await Util.postData(URI_API_CATALOG_UPDATE, requestData);
        if (service) {
            renderCatalogChanges(service);
            serviceModal.hide();
            successToast.show();
        }
        else {
            serviceModal.enableButtons();
        }
    }
}

export default initCatalogList;