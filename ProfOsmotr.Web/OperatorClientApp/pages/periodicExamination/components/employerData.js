import React from 'react';

import { EditableCard } from '../../../components/card';
import DataRow from './dataRow';

const EmployerData = ({ examination, onEditClick }) => {
    const { employerData } = examination;
    const hasNoData = !employerData;

    return (
        <EditableCard title='Сведения о работодателе' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    :
                    <>
                        <DataGroup title="Общая численность работников"
                            total={employerData.employeesTotal}
                            women={employerData.employeesWomen}
                            under18={employerData.employeesUnder18}
                            persistentlyDisabled={employerData.employeesPersistentlyDisabled}
                        />
                        <DataGroup title="Численность работников, занятых на работах с вредными и (или) опасными условиями труда"
                            total={employerData.workingWithHarmfulFactorsTotal}
                            women={employerData.workingWithHarmfulFactorsWomen}
                            under18={employerData.workingWithHarmfulFactorsUnder18}
                            persistentlyDisabled={employerData.workingWithHarmfulFactorsPersistentlyDisabled}
                        />
                        <DataGroup title="Численность работников, занятых на работах, при выполнении которых обязательно проведение периодических медицинских осмотров"
                            total={employerData.workingWithJobTypesTotal}
                            women={employerData.workingWithJobTypesWomen}
                            under18={employerData.workingWithJobTypesUnder18}
                            persistentlyDisabled={employerData.workingWithJobTypesPersistentlyDisabled}
                        />
                    </>
            }
        </EditableCard>
    )
}

const DataGroup = ({ title, total, women, under18, persistentlyDisabled }) => {
    if (!total && !women && !under18 && !persistentlyDisabled) return null;

    return (
        <p>
            <DataRow title={title} value={total} required={true} />
            в том числе<br />
            <DataRow title="женщин" value={women} />
            <DataRow title="работников в возрасте до 18 лет" value={under18} />
            <DataRow title="работников, которым установлена стойкая степень утраты трудоспособности"
                value={persistentlyDisabled}
                br={false}
            />
        </p>
    )
}

export default EmployerData;