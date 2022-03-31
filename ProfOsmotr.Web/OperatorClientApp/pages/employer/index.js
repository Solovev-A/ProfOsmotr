import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import Spinner from '../../components/spinner';
import routes from './../../routes';
import EmployerActions from './components/employerActions';
import EmployerInfo from './components/employerInfo';
import EmployerExamiantions from './components/employerExaminations';
import usePageId from './../../hooks/usePageId';
import useTitle from './../../hooks/useTitle';

const EmployerPage = (props) => {
    const employerId = props.match.params.id;
    const { employersStore } = useStore();

    usePageId({
        slugSetter: employersStore.setEmployerSlug,
        loader: employersStore.loadEmployer,
        onReset: employersStore.resetEmployer
    });

    const onAddPeriodicExamination = async (year) => {
        const response = await employersStore.onAddPeriodicExamination(year);
        if (response.success !== false) {
            const newUrl = routes.periodicExamination.getUrl(response.id);
            props.history.push(newUrl);
        }
    }

    const { isEmployerLoading, employer } = employersStore;

    const title = isEmployerLoading
        ? 'Организация - Загрузка'
        : `${employer.name} - Организация`;
    useTitle(title);

    if (isEmployerLoading) return <Spinner />

    return (
        <>
            <h2>Карта организации ID {employerId}</h2>
            <EmployerInfo employer={employer} />
            <EmployerActions employerId={employerId}
                onAddPeriodicExamination={onAddPeriodicExamination}
            />
            <EmployerExamiantions />
        </>
    )
}

export default observer(EmployerPage);