import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Autocomplete } from 'react-dropdown-components';

const DepartmentAutocomplete = ({ value, onChange, hasEmployer, departmentsListStore }) => {
    const { onSearch, searchResults, options, reset } = departmentsListStore;

    useEffect(() => {
        return () => {
            reset();
        }
    }, [])

    const hasNoDepartments = !options.length;
    const placeholder = hasEmployer
        ? hasNoDepartments ? 'Нет подразделений' : 'Начните ввод для поиска'
        : 'Сначала выберите организацию'

    return (
        <div className="form-group">
            <label>Структурное подразделение</label>
            <Autocomplete
                options={searchResults}
                value={value}
                onChange={onChange}
                onSearchChange={onSearch}
                renderOptionText={(department) => department.name}
                disabled={hasNoDepartments || !hasEmployer}
                threshold={-1}
                placeholder={placeholder}
            />
        </div>
    );
}

export default observer(DepartmentAutocomplete);