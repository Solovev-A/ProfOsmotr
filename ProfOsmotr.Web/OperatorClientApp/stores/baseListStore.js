import { action, computed, makeObservable, observable, runInAction } from "mobx";
import { errorToast, handleResponseWithToasts } from "../utils/toasts";

class BaseListStore {
    itemsPerPage = 20;

    constructor({
        loader,
        initialListLoader = null,
        notFoundErrorMessage = 'Ничего не найдено',
        minQueryLength = 3
    }) {
        if (!loader) throw 'Требуется загрузчик результата запроса';

        this._config = { loader, initialListLoader, notFoundErrorMessage, minQueryLength }
        this.reset();

        makeObservable(this, {
            items: observable,
            page: observable,
            itemsPerPage: observable,
            totalCount: observable,
            searchQuery: observable,
            inProgress: observable,
            inSearch: observable,
            onSearch: action,
            loadPage: action,
            loadInitialList: action,
            _loadSearchResults: action,
            reset: action,
            _queryHashCode: computed,
            totalPages: computed
        });
    }

    onSearch = (query) => {
        this.inSearch = true;

        // сброс состояния результата
        this.items = [];
        this.page = 1;
        this.totalCount = 0;
        this.searchQuery = query;

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

    loadInitialList = async () => {
        const { initialListLoader } = this._config;
        if (!initialListLoader) return;

        this.inProgress = true;
        const response = await initialListLoader();

        if (this.inSearch) return;
        if (response.success === false) throw response.message;

        runInAction(() => {
            this.items = response.items;
            this.totalCount = response.totalCount;
            this.inProgress = false;
        })
    }

    _loadSearchResults = async () => {
        if (this.searchQuery.length < this._config.minQueryLength) {
            errorToast('Слишком короткий запрос');
            return;
        }

        this.inProgress = true;

        const { loader, notFoundErrorMessage } = this._config;

        const queryHashBefore = this._queryHashCode;
        const response = await loader(this.searchQuery, this.page, this.itemsPerPage);
        const queryHashAfter = this._queryHashCode;

        if (queryHashBefore !== queryHashAfter) return; // не обрабатываем ответ, если начат новый поиск

        runInAction(() => {
            this.totalCount = response.totalCount;
            this.items = response.items;
            this.inProgress = false;
        })

        if (this.totalCount === 0) errorToast(notFoundErrorMessage);

        return handleResponseWithToasts(response);
    }

    reset = () => {
        this.inSearch = false;
        this.inProgress = false;
        this.items = [];
        this.page = 1;
        this.searchQuery = '';
        this.totalCount = 0;
    }

    get _queryHashCode() {
        return this.searchQuery + String(this.page) + String(this.itemsPerPage);
    }

    get totalPages() {
        return Math.ceil(this.totalCount / this.itemsPerPage);
    }
}

export default BaseListStore;