import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserEdit, faPlus } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'

const PatientActions = ({ patientId, onAddPreliminaryExamination }) => {
    const actionClassName = "btn btn-secondary";

    return (
        <div className="mb-3">
            <Link to={routes.editPatient.getUrl(patientId)} className={actionClassName}>
                <FontAwesomeIcon icon={faUserEdit} />
                {' '}
                Редактировать
            </Link>
            {' '}
            <button type="button"
                className={actionClassName}
                onClick={onAddPreliminaryExamination}
            >
                <FontAwesomeIcon icon={faPlus} />
                {' '}
                Добавить предварительный медосмотр
            </button>
        </div>
    )
}

export default PatientActions;