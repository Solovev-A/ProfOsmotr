import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import useErrorHandler from './../../hooks/useErrorHandler';
import CancellationToken from './../../utils/cancellationToken';
import ExaminationEditorData from './../../components/examinationEditorData';
import Spinner from '../../components/spinner';
import EmployerInfo from './components/employerInfo';
import PeriodicExaminationActions from './components/periodicExaminationActions';
import EmployerData from './components/employerData';
import ReportData from './components/reportData';
import ContingentList from './components/contingentList';


const PeriodicExaminationPage = observer((props) => {
    const examinationId = props.match.params.id;
    const { periodicExaminationsStore } = useStore();
    const errorHandler = useErrorHandler();

    useEffect(() => {
        periodicExaminationsStore.setExaminationSlug(examinationId);

        const cancellationToken = new CancellationToken();
        periodicExaminationsStore.loadExamination(cancellationToken)
            .catch(errorHandler);

        return () => {
            cancellationToken.cancel();
            periodicExaminationsStore.resetExamination();
        }
    }, [examinationId]);

    const { isExaminationLoading, examination } = periodicExaminationsStore;

    if (isExaminationLoading) return <Spinner />


    const onAddCheckupStatusClick = () => {

    }

    const onEmployerDataEditClick = () => {

    }

    const onReportDataEditClick = () => {

    }

    return (
        <>
            <h2>Карта периодического осмотра за {examination.examinationYear} год</h2>
            <EmployerInfo employer={examination.employer} />
            <ExaminationEditorData editor={examination.lastEditor} />
            <PeriodicExaminationActions />
            <ReportData examination={examination} onEditClick={onReportDataEditClick} />
            <EmployerData examination={examination} onEditClick={onEmployerDataEditClick} />
            <ContingentList examination={examination} onAddClick={onAddCheckupStatusClick} />
        </>
    )
})

export default PeriodicExaminationPage;