import React from 'react';
import { Link } from 'react-router-dom';

const ListItemView = ({ item, columns, itemUrl }) => {

    return (
        <tr>
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
}

const ItemsList = ({ items, getItemUrl, columns }) => {
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
                    items.map(item => <ListItemView key={item.id} item={item} columns={columns} itemUrl={getItemUrl(item)} />)
                }
            </tbody>
        </table>
    )
}

export default ItemsList;