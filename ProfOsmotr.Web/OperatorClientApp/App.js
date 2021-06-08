import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';

import { NavBar } from './components/nav';
import PreliminaryExaminations from './pages/preliminaryExaminationsMain';
import routes from './routes';

import 'react-toastify/dist/ReactToastify.min.css';

const App = () => (
    <Router>
        <NavBar />
        <Switch>
            <Route path={routes.preliminaryExaminations.path} component={PreliminaryExaminations} />
        </Switch>
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