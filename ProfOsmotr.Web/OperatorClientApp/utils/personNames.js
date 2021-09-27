const getFullName = (person) => {
    return `${person.lastName} ${person.firstName} ${person.patronymicName}`.trim();
}

const getShortName = (person) => {
    const patronymicNameShort = person.patronymicName ? `${person.patronymicName[0]}.` : null;

    return `${person.lastName} ${person.firstName[0]}.${patronymicNameShort}`.trim();
}

export { getFullName, getShortName }