import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import Spinner from '../../components/spinner';
import routes from './../../routes';
import PatientInfo from './components/patientInfo';
import PatientActions from './components/patientActions';
import PatientExamiantions from './components/patientExaminations';
import preliminaryExaminationsApiService from './../../services/preliminaryExaminationsApiService';
import usePageId from './../../hooks/usePageId';

const PatientPage = (props) => {
    const patientId = props.match.params.id;
    const { patientStore } = useStore();

    usePageId({
        slugSetter: patientStore.setPatientId,
        loader: patientStore.loadPatient,
        onReset: patientStore.reset
    });

    const onAddPreliminaryExamination = async () => {
        const response = await preliminaryExaminationsApiService.createEntity({ patientId });
        if (response.success !== false) {
            const newUrl = routes.preliminaryExamination.getUrl(response.id);
            props.history.push(newUrl);
        }
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