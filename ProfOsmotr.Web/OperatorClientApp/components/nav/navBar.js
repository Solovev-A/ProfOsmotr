import React from 'react';

import routes from '../../routes';
import { NavPills, NavItem } from './navPills'


const NavBar = () => (
    <NavPills>
        <NavItem link={routes.preliminaryExaminations.path} title={routes.preliminaryExaminations.name} />
        <NavItem link={routes.periodicExaminations.path} title={routes.periodicExaminations.name} />
        <NavItem link={routes.patients.path} title={routes.patients.name} />
        <NavItem link={routes.employers.path} title={routes.employers.name} />
        <NavItem link={routes.statistics.path} title={routes.statistics.name} />
    </NavPills>
)

export default NavBar;