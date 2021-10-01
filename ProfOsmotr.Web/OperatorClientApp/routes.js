const base = '/Operator';

class Route {
    constructor({ path, name, baseUrl = base }) {
        this.path = baseUrl + path;
        this.name = name;
    }
}

class RouteWithSlug extends Route {
    constructor({ path, name, slug = ':id', baseUrl = base }) {
        super({ path, name, baseUrl });
        this.slug = slug;
    }

    getUrl = (slugValue) => {
        return this.path.replace(this.slug, slugValue);
    }
}

class ApiRouteWithSlug extends RouteWithSlug {
    constructor({ path, name }) {
        super({ path, name, baseUrl: '/api' });
    }
}


const routes = {
    base,
    preliminaryExaminations: new Route({
        path: '/examinations/preliminary',
        name: 'Предварительные осмотры'
    }),
    preliminaryExaminationsJournal: new Route({
        path: '/examinations/preliminary/journal',
        name: 'Журнал предварительных осмотров'
    }),
    preliminaryExamination: new RouteWithSlug({
        path: '/examinations/preliminary/:id',
        name: 'Карта предварительного осмотра'
    }),
    preliminaryExaminationMedicalReport: new ApiRouteWithSlug({
        path: '/examinations/preliminary/:id/report',
        name: 'Заключение по результатам предварительного осмотра'
    }),
    periodicExaminations: new Route({
        path: '/examinations/periodic',
        name: 'Периодические осмотры'
    }),
    periodicExaminationsJournal: new Route({
        path: '/examinations/periodic/journal',
        name: 'Журнал периодических осмотров'
    }),
    periodicExamination: new RouteWithSlug({
        path: '/examinations/periodic/:id',
        name: 'Карта периодического осмотра'
    }),
    contingentCheckupStatus: new RouteWithSlug({
        path: '/examinations/periodic/checkup-statuses/:id',
        name: 'Карта периодического медосмотра работника'
    }),
    contingentCheckupStatusMedicalReport: new ApiRouteWithSlug({
        path: '/examinations/periodic/checkup-statuses/:id/report',
        name: 'Заключение периодического медосмотра работника'
    }),
    patients: new Route({
        path: '/patients',
        name: 'Пациенты'
    }),
    patient: new RouteWithSlug({
        path: '/patients/:id',
        name: 'Карта пациента'
    }),
    createPatient: new Route({
        path: '/patients/new',
        name: 'Добавление нового пациента'
    }),
    editPatient: new RouteWithSlug({
        path: '/patients/:id/edit',
        name: 'Редактирование пациента'
    }),
    employers: new Route({
        path: '/employers',
        name: 'Организации'
    }),
    employer: new RouteWithSlug({
        path: '/employers/:id',
        name: 'Карта организации'
    }),
    createEmployer: new Route({
        path: '/employers/new',
        name: 'Добавление новой организации'
    }),
    editEmployer: new RouteWithSlug({
        path: '/employers/:id/edit',
        name: 'Редактирование пациента'
    }),
    statistics: new Route({
        path: '/statistics',
        name: 'Статистика'
    })
}


export default routes;