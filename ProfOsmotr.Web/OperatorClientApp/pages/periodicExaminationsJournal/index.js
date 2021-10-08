import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import JournalPage from '../../components/journalPage';
import routes from './../../routes';
import ExaminationStatusText from './../../components/examinationStatusText';
import useTitle from '../../hooks/useTitle';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileDownload } from '@fortawesome/free-solid-svg-icons';


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

    const YearReportBtn = () => {
        if (!periodicExaminationsStore.journal.items.length) return null;

        return (
            <a href={`${routes.periodicExaminationsYearReport.path}?year=${periodicExaminationsStore.journalYear}`}
                className="btn btn-secondary"
            >
                <FontAwesomeIcon icon={faFileDownload} /> Годовой отчет
            </a>
        )
    }

    return (
        <JournalPage
            title="Журнал периодических медосмотров"
            examinationsStore={periodicExaminationsStore}
            examinationRoute={routes.periodicExamination}
            listColumns={listColumns}
            actions={[YearReportBtn]}
        />
    )
}


export default observer(PeriodicExaminationsJournalPage);