import React from 'react';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';
import { runInAction } from 'mobx';


const ItemsList = ({ items, getItemUrl, columns, withCheckboxes = false }) => {
    if (!columns) throw 'Отсутствует обязательное свойство columns'

    if (!items.length) {
        return (
            <div className="text-center font-italic">
                Список пуст
            </div>
        )
    }


    return (
        <table className="table table-hover">
            <thead>
                <tr>
                    {withCheckboxes && <td><MasterCheckbox items={items} /></td>}
                    {
                        columns.map((col, index) => {
                            const { title, width = 'auto' } = col;

                            return (
                                <th key={index} style={{ width }}>
                                    {title}
                                </th>
                            )
                        })
                    }
                </tr>
            </thead>
            <tbody>
                {
                    items.map(item => (
                        <ListItemView key={item.id}
                            item={item}
                            columns={columns}
                            itemUrl={getItemUrl(item)}
                            withCheckbox={withCheckboxes}
                        />
                    ))
                }
            </tbody>
        </table>
    )
}

const ListItemView = observer(({ item, columns, itemUrl, withCheckbox = false }) => {
    const onChange = (event) => {
        runInAction(() => {
            item.checked = event.target.checked;
        })
    }

    return (
        <tr>
            {withCheckbox && <td><Checkbox checked={item.checked === true} onChange={onChange} /></td>}
            {
                columns.map((col, index) => {
                    const stretchedLink = col.disableLink ? null : <Link to={itemUrl} className='stretched-link' />
                    return (
                        <td key={index} style={{ position: 'relative' }}>
                            {stretchedLink}
                            {col.render(item)}
                        </td>
                    )
                })
            }
        </tr>
    )
})

const Checkbox = (props) => {
    return (
        <div className="form-check">
            <input type="checkbox" className="form-check-input" {...props} />
        </div>
    )
}

const MasterCheckbox = observer(({ items }) => {
    if (items.length === 0) return null;

    const isAllChecked = !items.some(item => !item.checked);
    const onChange = () => {
        runInAction(() => {
            items.forEach(item => item.checked = !isAllChecked)
        })
    }

    return (
        <Checkbox checked={isAllChecked} onChange={onChange} />
    )
})


export default ItemsList;