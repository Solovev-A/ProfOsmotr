import { makeObservable, observable, action, computed, toJS, runInAction } from 'mobx';

// Не поддерживает вложенные объекты!

class BaseFormStore {
    constructor(template, validation = {}) {
        this._template = template;
        this._validation = validation;
        this._initialValues = {};
        this.model = {}
        this.clear();

        makeObservable(this, {
            model: observable,
            errors: observable,
            isLoading: observable,
            isProcessing: observable,
            updateProperty: action,
            clear: action,
            setInitialValues: action,
            validate: action,
            validateAll: action,
            onSendingData: action,
            patchedData: computed,
            data: computed,
            isValid: computed
        });
    }

    updateProperty = (name, newValue) => {
        this.model[name] = newValue;
        this._editedProperties.add(name);

        if (this._shouldValidateOnChange) {
            this.validate(name);
        }
    }

    setInitialValues = (values) => {
        this._editedProperties = new Set();
        this.errors = {};
        this._shouldValidateOnChange = false;
        this.isLoading = false;
        this.isProcessing = false;
        Object.assign(this._initialValues, values);
        Object.assign(this.model, values);
    }

    clear = () => {
        this.setInitialValues(this._template);
    }

    validate = (propName) => {
        const propCheck = this._validation[propName];

        if (!propCheck) return;

        this.errors[propName] = propCheck.isValid(this.model[propName])
            ? undefined
            : propCheck.errorMessage;
    }

    validateAll = () => {
        const propsToValidate = Object.keys(this._validation);

        propsToValidate.forEach(propName => {
            this.validate(propName);
        })

        this._shouldValidateOnChange = true;
    }

    onSendingData = (handler, shouldValidate = true) => {
        if (shouldValidate) {
            this.validateAll();
            if (!this.isValid) return;
        }

        this.isProcessing = true;

        const result = handler()
            .then(response => {
                // обработка прекращается, если ответ оказался неудачным, 
                // либо, в случае успеха - при очистке, 
                // которую следует производить во время анмаунта формы (при переходе на страницу результата)
                if (response.success === false) {
                    runInAction(() => this.isProcessing = false);
                }
                return response;
            });
        return result;
    }

    get patchedData() {
        const editedPropsArr = [...this._editedProperties];

        return editedPropsArr.reduce((result, propName) => {
            const initialValue = this._initialValues[propName];
            const currentValue = this.model[propName];
            if (Array.isArray(currentValue)) {
                // если массив содержит объекты с одинаковыми id, в любом порядке, 
                // считаем, что он не изменился
                if (!isArraysOfObjectsWithIdEquals(currentValue, initialValue)) {
                    result[propName] = toJS(currentValue);
                }
            }
            else if (currentValue !== initialValue) {
                result[propName] = currentValue;
            }
            return result;
        }, {});
    }

    get data() {
        return toJS(this.model);
    }

    get isValid() {
        return Object.entries(this.errors).every(([key, value]) => !value && value !== '');
    }
}

function isArraysOfObjectsWithIdEquals(array1, array2) {
    if (array1.length !== array2.length) {
        return false;
    }

    const array1IdentifiersSorted = array1.map(obj => obj.id).sort();
    const array2IdentifiersSorted = array2.map(obj => obj.id).sort();

    return array1IdentifiersSorted.every((value, index) => value === array2IdentifiersSorted[index]);
}

export default BaseFormStore;