import React from 'react';

const EmployerInfo = ({ employer }) => {
    const titleColClassName = "col-md-3 font-weight-bold";
    const dataColClassName = "col-md-9";

    const { name, headLastName, headFirstName, headPatronymicName, headPosition } = employer;

    return (
        <div className="mb-3">
            <div className="row">
                <div className={titleColClassName}>
                    Название
                </div>
                <div className={dataColClassName}>
                    <p className="h4">{name}</p>
                </div>
            </div>
            <div className="row">
                <div className={titleColClassName}>
                    ФИО руководителя
                </div>
                <div className={dataColClassName}>
                    <p>{`${headLastName} ${headFirstName} ${headPatronymicName}`}</p>
                </div>
            </div>
            <div className="row">
                <div className={titleColClassName}>
                    Должность руководителя
                </div>
                <div className={dataColClassName}>
                    <p>{headPosition}</p>
                </div>
            </div>
        </div>
    )
}

export default EmployerInfo;