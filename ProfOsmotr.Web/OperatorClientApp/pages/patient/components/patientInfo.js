import React from 'react';

const PatientInfo = ({ fullName, dateOfBirth, gender, address }) => {
    const titleColClassName = "col-md-2 font-weight-bold";
    const dataColClassName = "col-md-10";

    return (
        <div className="mb-3">
            <div className="row">
                <div className={titleColClassName}>
                    ФИО
                </div>
                <div className={dataColClassName}>
                    <p className="h4">{fullName}</p>
                </div>
            </div>
            <div className="row">
                <div className={titleColClassName}>
                    Пол
                </div>
                <div className={dataColClassName}>
                    {gender === "male" ? "Мужской" : gender === "female" ? "Женский" : ""}
                </div>
                <div className={titleColClassName}>
                    Дата рождения
                </div>
                <div className={dataColClassName}>
                    {dateOfBirth}
                </div>
            </div>
            <div className="row">
                <div className={titleColClassName}>
                    Адрес
                </div>
                <div className={dataColClassName}>
                    {address}
                </div>
            </div>
        </div>
    )
}

export default PatientInfo;