import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';
import DocumentsDropdown from './../../../components/documentsDropdown';

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
            <DocumentsDropdown
                docs={[{
                    href: routes.preliminaryExaminationMedicalReport.getUrl(examinationSlug),
                    title: 'Заключение'
                }, {
                    href: routes.preliminaryExaminationExcerpt.getUrl(examinationSlug),
                    title: 'Выписка'
                }]}
            />
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