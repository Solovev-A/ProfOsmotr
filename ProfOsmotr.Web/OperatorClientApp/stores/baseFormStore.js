import { makeAutoObservable, toJS } from 'mobx';

// Не поддерживает вложенные объекты!

class BaseFormStore {
    constructor(validation = {}) {
        this._validation = validation;
        this._shouldValidateOnChange = false;
        this._editedProperties = new Set();
        this._initialValues = {};
        this.model = {};
        this.errors = {};
        this.isProcessing = false;

        makeAutoObservable(this);
    }

    updateProperty = (name, newValue) => {
        this.model[name] = newValue;
        this._editedProperties.add(name);

        if (this._shouldValidateOnChange) {
            this.validate(name);
        }
    }

    setInitialValues = (values) => {
        this.reset();
        Object.assign(this._initialValues, values);
        Object.assign(this.model, values);
    }

    reset = () => {
        Object.assign(this.model, this._initialValues);
        this._editedProperties = new Set();
        this.errors = {};
        this._shouldValidateOnChange = false;
        this.isProcessing = false;
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