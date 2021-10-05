import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileMedical, faFileAlt } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';
import { Button } from '../../../components/buttons';

const CheckupStatusActions = () => {
    const { contingentCheckupStatusStore } = useStore();
    const { removeCheckupStatus, checkupStatusSlug } = contingentCheckupStatusStore;
    const history = useHistory();

    const examinationId = contingentCheckupStatusStore.checkupStatus.examination.id;
    const examinationUrl = routes.periodicExamination.getUrl(examinationId);

    const onRemoveCheckupStatus = async () => {
        const response = await removeCheckupStatus();
        if (response && response.success !== false) {
            history.replace(examinationUrl);
        }
    }

    return (
        <div className="mb-3">
            <a href={routes.contingentCheckupStatusExcerpt.getUrl(checkupStatusSlug)}
                className="btn btn-secondary"
                download
            >
                <FontAwesomeIcon icon={faFileMedical} />
                {' '}
                Выписка
            </a>
            &nbsp;
            <a href={routes.contingentCheckupStatusMedicalReport.getUrl(checkupStatusSlug)}
                className="btn btn-secondary"
                download
            >
                <FontAwesomeIcon icon={faFileAlt} />
                {' '}
                Заключение
            </a>
            &nbsp;
            <Button onClick={onRemoveCheckupStatus}>
                <FontAwesomeIcon icon={faTrash} />
                {' '}
                Удалить
            </Button>
        </div>
    )
}

export default CheckupStatusActions;