import React from 'react';
import InputCheckbox from './../../../components/forms/observers/inputCheckbox';
import NewlyDiagnosedDiseasesEditor from './newlyDiagnosedDiseasesEditor';

const MedicalReportEditorForm = ({ formStore }) => {
    return (
        <>
            <InputCheckbox
                label="Медосмотр начат"
                name="checkupStarted"
                formStore={formStore}
                inline
            />
            <InputCheckbox
                label="Имеет стойкую степень утраты трудоспособности"
                name="isDisabled"
                formStore={formStore}
                inline
            />
            <InputCheckbox
                label="Нуждается в дообследовании в центре профпаталогии"
                name="needExaminationAtOccupationalPathologyCenter"
                formStore={formStore}
                inline
            />
            <InputCheckbox
                label="Нуждается в амбулаторном обследовании и лечении"
                name="needOutpatientExamunationAndTreatment"
                formStore={formStore}
                inline
            />
            <InputCheckbox
                label="Нуждается в стационарном обследовании и лечении"
                name="needInpatientExamunationAndTreatment"
                formStore={formStore}
                inline
            />
            <InputCheckbox
                label="Нуждается в санаторно-курортном лечении"
                name="needSpaTreatment"
                formStore={formStore}
                inline
            />
            <InputCheckbox
                label="Нуждается в диспансерном наблюдении"
                name="needDispensaryObservation"
                formStore={formStore}
                inline
            />
            <hr />
            <NewlyDiagnosedDiseasesEditor
                title="Впервые выявленные хронические соматические заболевания (случаев)"
                store={formStore.chronicSomaticDiseasesEditorStore}
            />
            <hr />
            <NewlyDiagnosedDiseasesEditor
                title="Впервые выявленные профессиональные заболевания"
                store={formStore.occupationalDiseasesEditorStore}
            />
        </>
    );
}

export default MedicalReportEditorForm;