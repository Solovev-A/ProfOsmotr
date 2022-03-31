import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar } from '@fortawesome/free-solid-svg-icons';

const SuggestionsMark = (props) => {
    return (
        <FontAwesomeIcon icon={faStar}
            style={{ marginRight: '.5rem' }}
            {...props}
        />
    );
}

export default SuggestionsMark;