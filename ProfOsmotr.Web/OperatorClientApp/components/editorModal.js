import React from 'react';
import Modal from 'react-bootstrap/Modal';
import { observer } from 'mobx-react-lite';

import SubmitBtn from './forms/general/submitBtn';
import Spinner from './spinner';
import useModalEditor from '../hooks/useModalEditor';


const EditorModal = ({ modalStore, editorStore, children, title, scrollable = true, ...props }) => {
    const { onSubmit, onHide, onEnter, onExited } = useModalEditor(modalStore, editorStore);

    const body = editorStore.isLoading
        ? <Spinner />
        : children;

    const handleEnter = () => {
        onEnter();

        if (props.onEnter) {
            props.onEnter();
        }
    }

    const handleExited = () => {
        onExited();

        if (props.onExited) {
            props.onExited();
        }
    }

    return (
        <Modal
            centered
            size='lg'
            backdrop='static'
            show={modalStore.isOpen}
            onHide={onHide}
            onEnter={handleEnter}
            onExited={handleExited}
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