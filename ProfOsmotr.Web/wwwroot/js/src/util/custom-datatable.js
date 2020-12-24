const DOM_FLAG_BUTTON = 'B';
const DOM_FLAG_PAGINATION = 'p';

const PLACEHOLDER_DOM_BUTTON = '{BUTTON}';
const PLACEHOLDER_PAGINATION = '{PAGINATION}';

const localisation = {
    "decimal": "",
    "emptyTable": "Нет данных для отображения",
    "info": "Всего элементов: _TOTAL_",
    "infoEmpty": "",
    "infoFiltered": "(отфильтровано из _MAX_ элементов)",
    "infoPostFix": "",
    "thousands": ",",
    "lengthMenu": "_MENU_ элементов на страницу",
    "loadingRecords": "Загрузка...",
    "processing": "Загрузка...",
    "search": "Поиск:",
    "zeroRecords": "Ничего не найдено",
    "paginate": {
        "first": "Начало",
        "last": "Конец",
        "next": "Вперед",
        "previous": "Назад"
    },
    "aria": {
        "sortAscending": ": сортировка по возрастанию",
        "sortDescending": ": сортировка по убыванию"
    },
    select: {
        rows: {
            _: ''
        }
    }
};

const DefaultDt = {
    config: {
        select: {
            style: 'single'
        },
        language: localisation,
        order: [[0, 'asc']],
        orderMulti: false,
        processing: true
    },
    scrollerConfig: {
        scrollY: 450,
        scrollCollapse: true,
        deferRender: true,
        scroller: true
    },
    serverSideAjax: {
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: (d) => JSON.stringify(d)
    },
    dom: `<'row'<'col-sm-12 col-md-6'${PLACEHOLDER_DOM_BUTTON}><'col-sm-12 col-md-6'f>>
		<'row'<'col-sm-12'tr>>
		<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'${PLACEHOLDER_PAGINATION}>>`
}

// Options:
// --- tableId, // (обязательно) id целевой таблицы
// --- ajaxUrl, // URL для запроса данных в формате dataTables
// --- serverSide : Boolean // Если true, то таблица будет загружаться генерируемыми на сервере фрагментами (требуется настройка на сервере - см. DataTables)
// --- scroll : Boolean // если true, применяются настройки скролла из конфига по умолчанию
// --- advanced : {} // Обычный конфиг dataTables, который будет наложен поверх настроенного, если необходима детальная настройка
// ------ (например, можно переопределить order или задать columns, buttons)
// ------ columns, // (обязательно для ajax) см. dataTables
// --------- (коротко)
// --------- data : string // путь к свойству с данными
// --------- name : string // имя свойства с данными, которое будет отправлено с запросом при serverSide обработке
// --------- render : function(data, type, row) : string // функция обработки данных для отображения
// --------- searchable : Boolean // Если true, данные из колонки будет возможно найти поиском
// --------- orderable : Boolean // Если true, возможна сортировка
// ------ buttons // см. dataTables, коротко:
// --------- text : string // Текст кнопки
// --------- action: function(e, dt, button, config) // действие при клике
// --------- extend: 'selectedSingle' // кнопка активна только, если выделена одна строка в таблице

class CustomDataTable {
    constructor(options) {
        this._options = options;
        this._applyOptions();
        this._table = $('#' + this._options.tableId).DataTable(this._config);
    };

    getTable() {
        return this._table;
    };

    ajaxReload() {
        if (!this._config.ajax) {
            console.log('CustomDataTable: Невозможно перезагрузить данные для этой таблицы посредствам ajax');
            return;
        }
        this._table.ajax.reload();
    }

    _applyOptions() {
        this._config = $.extend(true, {}, DefaultDt.config);

        if (this._options.ajaxUrl) {
            this._config.ajax = {
                url: this._options.ajaxUrl
            }
        }

        if (this._options.scroll) {
            this._config = { ...this._config, ...DefaultDt.scrollerConfig };
        }

        if (this._options.serverSide) {
            if (this._config.ajax) {
                this._config.serverSide = true;
                Object.assign(this._config.ajax, DefaultDt.serverSideAjax);
            } else {
                console.log('CustomDataTable: Для обработки таблиц на сервере, задайте ajaxUrl');
            }
        }

        if (this._options.advanced) {
            Object.assign(this._config, this._options.advanced);
        }

        this._applyDomConfig();
    };

    _applyDomConfig() {
        let buttonDomFlag = '';
        let paginationDomFlag = '';

        if (this._config.buttons) {
            buttonDomFlag = DOM_FLAG_BUTTON;
        }
        if (!this._scroller) {
            paginationDomFlag = DOM_FLAG_PAGINATION;
        }

        this._config.dom = DefaultDt.dom.replace(PLACEHOLDER_DOM_BUTTON, buttonDomFlag)
                                      .replace(PLACEHOLDER_PAGINATION, paginationDomFlag);
    }
}

export default CustomDataTable;