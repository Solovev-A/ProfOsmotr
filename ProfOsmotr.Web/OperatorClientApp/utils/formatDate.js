export function formatDateStringForDateInput(value) {
    if (value === null) return '';

    const date = value.split('.');

    const dd = date[0];
    const MM = date[1];
    const yyyy = date[2];

    return `${yyyy}-${MM}-${dd}`;
}