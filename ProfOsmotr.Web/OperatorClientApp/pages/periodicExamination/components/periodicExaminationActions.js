import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileAlt } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';

const PeriodicExaminationActions = () => {
    const actionClassName = "btn btn-secondary";
    const { periodicExaminationsStore } = useStore();
    const history = useHistory();

    const onRemoveExamination = async () => {
        const response = await periodicExaminationsStore.removeExamination();
        if (response && response.success !== false) {
            history.replace(routes.periodicExaminations.path);
        }
    }

    return (
        <div className="mb-3">
            <a href={routes.periodicExaminationAllReports.getUrl(periodicExaminationsStore.examinationSlug)}
                className={actionClassName}
            >
                <FontAwesomeIcon icon={faFileAlt} />
                {' '}
                Все заключения
            </a>
            &nbsp;
            <button type="button"
                className={actionClassName}
            >
                <FontAwesomeIcon icon={faFileAlt} />
                {' '}
                Заключительный акт
            </button>
            &nbsp;
            <button type="button"
                className={actionClassName}
                onClick={onRemoveExamination}
            >
                <FontAwesomeIcon icon={faTrash} />
                {' '}
                Удалить
            </button>
        </div>
    )
}

export default PeriodicExaminationActions;