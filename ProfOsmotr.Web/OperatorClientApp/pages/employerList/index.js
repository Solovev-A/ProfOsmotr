import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import Spinner from '../../components/spinner';
import SearchInput from '../../components/searchInput';
import Pagination from './../../components/pagination';
import Card from './../../components/card';
import useListStore from './../../hooks/useListStore';
import EmployerListActions from './components/employerListActions';
import ItemsList from './../../components/itemsList';
import routes from './../../routes';

const EmployerListPage = (props) => {
    const { employersStore } = useStore();
    useListStore(employersStore);

    const { items, inProgress, inSearch, totalCount, page, totalPages, loadPage, onSearch } = employersStore;

    const listColumns = [
        { title: 'Название', render: (item) => item.name }
    ]

    return (
        <>
            <EmployerListActions />
            <SearchInput placeholder="название" disabled={inProgress} onSearch={onSearch} />
            <Card title={inSearch ? `Результаты поиска: ${totalCount}` : "Недавно добавленные организации"}>
                {inProgress
                    ? <Spinner />
                    : <ItemsList columns={listColumns}
                        items={items}
                        onItemCLick={(item) => props.history.push(routes.employer.getUrl(item.id))} />
                }
                {totalPages > 1
                    ? <Pagination currentPage={page} totalPages={totalPages} onPageChange={loadPage} />
                    : null
                }
            </Card>
        </>
    )
}

export default observer(EmployerListPage);