import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Autocomplete } from 'react-dropdown-components';
import SuggestionsMark from './suggestionsMark';


const ProfessionAutocomplete = ({ value, onChange, professionListStore, disabled = false }) => {
    const { items, onSearch, inProgress, reset } = professionListStore;

    useEffect(() => {
        return () => {
            reset();
        }
    }, [])

    const renderOption = (profession, showMark = true) => {
        const mark = profession.isSuggested && showMark
            ? <SuggestionsMark />
            : null;

        return (
            <div style={{ display: 'flex', alignItems: 'center' }}>
                {mark}
                <div>
                    {profession.name}<br />
                    {profession.orderItems.join('; ')}
                </div>
            </div>
        )
    }

    return (
        <div className="form-group">
            <label>Профессия</label>
            <Autocomplete
                options={items}
                value={value}
                onChange={onChange}
                onSearchChange={onSearch}
                isLoading={inProgress}
                renderOptionText={renderOption}
                renderValueText={(profession => renderOption(profession, false))}
                disabled={disabled}
            />
        </div>
    );
}

export default observer(ProfessionAutocomplete);