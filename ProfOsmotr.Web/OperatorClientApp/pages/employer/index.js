import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import CancellationToken from './../../utils/cancellationToken';
import Spinner from '../../components/spinner';
import routes from './../../routes';
import useErrorHandler from './../../hooks/useErrorHandler';
import EmployerActions from './components/employerActions';
import EmployerInfo from './components/employerInfo';
import EmployerExamiantions from './components/employerExaminations';

const EmployerPage = (props) => {
    const employerId = props.match.params.id;
    const { employersStore } = useStore();
    const errorHandler = useErrorHandler();

    useEffect(() => {
        employersStore.setEmployerSlug(employerId);

        const cancellationToken = new CancellationToken();
        employersStore.loadEmployer(cancellationToken)
            .catch(errorHandler);

        return () => {
            cancellationToken.cancel();
            employersStore.resetEmployer();
        }
    }, [employerId]);

    const onAddPeriodicExamination = () => {
        // ... вызов api для создания медосмотра
        const id = 0;
        const newUrl = routes.periodicExaminations.getUrl(id);
        props.history.push(newUrl);
    }

    const { isEmployerLoading, employer } = employersStore;

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