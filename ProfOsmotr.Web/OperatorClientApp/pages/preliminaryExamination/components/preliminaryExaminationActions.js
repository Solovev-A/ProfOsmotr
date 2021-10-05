import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileMedical, faFileAlt } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';

const PreliminaryExaminationActions = () => {
    const actionClassName = "btn btn-secondary";
    const { preliminaryExaminationsStore } = useStore();
    const { removeExamination, examinationSlug } = preliminaryExaminationsStore;
    const history = useHistory();

    const onRemoveExamination = async () => {
        const response = await removeExamination();
        if (response && response.success !== false) {
            history.replace(routes.preliminaryExaminations.path);
        }
    }

    return (
        <div className="mb-3">
            <a href={routes.preliminaryExaminationExcerpt.getUrl(examinationSlug)}
                className="btn btn-secondary"
                download
            >
                <FontAwesomeIcon icon={faFileMedical} />
                {' '}
                Выписка
            </a>
            &nbsp;
            <a href={routes.preliminaryExaminationMedicalReport.getUrl(examinationSlug)}
                className={actionClassName}
                download
            >
                <FontAwesomeIcon icon={faFileAlt} />
                {' '}
                Заключение
            </a>
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