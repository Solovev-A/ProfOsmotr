import React, { useState } from 'react';
import Card from './card';
import SubmitBtn from './forms/general/submitBtn';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';


const SearchInput = ({ disabled, placeholder, onSearch }) => {
    const [searchQuery, setSearchQuery] = useState('');

    const onKeyDown = (event) => {
        if (event.keyCode === 13) {
            // 'Enter' key
            event.preventDefault();
            onSearch(searchQuery);
        }
    }

    const handleBtnClick = (event) => {
        event.preventDefault();
        onSearch(searchQuery);
    }

    return (
        <Card title={<><FontAwesomeIcon icon={faSearch} /> Поиск</>}>
            <div className="form-inline">
                <input
                    type="text"
                    className="form-control flex-grow-1 mr-3"
                    placeholder={placeholder}
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    onKeyDown={onKeyDown}
                />
                <SubmitBtn disabled={disabled} processing={disabled} onClick={handleBtnClick}>
                    Найти
                </SubmitBtn>
            </div>
        </Card>
    )
}

export default SearchInput;