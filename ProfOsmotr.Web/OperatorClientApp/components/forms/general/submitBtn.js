import React from 'react';

const SubmitBtn = ({ disabled, processing, children, ...buttonProps }) => {
    return (
        <button {...buttonProps}
            className={buttonProps.className || "btn btn-primary"}
            type="submit"
            disabled={disabled || processing}
        >
            {processing
                ? <><span className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Загрузка...</>
                : children
            }
        </button>
    )
}

export default SubmitBtn;