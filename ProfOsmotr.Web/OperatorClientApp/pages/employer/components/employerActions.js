import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'

const EmployerActions = ({ employerId, onAddPeriodicExamination }) => {
    const actionClassName = "btn btn-secondary";

    return (
        <div className="mb-3">
            <Link to={routes.editEmployer.getUrl(employerId)} className={actionClassName}>
                <FontAwesomeIcon icon={faEdit} />
                {' '}
                Редактировать
            </Link>
            {' '}
            <button type="button"
                className={actionClassName}
                onClick={onAddPeriodicExamination}
            >
                <FontAwesomeIcon icon={faPlus} />
                {' '}
                Добавить периодический медосмотр
            </button>
        </div>
    )
}

export default EmployerActions;