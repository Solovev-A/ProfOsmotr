import Util from './common';

/*
 *  Добавление опций в селекты пунктов приказа
 */
async function initOrderItemsSelect() {
    const orderData = await Util.getData('/api/order/getOrder');
    if (orderData) {
        workWithSelect('#OrderItems', orderData);
    }
}

function workWithSelect(selector, orderData) {
    const _formatOption = option => {
        const text = Util.escapeHTML(option.text);
        const html = `<span style="display: block; overflow: hidden; white-space: nowrap;" title="${text}">${text}</span>`;
        return $(html);
    };
    const _formatSelection = option => option.key;


    $(selector).select2({
        data: orderData.map(getSelect2Data),
        multiple: true,
        placeholder: 'Найти по номеру или названию пункта...',
        theme: 'bootstrap4',
        width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
        templateResult: _formatOption,
        templateSelection: _formatSelection,
        language: {
            noResults: () => 'Совпадений не найдено'
        }
    });
}

function getSelect2Data(orderItem) {
    return {
        id: orderItem.id,
        text: `${orderItem.key}. ${orderItem.name}`,
        key: orderItem.key
    }
}

export { initOrderItemsSelect };