import React from 'react';
import { observer } from 'mobx-react-lite';

const YearSelect = ({ examinationsStore }) => {
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
}

export default observer(YearSelect);