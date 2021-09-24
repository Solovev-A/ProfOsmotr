import React from 'react';
import { Dropdown } from 'react-bootstrap';


const YearPicker = ({ title, onYearPick, start = 2020, end = new Date().getFullYear(), ...props }) => {
    if (start > end) throw 'Нижняя граница диапазона лет не может быть больше верхней';

    return (
        <div {...props}>
            <Dropdown>
                <Dropdown.Toggle variant='secondary'>
                    {title}
                </Dropdown.Toggle>
                <Dropdown.Menu style={{ maxHeight: "250px", overflowY: "auto" }}>
                    <ItemsList start={start} end={end} onItemClick={onYearPick} />
                </Dropdown.Menu>
            </Dropdown>
        </div>
    )
}


const ItemsList = ({ start, end, onItemClick }) => {
    const range = [];

    for (let i = start; i <= end; i++) {
        range.push(i);
    }

    const onClick = (event) => {
        const year = Number(event.target.text);
        onItemClick(year);
    }

    return (
        <>
            {
                range.map(year => {
                    return (
                        <Dropdown.Item key={year} onClick={onClick}>
                            {year}
                        </Dropdown.Item>
                    )
                })
            }
        </>
    )
}


export default YearPicker;