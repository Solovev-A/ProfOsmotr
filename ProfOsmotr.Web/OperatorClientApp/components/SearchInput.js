import { runInAction } from 'mobx';
import { observer } from 'mobx-react-lite';
import React from 'react';

const SearchInput = ({ search, placeholder, onSearch }) => {

    const onInputChange = (event) => {
        runInAction(() => {
            search.query = event.target.value;
        })
    }

    const onKeyDown = (event) => {
        if (event.keyCode === 13) {
            // 'Enter' key
            event.preventDefault();
            onSearch();
        }
    }

    return (
        <div className='card'>
            <div className="card-header">
                Поиск
            </div>
            <div className="card-body">
                <div className="form-inline">
                    <input
                        type="text"
                        className="form-control flex-grow-1 mr-3"
                        placeholder={placeholder}
                        value={search.query}
                        onChange={onInputChange}
                        onKeyDown={onKeyDown}
                    />
                    <button
                        type="button"
                        className="btn btn-primary"
                        onClick={onSearch}
                        disabled={search.isDisabled}
                    >Найти</button>
                </div>
            </div>
        </div >
    )
}

export default observer(SearchInput);