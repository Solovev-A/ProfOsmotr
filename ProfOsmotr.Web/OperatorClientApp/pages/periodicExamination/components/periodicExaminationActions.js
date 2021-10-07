import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileDownload } from '@fortawesome/free-solid-svg-icons';
import { Dropdown, DropdownButton } from 'react-bootstrap';

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
            <DropdownButton title={<><FontAwesomeIcon icon={faFileDownload} /> Документы</>}
                variant="secondary"
                style={{ display: 'inline-block' }}
            >
                {
                    examination.checkupStatuses.length
                        ?
                        <>
                            <Dropdown.Item href={routes.periodicExaminationAllReports.getUrl(examinationSlug)} >
                                Все заключения
                            </Dropdown.Item>
                            <Dropdown.Item href={routes.periodicExaminationAllExcerpts.getUrl(examinationSlug)} >
                                Все выписки
                            </Dropdown.Item>
                        </>
                        : null
                }
                {
                    examination.reportDate
                        ?
                        <Dropdown.Item href={routes.periodicExaminationReport.getUrl(examinationSlug)} >
                            Заключительный акт
                        </Dropdown.Item>
                        : null
                }
            </DropdownButton>
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