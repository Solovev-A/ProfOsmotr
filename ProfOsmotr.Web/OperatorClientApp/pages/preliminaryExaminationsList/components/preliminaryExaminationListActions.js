import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'

const PreliminaryExaminationListActions = (props) => {
    const actionClassName = "btn btn-secondary";

    return (
        <div className="mb-3">
            <Link to={routes.createPreliminaryExamination.path} className={actionClassName}>
                <FontAwesomeIcon icon={faPlus} />
                {' '}
                Создать
            </Link>
        </div>
    )
}

export default PreliminaryExaminationListActions;