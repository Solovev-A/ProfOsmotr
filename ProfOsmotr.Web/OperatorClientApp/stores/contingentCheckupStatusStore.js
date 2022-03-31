import { makeAutoObservable, runInAction } from "mobx";

import periodicExaminationsApiService from "../services/periodicExaminationsApiService";
import { handleResponseWithToasts } from "../utils/toasts";
import ModalStore from './modalStore';

class ContingentCheckupStatusStore {
    constructor() {
        this.reset();
        makeAutoObservable(this);
    }

    setCheckupStatusSlug = (newSlug) => {
        this.checkupStatusSlug = newSlug;
    }

    loadCheckupStatus = async (cancellationToken) => {
        if (!this.checkupStatusSlug) return;

        this.isLoading = true;
        const response = await periodicExaminationsApiService.getCheckupStatus(this.checkupStatusSlug);
        if (!cancellationToken.isCancelled) {
            if (response.success === false) throw response.message
            runInAction(() => {
                this.checkupStatus = response;
                this.isLoading = false;
            });
        }
    }

    removeCheckupStatus = async () => {
        const confirmation = confirm('Вы действительно хотите удалить медосмотр?');
        if (!confirmation) return null;

        const response = await periodicExaminationsApiService.removeCheckupStatus(this.checkupStatusSlug);
        return handleResponseWithToasts(response, true);
    }

    reset = () => {
        this.isLoading = true;
        this.checkupStatusSlug = null;
        this.checkupStatus = null;
        this.workPlaceModal = new ModalStore();
        this.checkupIndexValuesModal = new ModalStore();
        this.medicalReportModal = new ModalStore();
    }
}

export default ContingentCheckupStatusStore;