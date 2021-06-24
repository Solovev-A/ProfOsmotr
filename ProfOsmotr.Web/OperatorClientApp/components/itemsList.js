import React, { useState } from 'react';

const ListItemView = ({ item, columns, onClick }) => {
    const [hover, setHover] = useState(false);
    const style = {
        cursor: 'pointer',
        backgroundColor: hover ? 'rgba(0,0,0,.03)' : '#fff'
    }

    return (
        <tr onClick={() => onClick(item)}
            style={style}
            onMouseEnter={() => setHover(true)}
            onMouseLeave={() => setHover(false)}
        >
            {
                columns.map((col, index) => {
                    return (
                        <td key={index}>
                            {col.render(item)}
                        </td>
                    )
                })
            }
        </tr>
    )
}

const ItemsList = ({ items, onItemCLick, columns }) => {
    return (
        <table className="table">
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
                    items.map(item => <ListItemView key={item.id} item={item} columns={columns} onClick={onItemCLick} />)
                }
            </tbody>
        </table>
    )
}

export default ItemsList;