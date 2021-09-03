import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';

import { NavBar } from './components/nav';
import PatientListPage from './pages/patientList';
import PatientEditorPage from './pages/patientEditor';
import PatientPage from './pages/patient';
import EmployerEditorPage from './pages/employerEditor';
import EmployerListPage from './pages/employerList';
import EmployerPage from './pages/employer';
import PreliminaryExaminationsListPage from './pages/preliminaryExaminationsList';
import PreliminaryExaminationPage from './pages/preliminaryExamination';
import ErrorBoundary from './components/errorBoundary';
import routes from './routes';

import 'react-toastify/dist/ReactToastify.min.css';
import './style.css';

const App = () => (
    <Router>
        <NavBar />
        <ErrorBoundary>
            <Switch>
                <Route path={routes.patients.path} component={PatientListPage} exact />
                <Route path={routes.createPatient.path} component={PatientEditorPage} exact />
                <Route path={routes.patient.path} component={PatientPage} exact />
                <Route path={routes.editPatient.path} component={PatientEditorPage} exact />
                <Route path={routes.employers.path} component={EmployerListPage} exact />
                <Route path={routes.createEmployer.path} component={EmployerEditorPage} exact />
                <Route path={routes.employer.path} component={EmployerPage} exact />
                <Route path={routes.editEmployer.path} component={EmployerEditorPage} exact />
                <Route path={routes.preliminaryExaminations.path} component={PreliminaryExaminationsListPage} exact />
                <Route path={routes.preliminaryExamination.path} component={PreliminaryExaminationPage} />
            </Switch>
        </ErrorBoundary>
        <ToastContainer
            position="bottom-right"
            autoClose={10000}
            hideProgressBar={false}
            newestOnTop
            closeOnClick
            rtl={false}
            pauseOnFocusLoss
            draggable
            pauseOnHover
        />
    </Router>
);

export default App;