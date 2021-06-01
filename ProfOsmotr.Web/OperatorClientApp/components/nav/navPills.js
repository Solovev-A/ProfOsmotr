import React from 'react';
import { NavLink } from 'react-router-dom';


export const NavItem = ({ title, link }) => (
    <li className="nav-item">
        <NavLink to={link} className="nav-link">{title}</NavLink>
    </li>
);

export const NavPills = ({ children }) => {
    return (
        <ul className="nav nav-pills nav-fill mb-4">
            {
                children
            }
        </ul >
    )
};