import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Autocomplete } from 'react-dropdown-components';

import SuggestionsMark from './../components/suggestionsMark';


const PatientAutocomplete = observer(({ value, onChange, patientListStore, disabled = false }) => {
    const { items, onSearch, inProgress, reset } = patientListStore;

    useEffect(() => {
        return () => {
            reset();
        }
    }, [])

    const renderOption = (patient, showMark = true) => {
        const mark = patient.isSuggested && showMark
            ? <SuggestionsMark />
            : null;

        return (
            <div style={{ display: 'flex', alignItems: 'center' }}>
                {mark}
                <div>
                    {patient.lastName} {patient.firstName} {patient.patronymicName}<br />
                    {patient.dateOfBirth}
                </div>
            </div>
        )
    }

    return (
        <Autocomplete
            options={items}
            value={value}
            onChange={onChange}
            onSearchChange={onSearch}
            isLoading={inProgress}
            renderOptionText={renderOption}
            renderValueText={(patient => renderOption(patient, false))}
            disabled={disabled}
            placeholder="Поиск по фио"
        />
    );
})

export default PatientAutocomplete;