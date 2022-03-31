import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserPlus } from '@fortawesome/free-solid-svg-icons';

import routes from '../../../routes';

const PatientListActions = (props) => {
    return (
        <div className="mb-3">
            <Link to={routes.createPatient.path} className="btn btn-secondary">
                <FontAwesomeIcon icon={faUserPlus} />
                {' '}
                Добавить пациента
            </Link>
        </div>
    )
}

export default PatientListActions;