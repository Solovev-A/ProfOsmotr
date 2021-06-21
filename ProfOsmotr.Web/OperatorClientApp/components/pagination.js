import React from 'react';

const PageItem = ({ num, isActive, onClick }) => {
    const cn = isActive ? "page-item active" : "page-item";

    const onClickHandler = (event) => {
        event.preventDefault();
        onClick(num);
    }

    return (
        <li className={cn}>
            <a className="page-link" onClick={onClickHandler} style={{ cursor: "pointer" }}>{num}</a>
        </li>
    )
}

const PageSeparator = () => {
    return (
        <li className="page-item disabled"><span className="page-link">...</span></li>
    )
}

const Pagination = ({ currentPage, totalPages, onPageChange }) => {
    const getRange = (from, to) => {
        const pageItems = [];
        for (let i = from; i <= to; i++) {
            pageItems.push(<PageItem key={i} num={i} isActive={currentPage === i} onClick={onPageChange} />)
        }
        return pageItems;
    }

    let items;
    if (totalPages <= 10) {
        items = getRange(1, totalPages);
    } else {
        // если страниц много

        if (currentPage <= 5) {
            items = [...getRange(2, 7), <PageSeparator key="s1" />];
        } else if (currentPage >= totalPages - 4) {
            items = [<PageSeparator key="s1" />, ...getRange(totalPages - 6, totalPages - 1)];
        } else {
            items = [<PageSeparator key="s1" />, ...getRange(currentPage - 2, currentPage + 2), <PageSeparator key="s2" />];
        }

        items.unshift(...getRange(1, 1));
        items.push(...getRange(totalPages, totalPages));
    }

    return (
        <ul className="pagination justify-content-center">
            {items}
        </ul>
    )
}

export default Pagination;