import React from 'react';

import routes from '../routes';
import LinkIcon from './linkIcon';
import { getFullName } from '../utils/personNames';

const PatientData = ({ patient }) => {
    return (
        <>
            <p className='h4'>
                {getFullName(patient)}{' '}
                <LinkIcon url={routes.patient.getUrl(patient.id)} />
            </p>
            <p>Дата рождения: {patient.dateOfBirth}</p>
        </>
    )
}

export default PatientData;