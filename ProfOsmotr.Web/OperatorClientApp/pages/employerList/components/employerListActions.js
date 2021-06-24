import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

import routes from '../../../routes';

const EmployerListActions = (props) => {
    return (
        <div className="mb-3">
            <Link to={routes.createEmployer.path} className="btn btn-secondary">
                <FontAwesomeIcon icon={faPlus} />
                {' '}
                Добавить организацию
            </Link>
        </div>
    )
}

export default EmployerListActions;