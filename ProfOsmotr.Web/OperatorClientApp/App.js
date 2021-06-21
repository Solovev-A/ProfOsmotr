import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';

import { NavBar } from './components/nav';
import PreliminaryExaminations from './pages/preliminaryExaminationsMain';
import PatientListPage from './pages/patientList';
import PatientEditorPage from './pages/patientEditor';
import PatientPage from './pages/patient';
import ErrorBoundary from './components/errorBoundary';
import routes from './routes';

import 'react-toastify/dist/ReactToastify.min.css';

const App = () => (
    <Router>
        <NavBar />
        <ErrorBoundary>
            <Switch>
                <Route path={routes.preliminaryExaminations.path} component={PreliminaryExaminations} />
                <Route path={routes.patients.path} component={PatientListPage} exact />
                <Route path={routes.createPatient.path} component={PatientEditorPage} exact />
                <Route path={routes.patient.path} component={PatientPage} exact />
                <Route path={routes.editPatient.path} component={PatientEditorPage} exact />
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