import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLink } from '@fortawesome/free-solid-svg-icons';

import routes from '../routes';
import LinkIcon from './linkIcon';

const PatientData = ({ patient }) => {
    return (
        <>
            <p className='h4'>
                {patient.lastName} {patient.firstName} {patient.patronymicName} {' '}
                <LinkIcon url={routes.patient.getUrl(patient.id)} />
            </p>
            <p>Дата рождения: {patient.dateOfBirth}</p>
        </>
    )
}

export default PatientData;