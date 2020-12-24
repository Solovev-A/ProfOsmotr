import Util from './util/common';
import CustomBootstrapModal from './util/custom-modal';
import CustomDataTable from './util/custom-datatable';
import SuccessToast from './util/success-toast';
import { ValidityCheck, DefaultChecks } from './util/custom-validation';


function initUserList() {
    const URI_API_USERS_LIST = '/api/user/list';
    const URI_API_USER_CREATE = '/api/user/create';
    const URI_API_USER_UPDATE_BASE = '/api/user/update';

    const isGlobal = document.getElementById('UsersList').dataset.global == 'True';
    const successToast = new SuccessToast();


    // Data объекты для Modal

    const usernameData = {
        id: 'username',
        path: 'username',
        type: 'input-text',
        label: 'Имя пользователя',
        validityCheck: DefaultChecks.username
    };

    const passwordData = {
        id: 'password',
        path: 'password',
        type: 'input-password',
        label: 'Пароль',
        validityCheck: DefaultChecks.password
    };

    const confirmPasswordData = getConfirmPasswordData('#custom-modal-1 input[data-custom-modal-id="password"]');
    const confirmPasswordData2 = getConfirmPasswordData('#custom-modal-3  input[data-custom-modal-id="password"]');

    const nameData = {
        id: 'name',
        path: 'name',
        type: 'input-text',
        label: 'ФИО',
        validityCheck: DefaultChecks.requiredText70
    };
    const positionData = {
        id: 'position',
        path: 'position',
        type: 'input-text',
        label: 'Должность',
        validityCheck: DefaultChecks.requiredText70
    };


    //
    // Создание пользователя
    //
    const createUserModalConfig = {
        title: 'Создание нового пользователя',
        data: [
            usernameData,
            passwordData,
            confirmPasswordData,
            nameData,
            positionData,
            getRoleData()
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: async (model) => await onUserCreate(model)
            }
        ]
    }
    const createUserModal = new CustomBootstrapModal(createUserModalConfig);


    //
    // Редактирование профиля
    //
    const editProfileModalConfig = {
        title: (model) => `Редактирование профиля пользователя ${model.username}`,
        data: [
            nameData,
            positionData,
            getRoleData()
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: async (model) => await onUserProfileSave(model)
            }
        ]
    }
    const editProfileModal = new CustomBootstrapModal(editProfileModalConfig);


    //
    // Изменение пароля
    //
    const changePasswordModalConfig = {
        title: (model) => `Задать новый пароль для пользователя ${model.username}`,
        data: [
            passwordData,
            confirmPasswordData2
        ],
        buttons: [
            {
                text: 'Сохранить',
                action: async (model) => await onPasswordChange(model)
            }
        ]
    }
    const changePasswordModal = new CustomBootstrapModal(changePasswordModalConfig);


    //
    // Таблица
    //
    const dataTableConfig = {
        tableId: 'UsersList',
        ajaxUrl: URI_API_USERS_LIST,
        serverSide: true,
        scroll: true,
        advanced: {
            columns: [
                {
                    data: 'id',
                    name: 'Id',
                    searchable: false,
                    orderable: true
                },
                {
                    data: 'name',
                    name: 'Name',
                    render: (data, type, row) => Util.escapeHTML(data),
                    searchable: true,
                    orderable: true
                },
                {
                    data: 'role.name',
                    name: 'Role.Name',
                    render: (data, type, row) => Util.escapeHTML(data),
                    searchable: true,
                    orderable: true
                }
            ],
            buttons: [
                {
                    text: 'Создать',
                    action: function (e, dt, button, config) {
                        createUserModal.show(null);
                    }
                },
                {
                    extend: 'selectedSingle',
                    text: 'Изменить профиль',
                    action: function (e, dt, button, config) {
                        let model = dt.row({ selected: true }).data();
                        editProfileModal.show(model);
                    }
                },
                {
                    extend: 'selectedSingle',
                    text: 'Изменить пароль',
                    action: function (e, dt, button, config) {
                        let model = dt.row({ selected: true }).data();
                        changePasswordModal.show(model);
                    }
                }
            ]
        }
    };
    const clinicCol = {
        data: 'clinicShortName',
        name: 'ClinicShortName',
        render: (data, type, row) => Util.escapeHTML(data),
        searchable: true,
        orderable: true
    };

    if (isGlobal)
        dataTableConfig.advanced.columns.push(clinicCol);

    const table = new CustomDataTable(dataTableConfig);


    // Обработчики кнопок модальных окон

    async function onUserCreate(model) {
        const data = {
            name: model.name,
            position: model.position,
            username: model.username,
            password: model.password,
            roleId: +model.role.id
        };

        const result = await Util.postData(URI_API_USER_CREATE, data);
        processResult(createUserModal, result);
    }

    async function onUserProfileSave(model) {
        const data = {
            name: model.name,
            position: model.position,
            roleId: +model.role.id
        };
        const result = await Util.postData(`${URI_API_USER_UPDATE_BASE}/${model.id}`, data);
        processResult(editProfileModal, result);
    }

    async function onPasswordChange(model) {
        const data = {
            password: model.password
        }
        const result = await Util.postData(`${URI_API_USER_UPDATE_BASE}/${model.id}`, data);
        processResult(changePasswordModal, result);
    }


    // Helpers

    function getConfirmPasswordData(inputToConfirmSelector) {
        return {
            id: 'password-confirmation',
            path: null,
            type: 'input-password',
            label: 'Повторите пароль',
            validityCheck: getConfirmPasswordValidityCheck(inputToConfirmSelector)
        }

        function getConfirmPasswordValidityCheck(inputToConfirmSelector) {
            return new ValidityCheck(
                'Пароли не совпадают',
                (input) => input.value !== document.querySelector(inputToConfirmSelector).value);
        }
    }

    function getRoleOptions() {
        let options = [
            new Option('Сотрудник', '3'),
            new Option('Модератор клиники', '2'),
            new Option('Заблокированный', '4')
        ];
        if (isGlobal)
            options.push(new Option('Администратор сайта', '1'));
        return options;
    }

    function getRoleData() {
        return {
            id: 'roleId',
            path: 'role.id',
            type: 'select',
            label: 'Тип аккаунта',
            options: getRoleOptions()
        };
    }

    function processResult(modal, result) {
        if (result) {
            modal.hide();
            successToast.show();
            table.ajaxReload();
        }
        else {
            modal.enableButtons();
        }
    }
}


export default initUserList;