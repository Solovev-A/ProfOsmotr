import React from 'react';
import useStore from './../hooks/useStore';
import InputField from './forms/observers/inputField';

const EmployerEditor = (props) => {
    const { employerEditorStore } = useStore();

    return (
        <>
            <div className="row">
                <div className="col">
                    <InputField
                        formStore={employerEditorStore}
                        label="Название"
                        name="name"
                        maxLength="500"
                    />
                </div>
            </div>
            <h4>Сведения о руководителе</h4>
            <div className="row">
                <div className="col-md-4">
                    <InputField
                        formStore={employerEditorStore}
                        label="Фамилия"
                        name="headLastName"
                        maxLength="70"
                    />
                </div>
                <div className="col-md-4">
                    <InputField
                        formStore={employerEditorStore}
                        label="Имя"
                        name="headFirstName"
                        maxLength="70"
                    />
                </div>
                <div className="col-md-4">
                    <InputField
                        formStore={employerEditorStore}
                        label="Отчество"
                        name="headPatronymicName"
                        maxLength="70"
                    />
                </div>
            </div>
            <div className="row">
                <div className="col-md-4">
                    <InputField
                        formStore={employerEditorStore}
                        label="Должность"
                        name="headPosition"
                        maxLength="70"
                    />
                </div>
            </div>
        </>
    )
}

export default EmployerEditor;