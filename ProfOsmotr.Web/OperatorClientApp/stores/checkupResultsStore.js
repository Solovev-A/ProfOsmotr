import { makeAutoObservable } from "mobx";

class CheckupResultsStore {
    checkupResults = [];

    constructor() {
        makeAutoObservable(this);
    }

    loadCheckupResults = async () => {
        // здесь может быть вызов api для загрузки списка возможных результатов

        this.checkupResults = [
            {
                id: 'NoContraindications',
                text: 'Противопоказания к работе не выявлены'
            },
            {
                id: 'PermanentContraindications',
                text: 'Выявлены постоянные противопоказания к работе'
            },
            {
                id: 'TemporaryContraindications',
                text: 'Выявлены временные противопоказания к работе'
            },
            {
                id: 'NeedAdditionalMedicalExamination',
                text: 'Нуждается в проведении дополнительного обследования (заключение не дано)'
            }
        ]
    }
}

export default CheckupResultsStore;