import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileAlt, faFileMedical } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';

const PeriodicExaminationActions = () => {
    const actionClassName = "btn btn-secondary";
    const { periodicExaminationsStore } = useStore();
    const { removeExamination, examinationSlug, examination } = periodicExaminationsStore;
    const history = useHistory();

    const onRemoveExamination = async () => {
        const response = await removeExamination();
        if (response && response.success !== false) {
            history.replace(routes.periodicExaminations.path);
        }
    }

    return (
        <div className="mb-3">
            {
                examination.checkupStatuses.length
                    ?
                    <>
                        <a href={routes.periodicExaminationAllReports.getUrl(examinationSlug)}
                            className={actionClassName}
                        >
                            <FontAwesomeIcon icon={faFileAlt} />
                            {' '}
                            Все заключения
                        </a>
                        &nbsp;
                        <a href={routes.periodicExaminationAllExcerpts.getUrl(examinationSlug)}
                            className={actionClassName}
                        >
                            <FontAwesomeIcon icon={faFileMedical} />
                            {' '}
                            Все выписки
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
                    </>
                    : null
            }
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