import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import CancellationToken from './../../utils/cancellationToken';
import Spinner from '../../components/spinner';
import routes from './../../routes';
import PatientInfo from './components/patientInfo';
import PatientActions from './components/patientActions';
import PatientExamiantions from './components/patientExaminations';
import useErrorHandler from './../../hooks/useErrorHandler';

const PatientPage = (props) => {
    const patientId = props.match.params.id;
    const { patientStore } = useStore();
    const errorHandler = useErrorHandler();

    useEffect(() => {
        patientStore.setPatientId(patientId);

        const cancellationToken = new CancellationToken();
        patientStore.loadPatient(cancellationToken)
            .catch(errorHandler);

        return () => {
            cancellationToken.cancel();
            patientStore.reset();
        }
    }, [patientId]);

    const onAddPreliminaryExamination = () => {
        // ... вызов api для создания медосмотра
        const id = 0;
        const newUrl = routes.preliminaryExaminations.getUrl(id);
        props.history.push(newUrl);
    }

    const { isLoading,
        fullName,
        gender,
        dateOfBirth,
        address,
        hasExaminations,
        preliminaryMedicalExaminations,
        contingentCheckupStatuses
    } = patientStore;

    if (isLoading) return <Spinner />

    return (
        <>
            <h2>Карта пациента ID {patientId}</h2>
            <PatientInfo fullName={fullName}
                gender={gender}
                dateOfBirth={dateOfBirth}
                address={address}
            />
            <PatientActions patientId={patientId}
                onAddPreliminaryExamination={onAddPreliminaryExamination}
            />
            <PatientExamiantions hasExaminations={hasExaminations}
                contingentCheckupStatuses={contingentCheckupStatuses}
                preliminaryExaminations={preliminaryMedicalExaminations}
            />
        </>
    )
}

export default observer(PatientPage);