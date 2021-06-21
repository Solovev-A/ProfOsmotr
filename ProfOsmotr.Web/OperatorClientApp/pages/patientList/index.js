import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import Spinner from '../../components/spinner';
import SearchInput from '../../components/searchInput';
import useErrorHandler from '../../hooks/useErrorHandler';
import PatientListActions from './components/patientListActions';
import PatientList from './components/patientList';
import Pagination from './../../components/pagination';
import Card from './../../components/card';

const PatientListPage = () => {
    const { patientsListStore } = useStore();
    const errorHandler = useErrorHandler();

    useEffect(() => {
        patientsListStore.loadActualPatients()
            .catch(errorHandler);

        return () => {
            patientsListStore.reset();
        }
    }, []);

    const handleSearchRequest = (searchQuerry) => {
        patientsListStore.onSearch(searchQuerry);
    }

    const handlePageChange = (newPage) => {
        patientsListStore.loadPage(newPage);
    }

    const { inProgress, inSearch, totalCount, page, totalPages } = patientsListStore;

    return (
        <>
            <PatientListActions />
            <SearchInput placeholder="фамилия имя отчество" disabled={inProgress} onSearch={handleSearchRequest} />
            <Card title={inSearch ? `Результаты поиска: ${totalCount}` : "Недавно добавленные пациенты"}>
                {inProgress
                    ? <Spinner />
                    : <PatientList items={patientsListStore.items} />}
                {totalPages > 1
                    ? <Pagination currentPage={page} totalPages={totalPages} onPageChange={handlePageChange} />
                    : null}
            </Card>
        </>
    )
}

export default observer(PatientListPage);