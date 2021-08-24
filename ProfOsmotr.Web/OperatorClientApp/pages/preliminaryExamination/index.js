import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import useErrorHandler from './../../hooks/useErrorHandler';
import CancellationToken from './../../utils/cancellationToken';
import CheckupIndexValuesData from './../../components/checkupIndexValuesData';
import ExaminationEditorData from './../../components/examinationEditorData';
import Spinner from '../../components/spinner';
import WorkPlaceEditorModal from '../../components/workPlaceEditorModal';
import CheckupIndexValuesEditorModal from '../../components/checkupIndexValuesEditorModal';
import PreliminaryExaminationActions from './components/preliminaryExaminationActions';
import WorkPlaceData from './components/workPlaceData';
import MedicalReportData from './components/medicalReportData';
import PatientData from './components/patientData';
import MedicalReportModal from './components/medicalReportModal';


const PreliminaryExaminationPage = (props) => {
    const examinationId = props.match.params.id;
    const { preliminaryExaminationsStore, preliminaryExaminationEditorStore } = useStore();
    const errorHandler = useErrorHandler();

    useEffect(() => {
        preliminaryExaminationsStore.setExaminationSlug(examinationId);

        const cancellationToken = new CancellationToken();
        preliminaryExaminationsStore.loadExamination(cancellationToken)
            .catch(errorHandler);

        return () => {
            cancellationToken.cancel();
            preliminaryExaminationsStore.resetExamination();
        }
    }, [examinationId]);

    const { isExaminationLoading, examination } = preliminaryExaminationsStore;

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
                examination={examination}
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