import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';
import DocumentsDropdown from './../../../components/documentsDropdown';

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

    const docs = [];
    if (examination.checkupStatuses.length) {
        docs.push({
            href: routes.periodicExaminationAllReports.getUrl(examinationSlug),
            title: 'Все заключения'
        })
        docs.push({
            href: routes.periodicExaminationAllExcerpts.getUrl(examinationSlug),
            title: 'Все выписки'
        })
    }
    if (examination.reportDate) {
        docs.push({
            href: routes.periodicExaminationReport.getUrl(examinationSlug),
            title: 'Заключительный акт'
        })
    }


    return (
        <div className="mb-3">
            <DocumentsDropdown docs={docs} />
            &nbsp;
            <button type="button"
                className={actionClassName}
                onClick={onRemoveExamination}
            >
                <FontAwesomeIcon icon={faTrash} />
                {' '}
                Удалить
            </button>
        </div >
    )
}

export default PeriodicExaminationActions;