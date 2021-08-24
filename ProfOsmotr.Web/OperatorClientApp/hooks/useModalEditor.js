import { useHistory } from "react-router-dom";

const useModalEditor = (modalStore, editorStore) => {
    const history = useHistory();

    const onSubmit = async () => {
        const response = await editorStore.onSubmit();
        if (response && response.success !== false) {
            modalStore.close();
            history.go(0);
        }
    }

    const onHide = () => {
        modalStore.close();
    }

    const onEnter = () => {
        editorStore.loadInitialValues();
    }

    const onExited = () => {
        editorStore.clear();
    }

    return {
        onSubmit,
        onHide,
        onEnter,
        onExited
    }
}

export default useModalEditor;