import { action, computed, makeObservable, observable, runInAction } from "mobx";

import api from "../services/api";

class OrderStore {
    orderItems = [];
    orderExaminations = [];

    constructor() {
        makeObservable(this, {
            orderItems: observable,
            orderExaminations: observable,
            loadOrder: action,
            isLoaded: computed
        })
    }

    getOrder = async () => {
        if (!this.isLoaded) {
            await this.loadOrder();
        }

        return {
            orderItems: this.orderItems,
            orderExaminations: this.orderExaminations
        };
    }

    loadOrder = async () => {
        const orderItems = await api.get('/order/getOrder');
        if (orderItems.success === false) throw 'Ошибка при загрузке данных приказа';

        const orderExaminations = await api.get('/order/getExaminations');
        if (orderExaminations.success === false) throw 'Ошибка при загрузке данных обследований по приказу';

        runInAction(() => {
            this.orderItems = orderItems;
            this.orderExaminations = orderExaminations;
        })
    }

    getOrderItemByKey = (key) => {
        return this.orderItems.find(oi => oi.key === key);
    }

    get isLoaded() {
        return !!(this.orderItems.length && this.orderExaminations.orderExaminations?.length);
    }
}

export default OrderStore;