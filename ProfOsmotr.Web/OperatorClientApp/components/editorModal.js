import React, { useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { observer } from 'mobx-react-lite';
import { useHistory } from "react-router-dom";

import SubmitBtn from './forms/general/submitBtn';
import Spinner from './spinner';
import useErrorHandler from '../hooks/useErrorHandler';

const EditorModal = ({ modalStore, editorStore, children, title, scrollable = true, reloadOnSubmit = true, ...props }) => {
    const history = useHistory();
    const errorHandler = useErrorHandler();

    const [isSubmitted, setIsSubmitted] = useState(false);

    const onSubmit = async () => {
        const response = await editorStore.onSubmit();
        if (response && response.success !== false) {
            setIsSubmitted(true);
            modalStore.close();
        }
    }

    const onHide = () => {
        modalStore.close();
    }

    const onEnter = () => {
        setIsSubmitted(false);
        editorStore.loadInitialValues()
            .catch(errorHandler);

        if (props.onEnter) {
            props.onEnter();
        }
    }

    const onExited = () => {
        editorStore.clear();

        if (props.onExited) {
            props.onExited();
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
            onEnter={onEnter}
            onExited={onExited}
            scrollable={scrollable}
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