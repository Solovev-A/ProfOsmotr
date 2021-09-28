import React from 'react';

import DataRow from './dataRow';

const CheckupStatusMedicalReportGeneralData = ({ checkup }) => {
    const { result, medicalReport, dateOfCompletion, registrationJournalEntryNumber } = checkup;

    return (
        <>
            <DataRow title="Результат" value={result?.text} required />
            <DataRow title="Дата заключения" value={dateOfCompletion} required />
            <DataRow title="Медицинское заключение" value={medicalReport} />
            <DataRow title="Номер" value={registrationJournalEntryNumber} />
        </>
    );
}

export default CheckupStatusMedicalReportGeneralData;