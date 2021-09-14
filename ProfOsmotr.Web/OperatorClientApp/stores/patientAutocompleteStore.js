import BaseListStore from './baseListStore';
import patientApiService from './../services/patientApiService';

/*
config:
    getEmployerId : function 
*/

class PatientAutocompleteStore extends BaseListStore {
    constructor(config) {
        super({
            toastOnNotFound: false
        })

        this.config = config;
    }

    async _getSearchResultsResponse() {
        const employerId = this.config?.getEmployerId ? this.config.getEmployerId() : null;

        let response = await patientApiService.findWithSuggestions(this.searchQuery, employerId);

        if (response && response.success !== false) {
            return [
                ...response.suggestions.map(item => ({ ...item, isSuggested: true })),
                ...response.items
            ]
        }

        return response
    }
}

export default PatientAutocompleteStore;