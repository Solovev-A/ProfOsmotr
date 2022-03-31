import { makeAutoObservable } from 'mobx';

class ModalStore {
    isOpen = false;

    constructor() {
        makeAutoObservable(this);
    }

    open = () => {
        this.isOpen = true;
    }

    close = () => {
        this.isOpen = false;
    }
}

export default ModalStore;