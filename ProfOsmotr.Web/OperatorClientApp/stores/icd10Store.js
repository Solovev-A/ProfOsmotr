import { makeAutoObservable, runInAction } from "mobx";

import api from "../services/api";
import { errorToast } from "../utils/toasts";

class ICD10Store {
    chapters = [];

    constructor() {
        makeAutoObservable(this);
    }

    load = async () => {
        const response = await api.get('/icd10');
        if (response && response.success !== false) {
            runInAction(() => {
                this.chapters = response;
            })
        } else {
            errorToast('Ошибка при загрузке справочника МКБ');
        }
    }

    getChapterById = (id) => {
        if (!this.isLoaded) throw 'Невозможно найти главу до загрузки';

        const result = this.chapters.find(chapter => chapter.id === id);
        if (!result) throw `Глава МКБ-10 c id {${id}} не найдена`;

        return result;
    }

    get isLoaded() {
        return !!this.chapters.length;
    }
}

export default ICD10Store;