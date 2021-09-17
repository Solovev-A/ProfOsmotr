import React from 'react';
import { observer } from 'mobx-react-lite';

import useStore from './../../hooks/useStore';
import ExaminationEditorData from './../../components/examinationEditorData';
import Spinner from '../../components/spinner';
import EmployerInfo from './components/employerInfo';
import PeriodicExaminationActions from './components/periodicExaminationActions';
import EmployerData from './components/employerData';
import ReportData from './components/reportData';
import ContingentList from './components/contingentList';
import ReportDataEditorModal from './components/reportDataEditorModal';
import EmployerDataEditorModal from './components/employerDataEditorModal';
import CreateContingentCheckupStatusModal from './components/createContingentCheckupStatusModal';
import usePageId from './../../hooks/usePageId';


const PeriodicExaminationPage = observer((props) => {
    const { periodicExaminationsStore } = useStore();

    usePageId({
        loader: periodicExaminationsStore.loadExamination,
        slugSetter: periodicExaminationsStore.setExaminationSlug,
        onReset: periodicExaminationsStore.resetExamination
    });

    const { isExaminationLoading, examination } = periodicExaminationsStore;

    if (isExaminationLoading) return <Spinner />


    const onAddCheckupStatusClick = () => {
        periodicExaminationsStore.createContingentCheckupStatusModal.open();
    }

    const onEmployerDataEditClick = () => {
        periodicExaminationsStore.employerDataModal.open();
    }

    const onReportDataEditClick = () => {
        periodicExaminationsStore.reportDataModal.open();
    }

    return (
        <>
            <h2>Периодический осмотр за {examination.examinationYear} год</h2>
            <EmployerInfo employer={examination.employer} />
            <ExaminationEditorData editor={examination.lastEditor} />
            <PeriodicExaminationActions />
            <ReportData examination={examination} onEditClick={onReportDataEditClick} />
            <EmployerData examination={examination} onEditClick={onEmployerDataEditClick} />
            <ContingentList examination={examination} onAddClick={onAddCheckupStatusClick} />
            <ReportDataEditorModal />
            <EmployerDataEditorModal />
            <CreateContingentCheckupStatusModal />
        </>
    )
})

export default PeriodicExaminationPage;