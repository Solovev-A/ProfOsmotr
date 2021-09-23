import React from 'react';
import { observer } from 'mobx-react-lite';

import Spinner from './spinner';
import Pagination from './pagination';
import Card from './card';
import ItemsList from './itemsList';
import useJournalPage from '../hooks/useJournalPage';


const JournalPage = ({ examinationsStore, title, listColumns, examinationRoute }) => {
    const { journal } = examinationsStore;

    useJournalPage(examinationsStore);

    const { items, inProgress, totalCount, page, totalPages, loadPage } = journal;

    return (
        <>
            <h2>{title}</h2>
            <YearSelect examinationsStore={examinationsStore} />
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

const YearSelect = observer(({ examinationsStore }) => {
    const { journalYearsRange, journalYear, setJournalYear } = examinationsStore;
    const htmlId = "journal-year";

    const handleChange = (event) => {
        const select = event.target;
        const value = Number(select.value);
        setJournalYear(value);
    }

    return (
        <div className="mb-3 form-group" style={{ width: "10%" }}>
            <label htmlFor={htmlId}>Год</label>
            <select onChange={handleChange} defaultValue={journalYear} className="form-control" id={htmlId}>
                {
                    journalYearsRange.map(y => {
                        return (
                            <option value={y} key={y}>{y}</option>
                        )
                    })
                }
            </select>
        </div>
    )
})


export default observer(JournalPage);