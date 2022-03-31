import { action, computed, makeObservable, observable, override, runInAction } from "mobx";

import BaseListStore from './baseListStore';

class BasePagedListStore extends BaseListStore {
    itemsPerPage = 20;

    constructor({
        loader,
        initialListLoader = null,
        toastOnNotFound = true,
        notFoundErrorMessage = 'Ничего не найдено',
        minQueryLength = 3
    }) {
        if (!loader) throw 'Требуется загрузчик результата запроса';

        const config = {
            loader,
            initialListLoader,
            toastOnNotFound,
            notFoundErrorMessage,
            minQueryLength,
            getItemsFromResponse: (response) => response.items
        }
        super(config);

        this.reset();
        makeObservable(this, {
            page: observable,
            itemsPerPage: observable,
            totalCount: observable,
            onSearch: override,
            loadPage: action.bound,
            loadInitialList: override,
            _loadSearchResults: override,
            reset: override,
            totalPages: computed
        });
    }

    onSearch(query, config = null) {
        this.page = 1;
        this.totalCount = 0;

        return super.onSearch(query, config);
    }

    async loadPage(page) {
        const pageBefore = this.page;
        this.page = page;
        const response = await this._loadSearchResults();

        if (response.success === false) {
            // указатель текущей страницы остается актуальным для уже загруженной страницы, если не получилось загрузить новую
            this.page = pageBefore;
        }
    }

    async loadInitialList() {
        const response = await super.loadInitialList();

        if (response && response.success !== false) {
            runInAction(() => {
                this.totalCount = response.totalCount;
            })
        }

        return response;
    }

    async _loadSearchResults() {
        const response = await super._loadSearchResults();

        if (response && response.success !== false) {
            runInAction(() => {
                this.totalCount = response.totalCount;
            })
        }

        return response;
    }

    reset() {
        super.reset();

        this.page = 1;
        this.totalCount = 0;
    }

    _getSearchResultsResponse() {
        return this._config.loader(this.searchQuery, this.page, this.itemsPerPage);
    }

    getQueryHash() {
        return this.searchQuery + String(this.page) + String(this.itemsPerPage)
    }

    get totalPages() {
        return Math.ceil(this.totalCount / this.itemsPerPage);
    }
}

export default BasePagedListStore;