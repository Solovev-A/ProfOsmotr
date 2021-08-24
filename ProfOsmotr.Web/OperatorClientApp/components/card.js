import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';

const Card = ({ title, children }) => {
    return (
        <div className='card mb-3'>
            <div className="card-header font-weight-bold">
                {title}
            </div>
            <div className="card-body">
                {children}
            </div>
        </div >
    )
}

const EditableCard = ({ title, children, onEditClick }) => {
    const titleStyle = {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center'
    };

    return (
        <Card title=
            {
                <div style={titleStyle}>
                    <div>{title}</div>
                    <button className='btn btn-sm btn-secondary'
                        title='Редактировать'
                        onClick={onEditClick}
                    >
                        <FontAwesomeIcon icon={faEdit} />
                    </button>
                </div>
            }
        >
            {children ?? <div className='text-center font-italic'>Не задано</div>}
        </Card>
    )
}

export default Card;
export { EditableCard };