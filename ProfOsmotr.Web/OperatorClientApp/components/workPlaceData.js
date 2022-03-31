import React from 'react';

import { EditableCard } from './card';
import LinkIcon from './linkIcon';
import routes from './../routes';
import DataRow from './dataRow';

const WorkPlaceData = ({ workPlace, onEditClick }) => {
    const hasNoData = !workPlace.employer && !workPlace.profession;
    const employerUrl = routes.employer.getUrl(workPlace.employer?.id);
    const employerLink = workPlace.employer && <LinkIcon url={employerUrl} />
    const employerValue = workPlace.employer && <>{workPlace.employer?.name} {employerLink}</>;

    return (
        <EditableCard title='Место работы' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    :
                    <>
                        <DataRow title="Название организации" value={employerValue} />
                        <DataRow title="Структурное подразделение" value={workPlace.employer?.department?.name} />
                        <DataRow title="Должность (профессия)" value={workPlace.profession?.name} />
                        <DataRow title="Вредные факторы и (или) виды работ" value={workPlace.profession?.orderItems.join('; ')} />
                    </>
            }
        </EditableCard>
    )
}

export default WorkPlaceData;