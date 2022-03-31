import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLink } from '@fortawesome/free-solid-svg-icons';

const LinkIcon = ({ url, children }) => {
    return (
        <Link to={url}>
            {children} <FontAwesomeIcon icon={faLink} />
        </Link>
    );
}

export default LinkIcon;