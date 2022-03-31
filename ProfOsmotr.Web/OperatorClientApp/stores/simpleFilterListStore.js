import { makeAutoObservable } from "mobx";

class SimpleFilterListStore {
    constructor() {
        this.reset();
        makeAutoObservable(this);
    }

    onSearch = (search) => {
        const regExpSafeSearch = search.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
        const reg = new RegExp(regExpSafeSearch, 'i');

        this.searchResults = this.options
            .filter(option => reg.test(option.name));
    }

    setOptions = (newOptions) => {
        this.options = [...newOptions];
        this.searchResults = [...newOptions];
    }

    reset = () => {
        this.options = [];
        this.searchResults = [];
    }
}

export default SimpleFilterListStore;