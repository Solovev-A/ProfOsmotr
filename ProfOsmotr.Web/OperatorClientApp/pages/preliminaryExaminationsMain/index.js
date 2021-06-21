import { observer } from 'mobx-react-lite';
import React from 'react';
import SearchInput from '../../components/searchInput';
import useStore from '../../hooks/useStore';

const PreliminaryExaminations = () => {
    let { search, searchExaminations } = useStore();
    console.log('ререндер');

    return (
        <>
            <SearchInput
                placeholder={'фамилия имя отчество'}
                search={search}
                onSearch={searchExaminations}
            />
        </>
    )
}

export default observer(PreliminaryExaminations);