import React from 'react';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { EditableCard } from '../../../components/card';
import DataRow from '../../../components/dataRow';
import CheckupStatusMedicalReportGeneralData from './../../../components/checkupStatusMedicalReportGeneralData';
import useICD10Store from './../../../hooks/useICD10Store';
import { observer } from 'mobx-react-lite';

const MedicalReportData = ({ checkupStatus, onEditClick }) => {
    const {
        checkupStarted, isDisabled, needExaminationAtOccupationalPathologyCenter,
        needOutpatientExamunationAndTreatment, needInpatientExamunationAndTreatment,
        needSpaTreatment, needDispensaryObservation,
        newlyDiagnosedChronicSomaticDiseases, newlyDiagnosedOccupationalDiseases
    } = checkupStatus;

    return (
        <EditableCard title='Медицинское заключение' onEditClick={onEditClick}>
            <CheckupStatusMedicalReportGeneralData checkup={checkupStatus} />
            <DataRow title="Медосмотр начат" value={renderBooleanValue(checkupStarted)} />
            <DataRow
                title="Имеет стойкую степень утраты трудоспособности"
                value={renderBooleanValue(isDisabled)} />
            <DataRow
                title="Нуждается в дообследовании в центре профпаталогии"
                value={renderBooleanValue(needExaminationAtOccupationalPathologyCenter)} />
            <DataRow
                title="Нуждается в амбулаторном обследовании и лечении"
                value={renderBooleanValue(needOutpatientExamunationAndTreatment)} />
            <DataRow
                title="Нуждается в стационарном обследовании и лечении"
                value={renderBooleanValue(needInpatientExamunationAndTreatment)} />
            <DataRow
                title="Нуждается в санаторно-курортном лечении"
                value={renderBooleanValue(needSpaTreatment)} />
            <DataRow
                title="Нуждается в диспансерном наблюдении"
                value={renderBooleanValue(needDispensaryObservation)} />
            <DataRow
                title="Впервые выявленные хронические соматические заболевания"
                value={renderDiseasesList(newlyDiagnosedChronicSomaticDiseases)} />
            <DataRow
                title="Впервые выявленные профессиональные заболевания"
                value={renderDiseasesList(newlyDiagnosedOccupationalDiseases)} />
        </EditableCard>
    )
}

const renderBooleanValue = (value) => {
    return value
        ? <FontAwesomeIcon icon={faCheck} />
        : null;
}

const renderDiseasesList = (items) => {
    return items.length
        ? <DiseasesList items={items} />
        : null;
}

const DiseasesList = observer(({ items }) => {
    const icd10Store = useICD10Store();
    const { chapters } = icd10Store;

    if (!icd10Store.isLoaded) return null;

    const diseasesData = items.map(item => {
        const chapter = chapters.find(chapter => chapter.id === item.chapterId);

        return `${chapter.block}: ${item.cases}`
    })

    return diseasesData.join('; ')
})

export default MedicalReportData;