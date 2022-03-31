import React from 'react';

const Spinner = ({ size = "3rem" }) => {
    return (
        <div className="d-flex justify-content-center align-items-center h-100">
            <div className="spinner-border"
                style={{
                    width: size,
                    height: size
                }}>
            </div>
        </div>
    )
}

export default Spinner;