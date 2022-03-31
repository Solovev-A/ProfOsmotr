import React from 'react';

import routes from './../../../routes'
import Card from './../../../components/card';
import ItemsList from './../../../components/itemsList';

const columns = [{
    title: 'Место работы',
    width: '60%',
    render: (item) => {
        return (
            <>
                {item.employer ?? 'Место работы не указано'}<br />
                {item.profession}{item.profession ? ', ' : ''}{item.orderItems.join('; ')}
            </>
        )
    }
}, {
    title: 'Результат',
    width: "20%",
    render: (item) => item.result ?? 'Не завершен'
}, {
    title: 'Дата завершения',
    render: (item) => item.dateOfCompletion ?? '-'
}
]

const PatientExamiantions = ({ hasExaminations, preliminaryExaminations, contingentCheckupStatuses }) => {
    if (!hasExaminations) {
        return (
            <div className="text-center font-italic">
                Нет зарегистрированных медосмотров
            </div>
        )
    }

    return (
        <>
            <Card title="Предварительные медицинские осмотры">
                <ItemsList items={preliminaryExaminations}
                    getItemUrl={(item) => routes.preliminaryExamination.getUrl(item.id)}
                    columns={columns}
                />
            </Card>
            <Card title="Периодические медицинские осмотры">
                <ItemsList items={contingentCheckupStatuses}
                    getItemUrl={(item) => routes.contingentCheckupStatus.getUrl(item.id)}
                    columns={columns}
                />
            </Card>
        </>
    )
}

export default PatientExamiantions;