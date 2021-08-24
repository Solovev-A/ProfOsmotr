import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLink } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes';

const PatientData = ({ patient }) => {
    return (
        <>
            <p className='h4'>
                {patient.lastName} {patient.firstName} {patient.patronymicName} {' '}
                <Link to={routes.patient.getUrl(patient.id)}>
                    <FontAwesomeIcon icon={faLink} />
                </Link>
            </p>
            <p>Дата рождения: {patient.dateOfBirth}</p>
        </>
    )
}

export default PatientData;