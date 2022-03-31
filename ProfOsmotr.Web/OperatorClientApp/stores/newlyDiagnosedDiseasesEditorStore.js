import { makeAutoObservable, observable } from "mobx";


class NewlyDiagnosedDiseasesEditorStore {
    constructor() {
        this._initialCasesByChapterId = observable.map();
        this.casesByChapterId = observable.map();
        makeAutoObservable(this);
    }

    setInitialValues = (values) => {
        if (!values) return;
        values.forEach(element => {
            this._initialCasesByChapterId.set(element.chapterId, element.cases);
            this.casesByChapterId.set(element.chapterId, element.cases);
        });
    }

    updateValue = (chapterId, cases) => {
        this.casesByChapterId.set(chapterId, cases);
    }

    clear = () => {
        this._initialCasesByChapterId.clear();
        this.casesByChapterId.clear();
    }

    get patchedData() {
        const data = [...this.casesByChapterId.entries()];

        return data.filter(([chapterId, cases]) => {
            return cases !== this._initialCasesByChapterId.get(chapterId);
        }).map(([chapterId, cases]) => ({ chapterId, cases }));
    }
}

export default NewlyDiagnosedDiseasesEditorStore;