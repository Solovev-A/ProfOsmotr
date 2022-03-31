import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import routes from './../../routes';
import PatientEditor from '../../components/patientEditor';
import Spinner from '../../components/spinner';
import { SubmitBtn } from '../../components/forms/general';
import CancellationToken from './../../utils/cancellationToken';
import useErrorHandler from './../../hooks/useErrorHandler';
import useTitle from './../../hooks/useTitle';

const PatientEditorPage = (props) => {
    const { patientEditorStore } = useStore();
    const patientId = props.match.params.id;
    const errorHandler = useErrorHandler();

    const title = `${patientId ? 'Редактирование' : 'Добавление нового'} пациента`;
    useTitle(title);

    useEffect(() => {
        patientEditorStore.setPatientId(patientId);

        const cancellationToken = new CancellationToken();
        patientEditorStore.loadInitialValues(cancellationToken)
            .catch(errorHandler);

        return () => {
            cancellationToken.cancel();
            patientEditorStore.clear();
        }
    }, [patientId])

    const onSubmit = async (event) => {
        event.preventDefault();
        const response = await patientEditorStore.onSubmit();

        if (response && response.success !== false) {
            const newUrl = response.id
                ? routes.patient.getUrl(response.id) // если пациент был создан
                : routes.patient.getUrl(patientId); // если пациент был обновлен
            props.history.push(newUrl);
        }
    }

    const { isValid, isProcessing, isLoading } = patientEditorStore;

    if (isLoading) return <Spinner />

    return (
        <>
            <h2>{title}</h2>
            <form onSubmit={onSubmit}>
                <PatientEditor />
                <SubmitBtn disabled={!isValid} processing={isProcessing}>
                    {patientId ? 'Сохранить' : 'Добавить'}
                </SubmitBtn>
            </form>
        </>
    )
}

export default observer(PatientEditorPage);