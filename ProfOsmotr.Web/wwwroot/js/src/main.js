// Vendor
import 'jquery';
import 'bootstrap';
import 'jquery-mask-plugin';
import 'select2';
import 'datatables.net-bs4';
import 'datatables.net-buttons-bs4';
import 'datatables.net-fixedheader-bs4';
import 'datatables.net-rowgroup-bs4';
import 'datatables.net-scroller-bs4';
import 'datatables.net-select-bs4';

// Internal
import ScriptManager from './util/script-manager';
import initCalculationCompany from './calculation-company';
import initCalculationEdit from './calculation-edit';
import initCalculationSingle from './calculation-single';
import initCatalogList from './catalog-list';
import initClinicList from './clinic-list';
import initClinicSettings from './clinic-settings';
import OrderExaminationsPage from './order-examinations';
import OrderItemsPage from './order-items';
import initRegisterCreateRequest from './register-create-request';
import RegisterRequestsListPage from './register-requests';
import initUserList from './user-list';
import initUserLogin from './user-login';

// Styles
import 'bootstrap/dist/css/bootstrap.min.css';
import 'select2/dist/css/select2.min.css';
import '@ttskch/select2-bootstrap4-theme/dist/select2-bootstrap4.min.css';
import 'datatables.net-bs4/css/dataTables.bootstrap4.min.css';
import 'datatables.net-fixedheader-bs4/css/fixedHeader.bootstrap4.min.css';
import 'datatables.net-buttons-bs4/css/buttons.bootstrap4.min.css';
import 'datatables.net-rowgroup-bs4/css/rowGroup.bootstrap4.min.css';
import 'datatables.net-scroller-bs4/css/scroller.bootstrap4.min.css';
import 'datatables.net-select-bs4/css/select.bootstrap4.min.css';
import '../../css/src/site.css';


const namespace = {
    'calculation-company': initCalculationCompany,
    'calculation-edit': initCalculationEdit,
    'calculation-single': initCalculationSingle,
    'catalog-list': initCatalogList,
    'clinic-list': initClinicList,
    'clinic-settings': initClinicSettings,
    'order-examinations': OrderExaminationsPage.init,
    'order-items': OrderItemsPage.init,
    'register-create-request': initRegisterCreateRequest,
    'register-requests': () => new RegisterRequestsListPage(),
    'user-list': () => initUserList(),
    'user-login': () => initUserLogin()
};

const scriptManager = new ScriptManager(namespace);
document.addEventListener('DOMContentLoaded', event => scriptManager.load());