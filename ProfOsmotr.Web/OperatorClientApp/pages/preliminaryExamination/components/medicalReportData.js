import React from 'react';

import { EditableCard } from '../../../components/card';
import CheckupStatusMedicalReportGeneralData from './../../../components/checkupStatusMedicalReportGeneralData';

const MedicalReportData = ({ examination, onEditClick }) => {
    const { result, medicalReport, dateOfComplition, registrationJournalEntryNumber } = examination;
    const hasNoData = !result && !medicalReport && !dateOfComplition && !registrationJournalEntryNumber;

    return (
        <EditableCard title='Медицинское заключение' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    : <CheckupStatusMedicalReportGeneralData checkup={examination} />
            }
        </EditableCard>
    )
}

export default MedicalReportData;