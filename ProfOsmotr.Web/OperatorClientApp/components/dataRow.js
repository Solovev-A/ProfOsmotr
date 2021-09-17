import React from 'react';

const DataRow = ({ title, value, br = true, required = false }) => {
    if (!value && !required) return null;

    return (
        <>
            {title}: <b>{value}</b>
            {br && <br />}
        </>
    )
}

export default DataRow;