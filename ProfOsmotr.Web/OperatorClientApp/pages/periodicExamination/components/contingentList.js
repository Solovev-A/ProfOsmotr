import React from 'react';
import { observer } from 'mobx-react-lite';
import { Dropdown, DropdownButton } from 'react-bootstrap';

import { AddBtn } from '../../../components/buttons';
import ItemsList from '../../../components/itemsList';
import { getFullName } from '../../../utils/personNames';
import Card from './../../../components/card';
import routes from './../../../routes';
import useStore from './../../../hooks/useStore';

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
        <Card title={<TitleView onAddClick={onAddClick} checkupStatuses={checkupStatuses} />}>
            <ItemsList
                columns={contingentListColumns}
                items={checkupStatuses}
                getItemUrl={(item) => routes.contingentCheckupStatus.getUrl(item.id)}
                withCheckboxes
            />
        </Card>
    );
}

const TitleView = observer(({ onAddClick }) => {
    const { periodicExaminationsStore } = useStore();
    const { examination: { checkupStatuses } } = periodicExaminationsStore;

    const statusesCount = checkupStatuses.length;
    const checkedCount = checkupStatuses.filter(s => s.checked === true).length;

    return (
        <div style={{ display: 'flex', alignItems: 'center' }}>
            <div className="mr-2">Список работников ({statusesCount})</div>
            {
                checkedCount
                    ?
                    <DropdownButton title="Действие с выбранными" variant="secondary" size="sm">
                        <Dropdown.Item onClick={periodicExaminationsStore.contingentGroupMedicalReportEditorModal.open}>
                            Редактировать заключение
                        </Dropdown.Item>
                    </DropdownButton>
                    : null
            }

            <AddBtn className='btn btn-sm btn-secondary ml-auto' onClick={onAddClick} />
        </div>
    )
})

export default ContingentList;