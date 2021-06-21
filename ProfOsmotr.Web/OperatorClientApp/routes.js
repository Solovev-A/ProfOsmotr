const baseUrl = '/Operator';

const getFullPath = path => baseUrl + path;

const getUrl = (path, slug, slugValue) => {
    return path.replace(slug, slugValue);
}

const routes = {
    baseUrl,
    preliminaryExaminations: {
        path: getFullPath('/examinations/preliminary'),
        name: 'Предварительные осмотры'
    },
    periodicExaminations: {
        path: getFullPath('/examinations/periodic'),
        name: 'Периодические осмотры'
    },
    patients: {
        path: getFullPath('/patients'),
        name: 'Пациенты'
    },
    patient: {
        path: getFullPath('/patients/:id'),
        getUrl(id) { return getUrl(this.path, ':id', id) },
        name: 'Карта пациента'
    },
    employers: {
        path: getFullPath('/employers'),
        name: 'Организации'
    },
    statistics: {
        path: getFullPath('/statistics'),
        name: 'Статистика'
    },
    createPatient: {
        path: getFullPath('/patients/new'),
        name: 'Добавление нового пациента'
    },
    editPatient: {
        path: getFullPath('/patients/:id/edit'),
        getUrl(id) { return getUrl(this.path, ':id', id) },
        name: 'Редактирование пациента'
    }
}



export default routes;