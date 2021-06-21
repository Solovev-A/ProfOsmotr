import { makeAutoObservable, runInAction } from "mobx";
import { errorToast, handleResponseWithToasts } from "../utils/toasts";

import patientApiService from './../services/patientApiService';

class PatientsListStore {
    itemsPerPage = 20;

    constructor() {
        this.reset();
        makeAutoObservable(this);
    }

    onSearch = (querry) => {
        this.inSearch = true;

        // сброс состояния результата
        this.items = [];
        this.page = 1;
        this.totalCount = 0;
        this.searchQuerry = querry;

        return this._loadSearchResults();
    }

    loadPage = async (page) => {
        const pageBefore = this.page;
        this.page = page;
        const response = await this._loadSearchResults();

        if (response.success === false) {
            // указатель текущей страницы остается актуальным для уже загруженной страницы, если не получилось загрузить новую
            this.page = pageBefore;
        }
    }

    _loadSearchResults = async () => {
        if (this.searchQuerry.length < 3) {
            errorToast('Слишком короткий запрос');
            return;
        }

        this.inProgress = true;

        const querryHashBefore = this._querryHashCode;
        const response = await patientApiService.listPatients(this.searchQuerry, this.page, this.itemsPerPage);
        const querryHashAfter = this._querryHashCode;

        if (querryHashBefore !== querryHashAfter) return; // не обрабатываем ответ, если начат новый поиск

        runInAction(() => {
            this.totalCount = response.totalCount;
            this.items = response.items;
            this.inProgress = false;
        })

        if (this.totalCount === 0) errorToast('Пациент не найден');

        return handleResponseWithToasts(response);
    }

    loadActualPatients = async () => {
        this.inProgress = true;
        const response = await patientApiService.listActualPatients();

        if (this.inSearch) return; // не обновлять список актуальных пациентов, если поиск начался до его загрузки
        if (response.success === false) throw response.message;

        runInAction(() => {
            this.items = response.items;
            this.totalCount = response.totalCount;
            this.inProgress = false;
        })
    }

    reset = () => {
        this.inSearch = false;
        this.inProgress = false;
        this.items = [];
        this.page = 1;
        this.searchQuerry = '';
        this.totalCount = 0;
    }

    get _querryHashCode() {
        return this.searchQuerry + String(this.page) + String(this.itemsPerPage);
    }

    get totalPages() {
        return Math.ceil(this.totalCount / this.itemsPerPage);
    }
}

export default PatientsListStore;