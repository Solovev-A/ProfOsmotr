import React from 'react';
import { observer } from 'mobx-react-lite';

const CheckupExaminationResultIndexesForm = observer(({ examinationResultIndex, editorStore, onValueChange }) => {
    const onChange = (event) => {
        const input = event.target;
        const id = Number(input.dataset.checkupIndexId);
        onValueChange(id, input.value);
    }

    const { examinationName, checkupIndexValues } = examinationResultIndex;

    const hasLongName = examinationName.length > 80;

    return (
        <div className="mb-2">
            <span
                className="font-weight-bold"
                title={hasLongName ? examinationName : null}
            >
                {hasLongName ? `${examinationName.slice(0, 80)}...` : examinationName}
            </span>
            {
                checkupIndexValues.map((checkupIndexValue) => {
                    const { index: { id, title, unitOfMeasure } } = checkupIndexValue;

                    const htmlId = `checkup-index-${id}`;
                    const unitOfMeasureString = unitOfMeasure?.length
                        ? `, ${unitOfMeasure}`
                        : '';

                    return (
                        <div className="row form-group" key={id}>
                            <div className="col" style={{ alignItems: 'center' }} >
                                <label htmlFor={htmlId}>
                                    {title}
                                    {unitOfMeasureString}
                                </label>
                            </div>
                            <div className="col">
                                <input
                                    type="text"
                                    className="form-control form-control-sm"
                                    value={editorStore.valuesByIndexId.get(id)}
                                    onChange={onChange}
                                    data-checkup-index-id={id}
                                    id={htmlId}
                                    maxLength="1000"
                                />
                            </div>
                        </div>
                    )
                })
            }
        </div>
    )
})

const CheckupExaminationResultIndexesEditor = ({ examinationResultIndexes, editorStore, onValueChange }) => {
    return (
        examinationResultIndexes.map(examinationResultIndex => {
            return (
                <CheckupExaminationResultIndexesForm
                    key={examinationResultIndex.examinationName}
                    editorStore={editorStore}
                    examinationResultIndex={examinationResultIndex}
                    onValueChange={onValueChange}
                />
            )
        })
    )
}

export default CheckupExaminationResultIndexesEditor;