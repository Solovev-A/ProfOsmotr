import React from 'react';
import { observer } from 'mobx-react-lite';

import Spinner from './spinner';
import Pagination from './pagination';
import Card from './card';
import ItemsList from './itemsList';
import useJournalPage from '../hooks/useJournalPage';
import YearPicker from './yearPicker';


const JournalPage = ({ examinationsStore, title, listColumns, examinationRoute }) => {
    const { journalYear, setJournalYear, journal } = examinationsStore;

    useJournalPage(examinationsStore);

    const { items, inProgress, totalCount, page, totalPages, loadPage } = journal;

    return (
        <>
            <h2>{title}</h2>
            <YearPicker title={`Год: ${journalYear}`} onYearPick={setJournalYear} className="mb-3" />
            <Card title={`Количество записей: ${totalCount}`}>
                {inProgress
                    ? <Spinner />
                    : <ItemsList columns={listColumns}
                        items={items}
                        getItemUrl={(item) => examinationRoute.getUrl(item.id)} />
                }
                {totalPages > 1
                    ? <Pagination currentPage={page} totalPages={totalPages} onPageChange={loadPage} />
                    : null
                }
            </Card>
        </>
    )
}


export default observer(JournalPage);