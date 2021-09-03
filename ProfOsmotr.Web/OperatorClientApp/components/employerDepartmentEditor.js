import React from 'react';

import useStore from './../hooks/useStore';
import InputField from './forms/observers/inputField';

const EmployerDepartmentEditor = () => {
    const { employerDepartmentEditorStore } = useStore();

    return (
        <>
            <InputField
                formStore={employerDepartmentEditorStore}
                label="Название"
                name="name"
                maxLength="500"
            />
        </>
    )
}

export default EmployerDepartmentEditor;