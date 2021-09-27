import React from 'react';
import { AddBtn } from '../../../components/buttons';
import ItemsList from '../../../components/itemsList';
import { getFullName } from '../../../utils/personNames';
import Card from './../../../components/card';
import routes from './../../../routes';

const contingentListColumns = [{
    title: 'Работник',
    width: '30%',
    render: (item) => {
        const { patient } = item;
        return (
            <>
                {getFullName(patient)}<br />
                {patient.dateOfBirth}
            </>
        )
    }
}, {
    title: 'Профессия',
    width: '30%',
    render: (item) => item.profession
        ? <>{item.profession}{': '}{item.orderItems.join(', ')}</>
        : null
}, {
    title: 'Заключение',
    width: '25%',
    render: (item) => item.result?.text
}, {
    title: 'Дата',
    render: (item) => item.dateOfCompletion
}];


const ContingentList = ({ examination, onAddClick }) => {
    const { checkupStatuses } = examination;

    return (
        <Card title={<TitleView onAddClick={onAddClick} />}>
            <ItemsList
                columns={contingentListColumns}
                items={checkupStatuses}
                getItemUrl={(item) => routes.contingentCheckupStatus.getUrl(item.id)}
            />
        </Card>
    );
}

const TitleView = ({ onAddClick }) => {
    return (
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div>Список работников</div>
            <AddBtn className='btn btn-sm btn-secondary' onClick={onAddClick} />
        </div>
    )
}

export default ContingentList;