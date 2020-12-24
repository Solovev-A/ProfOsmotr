import Util from './common';

/*
 *  Добавление опций в селекты пунктов приказа
 */
async function initOrderItemsSelect() {
    const itemsData = await Util.getData('/api/order/getItemsList');
    if (itemsData) {
        workWithSelect('#OrderItems1', itemsData.annex1);
        workWithSelect('#OrderItems2', itemsData.annex2);
    }
}

function workWithSelect(selector, sourceDataArray) {
    $(selector).select2({
        data: sourceDataArray.map(getSelect2Data),
        multiple: true,
        placeholder: 'Найти по номеру пункта...',
        theme: 'bootstrap4',
        width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style'
    });
}

function getSelect2Data(value) {
    let name = value.name == null ? '' : value.name;
    return {
        id: value.id,
        text: Util.escapeHTML(value.key + ' ' + name)
    }
}

export { initOrderItemsSelect };