import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import { NavBar } from './components/nav';
import PreliminaryExaminations from './pages/preliminaryExaminationsMain';
import routes from './routes';

const App = () => (
    <Router>
        <NavBar />
        <Switch>
            <Route path={routes.preliminaryExaminations.path} component={PreliminaryExaminations} />
        </Switch>
    </Router>
);

export default App;