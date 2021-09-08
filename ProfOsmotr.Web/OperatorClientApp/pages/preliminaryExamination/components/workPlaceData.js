import React from 'react';

import { EditableCard } from '../../../components/card';

const WorkPlaceData = ({ examination, onEditClick }) => {
    const { workPlace } = examination;
    const hasNoData = !workPlace.employer && !workPlace.profession;

    return (
        <EditableCard title='Место работы' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    :
                    <>
                        <b>Название организации:</b> {workPlace.employer?.name}<br />
                        <b>Структурное подразделение:</b> {workPlace.employer?.department?.name}<br />
                        <b>Должность (профессия):</b> {workPlace.profession?.name}<br />
                        <b>Вредные факторы и (или) виды работ:</b> {workPlace.profession?.orderItems.join('; ')}
                    </>
            }
        </EditableCard>
    )
}

export default WorkPlaceData;