import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import routes from './../../routes';
import JournalPage from '../../components/journalPage';


const listColumns = [{
    title: '№',
    width: '10%',
    render: (item) => item.registrationJournalEntryNumber
}, {
    title: 'Дата завершения',
    width: '20%',
    render: (item) => item.dateOfCompletion ?? '-'
}, {
    title: 'Работник',
    width: '35%',
    render: (item) => {
        const { patient: { lastName, firstName, patronymicName, dateOfBirth } } = item;
        return (
            <>
                {lastName} {firstName} {patronymicName}<br />
                {dateOfBirth}
            </>
        )
    }
}, {
    title: 'Адрес',

    render: (item) => item.patient.address
}];


const PreliminaryExaminationsJournalPage = (props) => {
    const { preliminaryExaminationsStore } = useStore();

    return (
        <JournalPage
            title="Журнал предварительных медосмотров"
            examinationsStore={preliminaryExaminationsStore}
            examinationRoute={routes.preliminaryExamination}
            listColumns={listColumns}
        />
    )
}


export default observer(PreliminaryExaminationsJournalPage);