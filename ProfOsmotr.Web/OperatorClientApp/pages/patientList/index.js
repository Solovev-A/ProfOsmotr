import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import Spinner from '../../components/spinner';
import SearchInput from '../../components/searchInput';
import PatientListActions from './components/patientListActions';
import Pagination from './../../components/pagination';
import Card from './../../components/card';
import useListPage from './../../hooks/useListPage';
import ItemsList from './../../components/itemsList';
import routes from './../../routes';
import useTitle from './../../hooks/useTitle';

const PatientListPage = (props) => {
    useTitle('Пациенты');
    const { patientsListStore } = useStore();
    useListPage(patientsListStore);

    const { items, inProgress, inSearch, totalCount, page, totalPages, onSearch, loadPage } = patientsListStore;

    const listColumns = [
        { title: 'ФИО', width: '70%', render: (item) => `${item.lastName} ${item.firstName} ${item.patronymicName}` },
        { title: 'Дата рождения', render: (item) => item.dateOfBirth }
    ]

    return (
        <>
            <PatientListActions />
            <SearchInput placeholder="фамилия имя отчество" disabled={inProgress} onSearch={onSearch} />
            <Card title={inSearch ? `Результаты поиска: ${totalCount}` : "Недавно добавленные пациенты"}>
                {inProgress
                    ? <Spinner />
                    : <ItemsList
                        columns={listColumns}
                        items={items}
                        getItemUrl={(item) => routes.patient.getUrl(item.id)}
                    />}
                {totalPages > 1
                    ? <Pagination currentPage={page} totalPages={totalPages} onPageChange={loadPage} />
                    : null}
            </Card>
        </>
    )
}

export default observer(PatientListPage);