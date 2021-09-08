import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Multiselect } from 'react-dropdown-components';

import useStore from '../hooks/useStore';
import Spinner from './spinner';

const OrderItemMultiselect = observer(({ value, onChange }) => {
    const { orderStore } = useStore();
    const { orderItems } = orderStore;

    if (!orderItems.length) return <Spinner size="1rem" />

    return (
        <Multiselect
            options={orderItems}
            value={value}
            onChange={onChange}
            renderOptionText={(orderItem) => `${orderItem.key} ${orderItem.name}`}
            renderValueText={ValueTextView}
        />
    );
})

const ValueTextView = (orderItem) => {
    return (
        <span title={orderItem.name}>
            {orderItem.key}
        </span>
    )
}

export default OrderItemMultiselect;