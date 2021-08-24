import BaseListStore from './baseListStore';
import professionApiService from '../services/professionApiService';

class ProfessionAutocompleteStore extends BaseListStore {
    constructor(workPlaceEditorStore) {
        super({
            toastOnNotFound: false
        })

        this._workplaceEditorStore = workPlaceEditorStore;
    }

    async _getSearchResultsResponse() {
        let response = await professionApiService.find(this.searchQuery, this._workplaceEditorStore.employer?.id);

        if (response && response.success !== false) {
            return [
                ...response.suggestions.map(item => ({ ...item, isSuggested: true })),
                ...response.items
            ]
        }

        return response
    }
}

export default ProfessionAutocompleteStore;