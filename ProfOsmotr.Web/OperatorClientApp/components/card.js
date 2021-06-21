import React from 'react';

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

export default Card;