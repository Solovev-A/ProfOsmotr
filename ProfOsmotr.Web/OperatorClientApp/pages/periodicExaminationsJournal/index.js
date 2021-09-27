import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import JournalPage from '../../components/journalPage';
import routes from './../../routes';
import ExaminationStatusText from './../../components/examinationStatusText';
import useTitle from '../../hooks/useTitle';


const listColumns = [{
    title: 'Дата акта',
    width: '15%',
    render: (item) => item.reportDate ?? '-'
}, {
    title: 'Организация',
    render: (item) => item.employer
}, {
    title: 'Статус',
    width: '20%',
    render: (item) => <ExaminationStatusText examination={item} />
}];


const PeriodicExaminationsJournalPage = (props) => {
    useTitle('Журнал периодических медосмотров');

    const { periodicExaminationsStore } = useStore();

    return (
        <JournalPage
            title="Журнал периодических медосмотров"
            examinationsStore={periodicExaminationsStore}
            examinationRoute={routes.periodicExamination}
            listColumns={listColumns}
        />
    )
}


export default observer(PeriodicExaminationsJournalPage);