import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const PatientExamiantionsList = ({ items, getUrlToItemPage }) => {
    if (!items.length) {
        return (
            <div className="text-center font-italic">
                Список пуст
            </div>
        )
    }



    // TODO
    return (
        <>
            Список медосмотров
        </>
    )
}

export default PatientExamiantionsList;