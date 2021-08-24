import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Autocomplete } from 'react-dropdown-components';

import useStore from '../hooks/useStore';

const EmployerAutocomplete = ({ value, onChange, disabled = false }) => {
    const { employersStore } = useStore();
    const { inProgress, items, onSearch, reset, resetEmployer } = employersStore;

    useEffect(() => {
        return () => {
            reset();
        }
    }, [])

    return (
        <div className="form-group">
            <label>Организация</label>
            <Autocomplete
                options={items}
                value={value}
                onChange={onChange}
                onSearchChange={(search) => onSearch(search, { toastOnNotFound: false })}
                isLoading={inProgress}
                renderOptionText={(employer) => employer.name}
                disabled={disabled}
            />
        </div>
    );
}

export default observer(EmployerAutocomplete);