import React from 'react';

import { EditableCard } from '../../../components/card';
import DataRow from './../../../components/dataRow';

const ReportData = ({ examination, onEditClick }) => {
    const { reportDate, recommendations } = examination;
    const hasNoData = !reportDate && !recommendations;

    return (
        <EditableCard title='Заключительный акт' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    :
                    <>
                        <DataRow title="Дата акта" value={reportDate} />
                        <DataRow title="Рекомендации" value={recommendations} />
                    </>
            }
        </EditableCard>
    )
}

export default ReportData;