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
import PreliminaryExaminationListActions from './components/preliminaryExaminationListActions';

const listColumns = [{
    title: 'Работник',
    width: '30%',
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
    title: 'Место работы',
    width: '30%',
    render: (item) => {
        const employer = item.employerName
            ? <>{item.employerName}<br /></>
            : null;

        const profession = item.profession
            ? <>{item.profession}{': '}{item.orderItems.join(', ')}</>
            : null;

        return (
            <>
                {employer}
                {profession}
            </>
        )
    }
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
    title: 'Дата завершения',
    render: (item) => item.dateOfCompletion ?? '-'
}];


const PreliminaryExaminationsListPage = (props) => {
    const { preliminaryExaminationsStore } = useStore();
    useListPage(preliminaryExaminationsStore);

    const { items, inProgress, inSearch, totalCount, page, totalPages, loadPage, onSearch } = preliminaryExaminationsStore;

    return (
        <>
            <PreliminaryExaminationListActions />
            <SearchInput placeholder="фио работника" disabled={inProgress} onSearch={onSearch} />
            <Card title={inSearch ? `Результаты поиска: ${totalCount}` : "Недавно добавленные медосмотры"}>
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

export default observer(PreliminaryExaminationsListPage);