import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faPencilAlt, faClone } from '@fortawesome/free-solid-svg-icons';

const Button = ({ className, children, ...props }) => {
    return (
        <button className={className || "btn btn-secondary"}
            {...props}
        >
            {children}
        </button>
    )
}

const AddBtn = (props) => {
    return (
        <Button {...props} title="Добавить">
            <FontAwesomeIcon icon={faPlus} />
            {props.children}
        </Button>
    );
}

const EditBtn = (props) => {
    return (
        <Button {...props} title="Редактировать">
            <FontAwesomeIcon icon={faPencilAlt} />
            {props.children}
        </Button>
    );
}

const CloneBtn = (props) => {
    return (
        <Button {...props} title="Клонировать">
            <FontAwesomeIcon icon={faClone} />
            {props.children}
        </Button>
    );
}

export { AddBtn, EditBtn, CloneBtn };