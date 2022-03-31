import { makeObservable, action, observable, runInAction } from "mobx";

import { requiredString } from "../utils/validation";
import BaseFormStore from "./baseFormStore";
import professionApiService from "../services/professionApiService";
import { errorToast, handleResponseWithToasts } from "../utils/toasts";

const professionTemplate = {
    name: '',
    orderItems: []
}

const validation = {
    name: {
        isValid: requiredString,
        errorMessage: "Укажите название"
    }
}

class ProfessionEditorStore extends BaseFormStore {
    profession = null;

    constructor(rootStore) {
        super(professionTemplate, validation);

        this.rootStore = rootStore;
        makeObservable(this, {
            profession: observable,
            setProfession: action,
            loadInitialValues: action,
            onSubmit: action
        })
    }

    setProfession = (profession) => {
        this.profession = profession;
    }

    loadInitialValues = async () => {
        const orderStore = this.rootStore.orderStore;
        if (!orderStore.isLoaded) {
            await orderStore.loadOrder();
        }

        let initialValues = this.profession;
        if (initialValues) {
            const orderItems = initialValues.orderItems
                .map((key) => orderStore.getOrderItemByKey(key));

            initialValues = {
                ...initialValues,
                orderItems
            }
        }
        this.setInitialValues(initialValues);
        runInAction(() => {
            this.isLoading = false;
        });
    }

    onSubmit = async () => {
        const requestData = {
            ...this.data,
            orderItems: this.data.orderItems.map(oi => oi.id)
        }

        if (!Object.keys(this.patchedData).length) {
            errorToast('Ничего не изменилось, клонирование невозможно')
            return;
        }

        const handler = () => professionApiService.create(requestData);
        const response = await this.onSendingData(handler);

        if (!response) return;

        return handleResponseWithToasts(response, true);
    }
}

export default ProfessionEditorStore;