import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileDownload, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Dropdown, DropdownButton } from 'react-bootstrap';

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
            <DropdownButton title={<><FontAwesomeIcon icon={faFileDownload} /> Документы</>}
                variant="secondary"
                style={{ display: 'inline-block' }}
            >
                <Dropdown.Item href={routes.preliminaryExaminationMedicalReport.getUrl(examinationSlug)} >
                    Заключение
                </Dropdown.Item>
                <Dropdown.Item href={routes.preliminaryExaminationExcerpt.getUrl(examinationSlug)} >
                    Выписка
                </Dropdown.Item>
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
        </div>
    )
}

export default PreliminaryExaminationActions;