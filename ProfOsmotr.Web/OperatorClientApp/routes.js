const baseUrl = '/Operator';

const getFullPath = path => baseUrl + path;

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
    employers: {
        path: getFullPath('/employers'),
        name: 'Организации'
    },
    statistics: {
        path: getFullPath('/statistics'),
        name: 'Статистика'
    }
}

export default routes;