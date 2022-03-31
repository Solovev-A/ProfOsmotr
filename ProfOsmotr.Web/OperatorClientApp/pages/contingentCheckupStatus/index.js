import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import WorkPlaceData from './../../components/workPlaceData';
import CheckupIndexValuesData from './../../components/checkupIndexValuesData';
import ExaminationEditorData from './../../components/examinationEditorData';
import Spinner from '../../components/spinner';
import WorkPlaceEditorModal from '../../components/workPlaceEditorModal';
import CheckupIndexValuesEditorModal from '../../components/checkupIndexValuesEditorModal';
import usePageId from './../../hooks/usePageId';
import PatientData from './../../components/patientData';
import CheckupStatusActions from './components/checkupStatusActions';
import LinkIcon from '../../components/linkIcon';
import routes from '../../routes';
import MedicalReportData from './components/medicalReportData';
import MedicalReportModal from './components/medicalReportModal';
import useTitle from '../../hooks/useTitle';
import { getShortName } from '../../utils/personNames';


const ContingentCheckupStatusPage = (props) => {
    const { contingentCheckupStatusStore, contingentCheckupStatusEditorStore } = useStore();

    usePageId({
        slugSetter: contingentCheckupStatusStore.setCheckupStatusSlug,
        loader: contingentCheckupStatusStore.loadCheckupStatus,
        onReset: contingentCheckupStatusStore.reset
    });

    const { isLoading, checkupStatus } = contingentCheckupStatusStore;

    const title = isLoading
        ? 'Периодический осмотр работника - Загрузка'
        : `${getShortName(checkupStatus.patient)} - Периодический осмотр работника`
    useTitle(title);

    if (isLoading) return <Spinner />

    const { examination } = checkupStatus;
    const examinationUrl = routes.periodicExamination.getUrl(examination.id);


    const onWorkPlaceEditClick = () => {
        contingentCheckupStatusStore
            .workPlaceModal
            .open();
    }

    const onCheckupIndexValuesEditClick = () => {
        contingentCheckupStatusStore
            .checkupIndexValuesModal
            .open();
    }

    const onMedicalReportEditClick = () => {
        contingentCheckupStatusStore
            .medicalReportModal
            .open();
    }


    return (
        <>
            <h2>Карта периодического медосмотра <LinkIcon url={examinationUrl} /> за {examination.examinationYear} год</h2>
            <PatientData patient={checkupStatus.patient} />
            <ExaminationEditorData editor={checkupStatus.lastEditor} />
            <CheckupStatusActions />
            <WorkPlaceData
                workPlace={checkupStatus.workPlace}
                onEditClick={onWorkPlaceEditClick}
            />
            <CheckupIndexValuesData
                checkupExaminationResultIndexes={checkupStatus.checkupExaminationResultIndexes}
                onEditClick={onCheckupIndexValuesEditClick}
            />
            <MedicalReportData
                checkupStatus={checkupStatus}
                onEditClick={onMedicalReportEditClick}
            />
            <WorkPlaceEditorModal
                editorStore={contingentCheckupStatusEditorStore.workPlaceEditorStore}
                modalStore={contingentCheckupStatusStore.workPlaceModal}
                canChangeEmployer={false}
            />
            <CheckupIndexValuesEditorModal
                editorStore={contingentCheckupStatusEditorStore.checkupIndexValuesEditorStore}
                modalStore={contingentCheckupStatusStore.checkupIndexValuesModal}
            />
            <MedicalReportModal />
        </>
    )
}

export default observer(ContingentCheckupStatusPage);