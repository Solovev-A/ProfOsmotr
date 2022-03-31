import React from 'react';

const ExaminationStatusText = ({ examination }) => {
    const isCompleted = examination.isCompleted;
    const className = isCompleted ? 'text-success' : 'text-info';

    return (
        <span className={className}>{isCompleted ? 'Завершен' : 'В работе'}</span>
    )
}

export default ExaminationStatusText;