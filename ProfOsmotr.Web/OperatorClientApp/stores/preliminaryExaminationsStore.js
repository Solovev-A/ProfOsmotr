import { makeAutoObservable } from 'mobx';

class PreliminaryExaminationsStore {
    search = {
        isDisabled: false,
        query: ''
    }

    constructor() {
        makeAutoObservable(this);
    }

    searchExaminations = () => {
        console.log(this.search.query);
    }
}

export default PreliminaryExaminationsStore;