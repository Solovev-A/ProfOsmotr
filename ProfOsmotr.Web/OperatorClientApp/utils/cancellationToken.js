export default class CancellationToken {
    constructor() {
        this.isCancelled = false;
    }

    cancel = () => {
        this.isCancelled = true;
    }
}