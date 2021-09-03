import React, { useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { observer } from 'mobx-react-lite';
import { useHistory } from "react-router-dom";

import SubmitBtn from './forms/general/submitBtn';
import Spinner from './spinner';
import useErrorHandler from '../hooks/useErrorHandler';

const EditorModal = ({ modalStore, editorStore, children, title,
    reloadOnSubmit = true, onSubmitted, ...props }) => {
    const history = useHistory();
    const errorHandler = useErrorHandler();

    const [isSubmitted, setIsSubmitted] = useState(false);

    const { onEnter = null, onExited = null, ...modalProps } = props;

    const onSubmit = async () => {
        const response = await editorStore.onSubmit();
        if (response && response.success !== false) {
            setIsSubmitted(true);
            if (onSubmitted) {
                onSubmitted(response);
            }
            modalStore.close();
        }
    }

    const onHide = () => {
        modalStore.close();
    }

    const handleEnter = () => {
        setIsSubmitted(false);
        editorStore.loadInitialValues()
            .catch(errorHandler);

        if (onEnter) {
            onEnter();
        }
    }

    const handleExited = () => {
        editorStore.clear();

        if (onExited) {
            onExited();
        }

        if (isSubmitted && reloadOnSubmit) {
            // перезагрузка страницы
            // именно в этом обработчике: replace без таймаута не работает
            setTimeout(() => {
                const currentPath = history.location.pathname;
                history.replace('/none');
                history.replace(currentPath);
            }, 0)
        }
    }

    const body = editorStore.isLoading
        ? <Spinner />
        : children;

    return (
        <Modal
            centered
            size='lg'
            backdrop='static'
            show={modalStore.isOpen}
            onHide={onHide}
            onEnter={handleEnter}
            onExited={handleExited}
            {...modalProps}
        >
            <Modal.Header closeButton>
                <Modal.Title>
                    {title}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {body}
            </Modal.Body>
            <Modal.Footer>
                <SubmitBtn
                    onClick={onSubmit}
                    processing={editorStore.isProcessing}
                >
                    Сохранить
                </SubmitBtn>
            </Modal.Footer>
        </Modal>
    )
}

export default observer(EditorModal);