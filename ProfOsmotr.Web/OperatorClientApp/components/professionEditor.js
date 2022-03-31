import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../hooks/useStore';
import InputField from './forms/observers/inputField';
import OrderItemMultiselect from './orderItemMultiselect';

const orderItemsPropName = 'orderItems';

const ProfessionEditor = observer((props) => {
    const { professionEditorStore } = useStore();

    return (
        <>
            <InputField
                formStore={professionEditorStore}
                label="Название"
                name="name"
                maxLength="70"
            />
            <div className="form-group">
                <label>Вредные факторы и (или) виды работ</label>
                <OrderItemMultiselect
                    value={professionEditorStore.model[orderItemsPropName]}
                    onChange={(newValue) => professionEditorStore.updateProperty(orderItemsPropName, newValue)}
                />
            </div>
        </>
    )
})

export default ProfessionEditor;