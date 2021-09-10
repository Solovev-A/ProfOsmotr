import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLink } from '@fortawesome/free-solid-svg-icons';

import routes from '../../../routes';

const EmployerInfo = ({ employer }) => {

    return (
        <>
            <p className='h4'>
                {employer.name}
                {' '}
                <Link to={routes.employer.getUrl(employer.id)}>
                    <FontAwesomeIcon icon={faLink} />
                </Link>
            </p>
            <p>
                Руководитель: {employer.headLastName} {employer.headFirstName} {employer.headPatronymicName}
                {employer.headPosition ? `, ${employer.headPosition}` : null}
            </p>
        </>
    )
}

export default EmployerInfo;