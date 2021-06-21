import React from 'react';
import { Link } from 'react-router-dom';

import routes from '../../../routes';

const PatientListItemView = ({ id, lastName, firstName, patronymicName, dateOfBirth }) => {
    return (
        <tr>
            <td>
                <Link to={routes.patient.getUrl(id)}>
                    {`${lastName} ${firstName} ${patronymicName}`}
                </Link>
            </td>
            <td>
                {dateOfBirth}
            </td>
        </tr>
    )
}

const PatientList = ({ items }) => {
    if (!items) return null;

    if (items.length === 0) {
        return (
            <div className="text-center font-italic">Список пуст</div>
        )
    }

    return (
        <table className="table">
            <thead>
                <tr>
                    <th style={{ width: '70%' }}>
                        Ф.И.О.
                    </th>
                    <th>
                        Дата рождения
                    </th>
                </tr>
            </thead>
            <tbody>
                {
                    items.map(patient => <PatientListItemView key={patient.id} {...patient} />)
                }
            </tbody>
        </table>
    )
}

export default PatientList;