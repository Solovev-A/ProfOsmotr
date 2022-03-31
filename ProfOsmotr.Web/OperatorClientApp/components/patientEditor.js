import React from 'react';
import useStore from './../hooks/useStore';
import InputField from './forms/observers/inputField';
import { CheckInput, InputRadioWrapper } from './forms/general';
import { observer } from 'mobx-react-lite';

const PatientEditor = (props) => {
    const { patientEditorStore } = useStore();

    return (
        <>
            <div className="row">
                <div className="col-md-4">
                    <InputField label="Фамилия"
                        name="lastName"
                        formStore={patientEditorStore}
                        maxLength="70"
                    />
                </div>
                <div className="col-md-4">
                    <InputField label="Имя"
                        name="firstName"
                        formStore={patientEditorStore}
                        maxLength="70"
                    />
                </div>
                <div className="col-md-4">
                    <InputField label="Отчество"
                        name="patronymicName"
                        formStore={patientEditorStore}
                        maxLength="70"
                    />
                </div>
            </div>
            <div className="row justify-content-start">
                <div className="col-md-4">
                    <InputRadioWrapper legend="Пол"
                        name="gender"
                        onChange={patientEditorStore.updateProperty}
                        isInvalid={patientEditorStore.errors.gender}
                        errorMessage={patientEditorStore.errors.gender}
                    >
                        <CheckInput label="Мужской" id="male" value="male" checked={patientEditorStore.model.gender === 'male'} />
                        <CheckInput label="Женский" id="female" value="female" checked={patientEditorStore.model.gender === 'female'} />
                    </InputRadioWrapper>
                </div>
                <div className="col-md-2">
                    <InputField label="Дата рождения"
                        name="dateOfBirth"
                        type="date"
                        formStore={patientEditorStore}
                    />
                </div>
            </div>
            <div className="row">
                <div className="col">
                    <InputField label="Адрес"
                        name="address"
                        formStore={patientEditorStore}
                        maxLength="500"
                    />
                </div>
            </div>
        </>
    )
}

export default observer(PatientEditor);