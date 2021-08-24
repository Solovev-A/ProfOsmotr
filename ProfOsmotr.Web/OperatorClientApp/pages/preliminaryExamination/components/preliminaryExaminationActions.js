import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileMedical, faFileAlt } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';

const PreliminaryExaminationActions = () => {
    const actionClassName = "btn btn-secondary";
    const { preliminaryExaminationsStore } = useStore();
    const history = useHistory();

    const onRemoveExamination = async () => {
        const response = await preliminaryExaminationsStore.removeExamination();
        if (response && response.success !== false) {
            history.replace(routes.preliminaryExaminations.path);
        }
    }

    return (
        <div className="mb-3">
            <button type="button"
                className={actionClassName}
            >
                <FontAwesomeIcon icon={faFileMedical} />
                {' '}
                Выписка
            </button>
            &nbsp;
            <button type="button"
                className={actionClassName}
            >
                <FontAwesomeIcon icon={faFileAlt} />
                {' '}
                Заключение
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

export default PreliminaryExaminationActions;