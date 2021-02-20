import Util from '../common';
import CustomDataTable from '../custom-datatable';
import CustomBootstrapModalBase from './custom-modal-base';


// Options
// --- title : string | function (model) : void (required) // Заголовок модального окна
// --- columns : [] // Столбцы таблицы
// ------ title : string // Название столбца
// --- buttons : [] // Кнопки модального окна
// ------ text : string (required) // Текст
// ------ action : async function (model) : void (required) // Действие, выполняемое при клике
// ------ className : string // CSS классы
// ------ visibility : function (model) : boolean // Условие видимости кнопки, в зависимости от состояния модели
// --- dataTableOptions : Object // Конфигурация DataTables


const CLASSNAME_TABLE = "table table-bordered table-striped w-100";


class ModalDataTable extends CustomBootstrapModalBase {
    constructor(options) {
        super(options);

        this.options.dataTableOptions.tableId = this._getDataTableId();
        this.dataTable = new CustomDataTable(this.options.dataTableOptions).getTable();
    }


    seedTable() { // конфигурация ajax dataTables не используется из-за сложностей замены url
        Util.getData(this.model.sourceURL)
            .then(newData => {
                if (newData) {
                    this.dataTable.clear();
                    this.dataTable.rows.add(newData);
                    this.dataTable.draw();
                }
            });
    }


    // Overrides

    //_getContentValidationResult() {
    //    return true;
    //}

    _getModalBodyContent() {
        return this._createTable();
    }

    _onModalHidden() {
        super._onModalHidden();

        this.dataTable.clear();
    }

    _prepareToShow(model) {
        super._prepareToShow(model);

        this.seedTable();
    }


    // Private

    _createTable() {
        const tr = document.createElement('tr');

        for (let column of this.options.columns) {
            const th = document.createElement('th');
            th.innerText = column.title;

            tr.appendChild(th);
        }

        const thead = document.createElement('thead');
        thead.appendChild(tr);

        const table = document.createElement('table');
        table.id = this._getDataTableId();
        table.className = CLASSNAME_TABLE;
        table.appendChild(thead);

        return table;
    }

    _getDataTableId() {
        if (this._dataTableId) {
            return this._dataTableId;
        }
        this._dataTableId = this.getId() + '-table';
        return this._dataTableId;
    }
}

export default ModalDataTable;