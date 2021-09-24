import React from 'react';

import { EditableCard } from '../../../components/card';
import CheckupStatusMedicalReportGeneralData from './../../../components/checkupStatusMedicalReportGeneralData';

const MedicalReportData = ({ examination, onEditClick }) => {
    return (
        <EditableCard title='Медицинское заключение' onEditClick={onEditClick}>
            <CheckupStatusMedicalReportGeneralData checkup={examination} />
        </EditableCard>
    )
}

export default MedicalReportData;