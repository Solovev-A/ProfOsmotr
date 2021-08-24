import { action, makeObservable, observable, runInAction } from "mobx";
import { errorToast, handleResponseWithToasts } from "../utils/toasts";

class BaseListStore {
    constructor({
        initialListLoader = null,
        toastOnNotFound = true,
        notFoundErrorMessage = 'Ничего не найдено',
        minQueryLength = 3,
        getItemsFromResponse = (response) => response,
        ...config
    }) {
        this._config = {
            initialListLoader,
            toastOnNotFound,
            notFoundErrorMessage,
            minQueryLength,
            getItemsFromResponse,
            ...config
        }
        this._initialConfig = Object.assign({}, this._config);
        this.reset();

        makeObservable(this, {
            items: observable,
            searchQuery: observable,
            inProgress: observable,
            inSearch: observable,
            onSearch: action.bound,
            loadInitialList: action.bound,
            _loadSearchResults: action.bound,
            reset: action.bound
        });
    }

    onSearch(query, config = null) {
        this.inSearch = true;
        this.items = [];
        this.searchQuery = query;

        if (config && typeof config === 'object') {
            Object.assign(this._config, config);
        }

        return this._loadSearchResults();
    }

    async loadInitialList() {
        const { initialListLoader, getItemsFromResponse } = this._config;
        if (!initialListLoader) return;

        this.inProgress = true;
        const response = await initialListLoader();

        if (this.inSearch) return;
        if (response.success === false) throw response.message;

        runInAction(() => {
            this.items = getItemsFromResponse(response);
            this.inProgress = false;
        })

        return response;
    }

    async _loadSearchResults() {
        if (this.searchQuery.length < this._config.minQueryLength) {
            errorToast('Слишком короткий запрос');
            return;
        }

        this.inProgress = true;

        const { toastOnNotFound, notFoundErrorMessage, getItemsFromResponse } = this._config;

        const queryHashBefore = this.getQueryHash();
        const response = await this._getSearchResultsResponse();
        const queryHashAfter = this.getQueryHash();

        if (queryHashBefore !== queryHashAfter) return; // не обрабатываем ответ, если начат новый поиск

        runInAction(() => {
            this.inProgress = false;
        });

        if (response && response.success !== false) {
            runInAction(() => {
                this.items = getItemsFromResponse(response);
            });

            if (this.items.length === 0 && toastOnNotFound) errorToast(notFoundErrorMessage);
        }

        return handleResponseWithToasts(response);
    }

    async _getSearchResultsResponse() {
        throw 'Необходимо реализовать в производном классе'
    }

    getQueryHash() {
        return this.searchQuery;
    }

    reset() {
        this.inSearch = false;
        this.inProgress = false;
        this.items = [];
        this.searchQuery = '';
        this._config = Object.assign({}, this._initialConfig);
    }
}

export default BaseListStore;