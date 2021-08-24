import React, { useEffect } from 'react';

import routes from './../../../routes'
import Card from './../../../components/card';
import ItemsList from './../../../components/itemsList';
import Pagination from './../../../components/pagination';
import useStore from './../../../hooks/useStore';
import { observer } from 'mobx-react-lite';
import Spinner from './../../../components/spinner';

const preliminaryExaminationsColumns = [{
    title: 'ФИО работника',
    width: '60%',
    render: (item) => item.patient
}, {
    title: 'Завершен',
    width: "20%",
    render: (item) => item.isCompleted ? 'Да' : 'Нет'
}, {
    title: 'Дата заключения',
    render: (item) => item.reportDate ?? '-'
}
]

const EmployerExamiantions = () => {
    const { preliminaryExaminationsStore, employersStore } = useStore();

    useEffect(() => {
        preliminaryExaminationsStore.loadEmployerExaminations(employersStore.employerSlug);

        return () => {
            preliminaryExaminationsStore.reset();
        }
    }, [])

    if (!preliminaryExaminationsStore.items && !employersStore.employer.periodicMedicalExaminations) {
        return (
            <div className="text-center font-italic">
                Нет зарегистрированных медосмотров
            </div>
        )
    }

    return (
        <>
            <Card title="Предварительные медицинские осмотры">
                {preliminaryExaminationsStore.inProgress && !preliminaryExaminationsStore.items
                    ? <Spinner />
                    : <ItemsList items={preliminaryExaminationsStore.items}
                        getItemUrl={(item) => routes.preliminaryExamination.getUrl(item.id)}
                        columns={preliminaryExaminationsColumns}
                    />
                }
                {preliminaryExaminationsStore.totalPages > 1
                    ? <Pagination totalPages={preliminaryExaminationsStore.totalPages}
                        currentPage={preliminaryExaminationsStore.page}
                        onPageChange={(page) => preliminaryExaminationsStore.loadEmployerExaminations(employersStore.employerSlug, page)}
                    />
                    : null
                }
            </Card>
        </>
    )
}

export default observer(EmployerExamiantions);