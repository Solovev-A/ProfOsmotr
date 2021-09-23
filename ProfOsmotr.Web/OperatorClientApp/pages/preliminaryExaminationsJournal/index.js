import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import Spinner from '../../components/spinner';
import YearSelect from '../../components/yearSelect';
import Pagination from './../../components/pagination';
import Card from './../../components/card';
import ItemsList from './../../components/itemsList';
import routes from './../../routes';
import useJournalPage from './../../hooks/useJournalPage';


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
    const { journal } = preliminaryExaminationsStore;

    useJournalPage(preliminaryExaminationsStore);

    const { items, inProgress, totalCount, page, totalPages, loadPage } = journal;

    return (
        <>
            <h2>Журнал предварительных медосмотров</h2>
            <YearSelect examinationsStore={preliminaryExaminationsStore} />
            <Card title={`Количество записей: ${totalCount}`}>
                {inProgress
                    ? <Spinner />
                    : <ItemsList columns={listColumns}
                        items={items}
                        getItemUrl={(item) => routes.preliminaryExamination.getUrl(item.id)} />
                }
                {totalPages > 1
                    ? <Pagination currentPage={page} totalPages={totalPages} onPageChange={loadPage} />
                    : null
                }
            </Card>
        </>
    )
}


export default observer(PreliminaryExaminationsJournalPage);