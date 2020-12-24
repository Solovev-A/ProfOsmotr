// Запускает определенные функции на заданных страницах.

class ScriptManager {

    constructor(namespace) {
        this.namespace = namespace;
    }

    load() {
        const pageId = this._getPageId();

        this._fire('common');
        this._fire(pageId);
    }

    _getPageId() {
        return document.body.dataset.page;
    }

    _fire(funcName) {
        if (funcName !== '' && this.namespace[funcName] && typeof this.namespace[funcName] == 'function') {
            this.namespace[funcName]();
        }
    }
}

export default ScriptManager;