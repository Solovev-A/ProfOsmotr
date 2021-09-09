import { makeObservable, override } from 'mobx';

import BaseExaminationsStore from './baseExaminationsStore';
import periodicExaminationsApiService from './../services/periodicExaminationsApiService';



class PeriodicExaminationsStore extends BaseExaminationsStore {
    constructor() {
        super(periodicExaminationsApiService);

        makeObservable(this, {
            resetExamination: override
        })
    }

    resetExamination() {
        super.resetExamination();
    }
}

export default PeriodicExaminationsStore;