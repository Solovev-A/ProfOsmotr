import React from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileMedical, faFileAlt } from '@fortawesome/free-solid-svg-icons';

import routes from './../../../routes'
import useStore from './../../../hooks/useStore';
import { Button } from '../../../components/buttons';

const CheckupStatusActions = () => {
    const { contingentCheckupStatusStore } = useStore();
    const history = useHistory();

    const examinationId = contingentCheckupStatusStore.checkupStatus.examination.id;
    const examinationUrl = routes.periodicExamination.getUrl(examinationId);

    const onRemoveCheckupStatus = async () => {
        const response = await contingentCheckupStatusStore.removeCheckupStatus();
        if (response && response.success !== false) {
            history.replace(examinationUrl);
        }
    }

    return (
        <div className="mb-3">
            <Button>
                <FontAwesomeIcon icon={faFileMedical} />
                {' '}
                Выписка
            </Button>
            &nbsp;
            <a href={routes.contingentCheckupStatusMedicalReport.getUrl(contingentCheckupStatusStore.checkupStatusSlug)}
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