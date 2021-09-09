import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import Spinner from '../../components/spinner';
import SearchInput from '../../components/searchInput';
import Pagination from './../../components/pagination';
import Card from './../../components/card';
import useListPage from '../../hooks/useListPage';
import ItemsList from './../../components/itemsList';
import routes from './../../routes';


const listColumns = [{
    title: 'Организация',
    width: '45%',
    render: (item) => item.employer
}, {
    title: 'Год',
    width: '15%',
    render: (item) => item.examinationYear
}, {
    title: 'Статус',
    width: '20%',
    render: (item) => {
        const className = item.isCompleted ? 'text-success' : 'text-info';
        return (
            <span className={className}>{item.isCompleted ? 'Завершен' : 'В работе'}</span>
        )
    }
}, {
    title: 'Дата акта',
    render: (item) => item.reportDate ?? '-'
}];


const PeriodicExaminationsListPage = observer((props) => {
    const { periodicExaminationsStore } = useStore();
    useListPage(periodicExaminationsStore);

    const { items, inProgress, inSearch, totalCount, page, totalPages, loadPage, onSearch } = periodicExaminationsStore;

    return (
        <>
            <SearchInput placeholder="название организации" disabled={inProgress} onSearch={onSearch} />
            <Card title={inSearch ? `Результаты поиска: ${totalCount}` : "Недавно добавленные медосмотры"}>
                {inProgress
                    ? <Spinner />
                    : <ItemsList columns={listColumns}
                        items={items}
                        getItemUrl={(item) => routes.periodicExamination.getUrl(item.id)} />
                }
                {totalPages > 1
                    ? <Pagination currentPage={page} totalPages={totalPages} onPageChange={loadPage} />
                    : null
                }
            </Card>
        </>
    )
})

export default PeriodicExaminationsListPage;