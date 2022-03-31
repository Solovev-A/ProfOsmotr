import React from 'react';

const ErrorMessage = ({ error }) => {
    return (
        <div className="alert alert-warning">
            <h4>Что-то пошло не так</h4>
            <p>
                {error}
            </p>
        </div>
    )
}

export default ErrorMessage;