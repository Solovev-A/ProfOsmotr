import React from 'react';

import routes from './../../../routes'
import PatientExamiantionsList from './patientExaminationsList';

const PatientExamiantions = ({ hasExaminations, preliminaryExaminations, contingentCheckupStatuses }) => {
    if (!hasExaminations) {
        return (
            <div className="text-center font-italic">
                Нет зарегистрированных медосмотров
            </div>
        )
    }

    return (
        <>
            <h4>Предварительные медицинские осмотры</h4>
            <PatientExamiantionsList items={preliminaryExaminations}
                getUrlToItemPage={routes.preliminaryExaminations.getUrl}
            />
            <h4>Периодические медицинские осмотры</h4>
            <PatientExamiantionsList items={contingentCheckupStatuses}
                getUrlToItemPage={routes.contingentCheckupStatuses.getUrl}
            />
        </>
    )
}

export default PatientExamiantions;