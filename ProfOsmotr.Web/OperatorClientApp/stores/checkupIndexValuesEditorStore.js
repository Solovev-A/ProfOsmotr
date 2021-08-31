import { observable, runInAction, makeAutoObservable } from 'mobx';
import { handleResponseWithToasts } from '../utils/toasts';

class CheckupIndexValuesEditorStore {
    constructor(rootStore, checkupEditorStore) {
        this.rootStore = rootStore;
        this.checkupEditorStore = checkupEditorStore;
        this.checkupExaminationResultIndexes = [];
        this.suggestions = [];
        this.isLoading = true;
        this.valuesByIndexId = observable.map();
        this.suggestionValuesByIndexId = observable.map();
        this.initialValuesByIndexId = observable.map();

        makeAutoObservable(this);
    }

    loadInitialValues = async () => {
        this.isLoading = true;

        const checkup = await this.checkupEditorStore.loadExamination();
        runInAction(() => {
            this.checkupExaminationResultIndexes = checkup.checkupExaminationResultIndexes;
        });

        const suggestions = await this._getSuggestions(checkup);
        runInAction(() => {
            this.suggestions = suggestions;
        });

        setInitialIndexValues(this.checkupExaminationResultIndexes, this.initialValuesByIndexId);
        setInitialIndexValues(this.checkupExaminationResultIndexes, this.valuesByIndexId);
        setInitialIndexValues(this.suggestions, this.suggestionValuesByIndexId);

        runInAction(() => {
            this.isLoading = false;
        });

        function setInitialIndexValues(examinationResultIndexesArray, targetMap) {
            examinationResultIndexesArray.forEach(examinationResultIndex => {
                examinationResultIndex.checkupIndexValues.forEach(checkupIndexValue => {
                    targetMap.set(checkupIndexValue.index.id, checkupIndexValue.value ?? '');
                })
            })
        }
    }

    onSubmit = async () => {
        let indexValuesMapEntries = this.valuesByIndexId
            .entries();
        indexValuesMapEntries = [...indexValuesMapEntries]
            .filter(([id, value]) => value !== this.initialValuesByIndexId.get(id));

        let suggestionValuesMapEntries = this.suggestionValuesByIndexId
            .entries();
        suggestionValuesMapEntries = [...suggestionValuesMapEntries]
            .filter(([, value]) => value !== '');

        const data = [
            ...indexValuesMapEntries,
            ...suggestionValuesMapEntries
        ]
            .map(([id, value]) => ({ id, value }));

        const response = data.length > 0
            ? await this.checkupEditorStore.onUpdate({ checkupIndexValues: data })
            : Promise.resolve(true);
        return handleResponseWithToasts(response, true);
    }

    clear = () => {
        this.checkupExaminationResultIndexes = [];
        this.suggestions = [];
        this.valuesByIndexId.clear();
        this.suggestionValuesByIndexId.clear();
        this.isLoading = true;
    }

    updateIndexValue = (id, value) => {
        this.valuesByIndexId.set(id, value);
    }

    updateSuggestionIndexValue = (id, value) => {
        this.suggestionValuesByIndexId.set(id, value);
    }

    _getSuggestions = async (checkup) => {
        const orderItemsIdentifiers = checkup.workPlace?.profession?.orderItems.map(oi => oi.id) ?? [];
        const order = await this.rootStore.orderStore.getOrder();
        const mandatoryExaminationsSuggestions = getMandatoryExaminationsSuggestions();

        // id обследований по приказу, которые необходимы для медосмотра, без повторений
        let examinationsIdentifiers = orderItemsIdentifiers.reduce((set, currentId) => {
            const orderItem = order.orderItems.find(oi => oi.id === currentId);
            orderItem.orderExaminations.forEach(id => set.add(id));
            return set;
        }, new Set());
        examinationsIdentifiers = [...examinationsIdentifiers];

        // получаем предлагаемые показатели
        let suggestions = examinationsIdentifiers.map(id => {
            const examination = order.orderExaminations.orderExaminations.find(oe => oe.id === id);
            return mapToSuggestion(examination);
        });

        // формируем сет Id содержащихся в модели показетелей
        const modelCheckupIndexesIdSet = checkup.checkupExaminationResultIndexes.reduce((set, current) => {
            current.checkupIndexValues.forEach(checkupIndexValue => set.add(checkupIndexValue.index.id));
            return set;
        }, new Set());

        suggestions = suggestions
            // добавляем показатели обязательных обследований
            .concat(mandatoryExaminationsSuggestions)
            // убираем из предложений показатели, которые уже содержатся в модели
            .map(suggestion => {
                return {
                    ...suggestion,
                    checkupIndexValues: suggestion.checkupIndexValues
                        .filter(checkupIndexValue => !modelCheckupIndexesIdSet.has(checkupIndexValue.index.id))
                }
            })
            // убираем из предложений обследования, у которых после предыдущей фильтрации не осталось показателей
            .filter(suggestion => suggestion.checkupIndexValues.length > 0);

        return suggestions.sort(compareSuggestions);


        function getMandatoryExaminationsSuggestions() {
            const result = order.orderExaminations.orderExaminations
                .filter(examination => examination.isMandatory)
                .map(examination => mapToSuggestion(examination));

            return result.sort(compareSuggestions);
        }

        function mapToSuggestion(examination) {
            return {
                examinationName: examination.name,
                checkupIndexValues: examination.examinationResultIndexes.map(resultIndex => ({ index: resultIndex, value: '' }))
            }
        }

        function compareSuggestions(a, b) {
            if (a.examinationName < b.examinationName) return -1;
            if (a.examinationName > b.examinationName) return 1;
            return 0;
        }
    }
}

export default CheckupIndexValuesEditorStore;