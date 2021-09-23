import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBook } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'

const Actions = (props) => {
    const actionClassName = "btn btn-secondary";

    return (
        <div className="mb-3">
            <Link to={routes.periodicExaminationsJournal.path} className={actionClassName}>
                <FontAwesomeIcon icon={faBook} />
                {' '}
                Журнал
            </Link>
        </div>
    )
}

export default Actions;