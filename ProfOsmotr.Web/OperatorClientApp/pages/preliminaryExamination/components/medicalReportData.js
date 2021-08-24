import React from 'react';

import { EditableCard } from '../../../components/card';

const MedicalReportData = ({ examination, onEditClick }) => {
    const { result, medicalReport, dateOfComplition, registrationJournalEntryNumber } = examination;
    const hasNoData = !result && !medicalReport && !dateOfComplition && !registrationJournalEntryNumber;

    return (
        <EditableCard title='Медицинское заключение' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    :
                    <>
                        <b>Результат:</b> {result?.text}<br />
                        <b>Медицинское заключение:</b> {medicalReport}<br />
                        <b>Дата заключения:</b> {dateOfComplition}<br />
                        <b>Номер:</b> {registrationJournalEntryNumber}<br />
                    </>
            }
        </EditableCard>
    )
}

export default MedicalReportData;