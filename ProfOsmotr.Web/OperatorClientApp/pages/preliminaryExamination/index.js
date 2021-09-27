import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import CheckupIndexValuesData from './../../components/checkupIndexValuesData';
import ExaminationEditorData from './../../components/examinationEditorData';
import Spinner from '../../components/spinner';
import WorkPlaceEditorModal from '../../components/workPlaceEditorModal';
import CheckupIndexValuesEditorModal from '../../components/checkupIndexValuesEditorModal';
import PatientData from '../../components/patientData';
import PreliminaryExaminationActions from './components/preliminaryExaminationActions';
import WorkPlaceData from '../../components/workPlaceData';
import MedicalReportData from './components/medicalReportData';
import MedicalReportModal from './components/medicalReportModal';
import usePageId from './../../hooks/usePageId';
import useTitle from './../../hooks/useTitle';
import { getShortName } from '../../utils/personNames';


const PreliminaryExaminationPage = (props) => {
    const examinationId = props.match.params.id;

    const { preliminaryExaminationsStore, preliminaryExaminationEditorStore } = useStore();

    usePageId({
        slugSetter: preliminaryExaminationsStore.setExaminationSlug,
        loader: preliminaryExaminationsStore.loadExamination,
        onReset: preliminaryExaminationsStore.resetExamination
    });

    const { isExaminationLoading, examination } = preliminaryExaminationsStore;

    const title = isExaminationLoading
        ? 'Предварительный осмотр - Загрузка'
        : `${getShortName(examination.patient)} - Предварительный медосмотр`
    useTitle(title);

    if (isExaminationLoading) return <Spinner />

    const onWorkPlaceEditClick = () => {
        preliminaryExaminationsStore
            .workPlaceModal
            .open();
    }

    const onCheckupIndexValuesEditClick = () => {
        preliminaryExaminationsStore
            .checkupIndexValuesModal
            .open();
    }

    const onMedicalReportEditClick = () => {
        preliminaryExaminationsStore
            .medicalReportModal
            .open();
    }


    return (
        <>
            <h2>Карта предварительного осмотра ID {examinationId}</h2>
            <PatientData patient={examination.patient} />
            <ExaminationEditorData editor={examination.lastEditor} />
            <PreliminaryExaminationActions />
            <WorkPlaceData
                workPlace={examination.workPlace}
                onEditClick={onWorkPlaceEditClick}
            />
            <CheckupIndexValuesData
                checkupExaminationResultIndexes={examination.checkupExaminationResultIndexes}
                onEditClick={onCheckupIndexValuesEditClick}
            />
            <MedicalReportData
                examination={examination}
                onEditClick={onMedicalReportEditClick}
            />
            <WorkPlaceEditorModal
                editorStore={preliminaryExaminationEditorStore.workPlaceEditorStore}
                modalStore={preliminaryExaminationsStore.workPlaceModal}
            />
            <CheckupIndexValuesEditorModal
                editorStore={preliminaryExaminationEditorStore.checkupIndexValuesEditorStore}
                modalStore={preliminaryExaminationsStore.checkupIndexValuesModal}
            />
            <MedicalReportModal />
        </>
    )
}

export default observer(PreliminaryExaminationPage);