import React from 'react';

import { EditableCard } from './card';

const IndexValueView = ({ indexValue }) => {
    return (
        <span>
            {indexValue.index.title}: <b>{indexValue.value}</b> {indexValue.index.unitOfMeasure} <br />
        </span>
    )
}

const IndexValuesList = ({ resultIndex }) => {
    return (
        <p>
            <b>{resultIndex.examinationName}</b><br />
            {
                resultIndex.checkupIndexValues.map(indexValue => <IndexValueView key={indexValue.index.id} indexValue={indexValue} />)
            }
        </p>
    )
}

const CheckupIndexValuesData = ({ checkupExaminationResultIndexes, onEditClick }) => {
    const hasNoData = !checkupExaminationResultIndexes.length;

    return (
        <EditableCard title='Результаты обследований' onEditClick={onEditClick}>
            {
                hasNoData
                    ? null
                    : checkupExaminationResultIndexes.map((resultIndex) => <IndexValuesList key={resultIndex.examinationName} resultIndex={resultIndex} />)
            }
        </EditableCard>
    )
}

export default CheckupIndexValuesData;