import React from 'react';

import { EditableCard } from '../../../components/card';
import EmployerDataView from './employerDataView';

const EmployerData = ({ examination, onEditClick }) => {
    const { employerData } = examination;
    const hasNoData = !employerData;

    return (
        <EditableCard title='Сведения о работодателе' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    : <EmployerDataView employerData={employerData} />
            }
        </EditableCard>
    )
}

export default EmployerData;