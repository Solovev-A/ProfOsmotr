import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import YearPicker from '../../../components/yearPicker';

const EmployerActions = ({ employerId, onAddPeriodicExamination }) => {
    const actionClassName = "btn btn-secondary";
    const currentYear = new Date().getFullYear();

    return (
        <div className="mb-3">
            <Link to={routes.editEmployer.getUrl(employerId)} className={actionClassName}>
                <FontAwesomeIcon icon={faEdit} />
                {' '}
                Редактировать
            </Link>
            {' '}
            <YearPicker
                title={<><FontAwesomeIcon icon={faPlus} /> Добавить периодический медосмотр</>}
                start={currentYear - 1}
                end={currentYear + 1}
                onYearPick={onAddPeriodicExamination}
                className="d-inline-block"
            />
        </div>
    )
}

export default EmployerActions;