import React from 'react';

import InputField from './../../../components/forms/observers/inputField';


const EmployerDataView = ({ employerData = null, formStore = null }) => {
    if (!(!!employerData ^ !!formStore)) throw 'Недопустимые пропсы. Необходимо передать либо employerData, либо formStore'

    const valueGroupCommonProps = {
        employerData,
        formStore
    }

    return (
        <table className="table">
            <thead>
                <tr>
                    <th></th>
                    <th><span>Всего</span></th>
                    <th><span>Женщин</span></th>
                    <th>
                        <span title="Численность работников в возрасте до 18 лет">До 18 лет</span>
                    </th>
                    <th>
                        <span title="Численность работников, которым установлена стойкая степень утраты трудоспособности">
                            Инвалиды
                        </span>
                    </th>
                </tr>
            </thead>
            <tbody>
                <ValueGroup title="Общая численность работников"
                    prefix="employees"
                    {...valueGroupCommonProps}
                />
                <ValueGroup title="Численность работников, занятых на работах с вредными и (или) опасными условиями труда"
                    prefix="workingWithHarmfulFactors"
                    {...valueGroupCommonProps}
                />
                <ValueGroup title="Численность работников, занятых на работах, при выполнении которых обязательно проведение периодических медицинских осмотров"
                    prefix="workingWithJobTypes"
                    {...valueGroupCommonProps}
                />
            </tbody>
        </table>
    );
}

const ValueGroup = ({ title, prefix, formStore, employerData }) => {
    const ValueView = ({ path }) => {
        return (
            <>
                {
                    employerData
                        ? employerData[prefix + path]
                        : <InputField
                            name={prefix + path}
                            formStore={formStore}
                            type="number"
                            min="0"
                        />
                }
            </>
        )
    }

    const titleColWidthPercent = 50;
    const valueColWidthPercent = ((100 - titleColWidthPercent) / 4).toFixed(2);

    const ValueCol = ({ children }) => <td width={`${valueColWidthPercent}%`}>{children}</td>

    return (
        <tr>
            <td width={`${titleColWidthPercent}%`}>
                {title}
            </td>
            <ValueCol>
                <ValueView path="Total" />
            </ValueCol>
            <ValueCol>
                <ValueView path="Women" />
            </ValueCol>
            <ValueCol>
                <ValueView path="Under18" />
            </ValueCol>
            <ValueCol>
                <ValueView path="PersistentlyDisabled" />
            </ValueCol>
        </tr >
    )
}

export default EmployerDataView;