import api from "../services/api";

class OrderStore {
    orderItems = [];
    orderExaminations = [];

    getOrder = async () => {
        if (!this.orderItems.length || !this.orderExaminations.orderExaminations.length) {
            const orderItems = await api.get('/order/getOrder');
            if (orderItems.success === false) throw 'Ошибка при загрузке данных приказа';

            const orderExaminations = await api.get('/order/getExaminations');
            if (orderExaminations.success === false) throw 'Ошибка при загрузке данных обследований по приказу';

            this.orderItems = orderItems;
            this.orderExaminations = orderExaminations;
        }

        return {
            orderItems: this.orderItems,
            orderExaminations: this.orderExaminations
        };
    }
}

export default OrderStore;