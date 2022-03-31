import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from '../../hooks/useStore';
import CancellationToken from './../../utils/cancellationToken';
import useErrorHandler from './../../hooks/useErrorHandler';
import EmployerEditor from '../../components/employerEditor';
import SubmitBtn from './../../components/forms/general/submitBtn';
import Spinner from './../../components/spinner';
import routes from './../../routes';
import useTitle from './../../hooks/useTitle';

const EmployerEditorPage = (props) => {
    const { employerEditorStore } = useStore();
    const employerId = props.match.params.id;
    const errorHandler = useErrorHandler();

    const title = `${employerId ? 'Редактирование' : 'Добавление новой'} организации`;
    useTitle(title);

    useEffect(() => {
        employerEditorStore.setEmployerId(employerId);

        const cancellationToken = new CancellationToken();
        employerEditorStore.loadInitialValues(cancellationToken)
            .catch(errorHandler);

        return () => {
            cancellationToken.cancel();
            employerEditorStore.clear();
        }
    }, [employerId])

    const onSubmit = async (event) => {
        event.preventDefault();
        const response = await employerEditorStore.onSubmit();

        if (response && response.success !== false) {
            const newUrl = response.id
                ? routes.employer.getUrl(response.id)
                : routes.employer.getUrl(employerId);
            props.history.push(newUrl);
        }
    }

    const { isValid, isProcessing, isLoading } = employerEditorStore;

    if (isLoading) return <Spinner />

    return (
        <>
            <h2>{title}</h2>
            <form onSubmit={onSubmit}>
                <EmployerEditor />
                <SubmitBtn disabled={!isValid} processing={isProcessing}>
                    {employerId ? 'Сохранить' : 'Добавить'}
                </SubmitBtn>
            </form>
        </>
    )
}

export default observer(EmployerEditorPage);