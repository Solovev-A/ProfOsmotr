const Util = {
    customParseFloat: function (str) {
        return parseFloat(str.replace(',', '.').replace(/\s/g, ''));
    },

    toCurrencyString: function (number) {
        return number.toLocaleString(undefined,
            {
                'minimumFractionDigits': 2,
                'maximumFractionDigits': 2,
                'useGrouping': false
            });
    },

    escapeHTML: function (str) {
        if (typeof (str) !== 'string')
            return null;
        return str
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    },

    isEmpty: function (obj) {
        for (let i in obj)
            return false;
        return true;
    },

    toElement: function (html) {
        let div = document.createElement('div');
        div.innerHTML = html;
        return div.firstElementChild;
    },



    getData: async function (url) {
        var response = await fetch(url, {
            credentials: 'same-origin'
        });
        if (response.ok) {
            return await response.json();
        }
        await processBadRequestResult(response);
        return undefined;
    },

    postData: async function (url, dataObject) {
        var response = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            credentials: 'same-origin',
            body: JSON.stringify(dataObject)
        });
        if (response.ok) {
            return await response.json();
        }
        await processBadRequestResult(response);
        return undefined;
    },

    removeAllChildren: function (node) {
        while (node.firstChild) {
            node.removeChild(node.lastChild);
        }
    }
};

async function processBadRequestResult(response) {
    if (response.status === 400) {
        let errorResponse = await response.json();
        if (errorResponse.success === false) {
            let message = 'Во время обработки запроса возникла ошибка. Обратитесь к администратору.\n';
            for (let error of errorResponse.errors) {
                message += error + '\n'
            }
            alert(message);
        }
    }
    else {
        alert('Во время обработки запроса произошла ошибка. Код ошибки: ' + response.status);
    }
}

export default Util;