import React from 'react';
import { Dropdown, DropdownButton } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileDownload } from '@fortawesome/free-solid-svg-icons';

const DocumentsDropdown = ({ docs = [], ...props }) => {
    if (!docs.length) return null;

    return (
        <DropdownButton title={<><FontAwesomeIcon icon={faFileDownload} /> Документы</>}
            variant="secondary"
            style={{ display: 'inline-block' }}
        >
            {
                docs.map(doc => (
                    <Dropdown.Item href={doc.href} key={doc.title}>
                        {doc.title}
                    </Dropdown.Item>
                ))
            }
        </DropdownButton>
    );
}

export default DocumentsDropdown;