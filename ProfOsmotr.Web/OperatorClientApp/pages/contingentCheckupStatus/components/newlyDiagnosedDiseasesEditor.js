import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../../hooks/useStore';
import Spinner from './../../../components/spinner';
import compareICD10Chapters from './../../../utils/compareICD10Chapters';


const NewlyDiagnosedDiseasesEditor = ({ title, store }) => {
    const { icd10Store } = useStore();
    const { updateValue, casesByChapterId } = store;

    useEffect(() => {
        if (!icd10Store.isLoaded) icd10Store.load();
    }, [])

    if (!icd10Store.isLoaded) return <Spinner size=".5rem" />

    const onChange = (event) => {
        const input = event.target;
        const id = Number(input.dataset.chapterId);
        const value = Number(input.value);
        updateValue(id, value);
    }

    return (
        <>
            <p>{title}</p>
            {
                [...casesByChapterId.entries()]
                    .map(([id,]) => icd10Store.getChapterById(id))
                    .sort(compareICD10Chapters)
                    .map((chapter) => {
                        const id = chapter.id;
                        const htmlId = `icd10-chapter-${id}`;

                        return (
                            <div className="row form-group" key={id}>
                                <div className="col" style={{ alignItems: 'center' }} >
                                    <label htmlFor={htmlId}>
                                        {chapter.block}
                                    </label>
                                </div>
                                <div className="col">
                                    <input
                                        type="number"
                                        className="form-control form-control-sm"
                                        value={casesByChapterId.get(id)}
                                        onChange={onChange}
                                        data-chapter-id={id}
                                        id={htmlId}
                                        min="0"
                                    />
                                </div>
                            </div>
                        )
                    })
            }
            <AddNewDiseaseControl editorStore={store} />
        </>
    );
}

const AddNewDiseaseControl = observer(({ editorStore }) => {
    const { icd10Store } = useStore();

    const optionsData = icd10Store.chapters.filter(chapter => {
        return ![...editorStore.casesByChapterId.keys()]
            .includes(chapter.id)
    });

    if (!optionsData.length) return null;

    const emptyValue = 'empty';
    const options = [
        <option value={emptyValue} key={emptyValue}>
            Выберите главу, чтобы добавить
        </option>,
        ...optionsData.map(opt => (
            <option value={opt.id} key={opt.id}>
                {opt.block}
            </option>
        ))
    ]

    const addNewDisease = (event) => {
        const select = event.target;
        if (select.value === emptyValue) return;

        const id = Number(select.value);
        const defaultValue = 1;
        editorStore.updateValue(id, defaultValue);
    }

    return (
        <div className="row form-group">
            <div className="col-6" style={{ alignItems: 'center' }} >
                <select className="form-control"
                    value={emptyValue}
                    onChange={addNewDisease}
                >
                    {options}
                </select>
            </div>
        </div>
    )
})


export default observer(NewlyDiagnosedDiseasesEditor);