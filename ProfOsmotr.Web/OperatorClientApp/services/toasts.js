import { toast } from 'react-toastify';

const toastConfig = {
    position: "bottom-right",
    autoClose: 10000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
};

const successToast = () => {
    toast.success('Операция успешно завершена', toastConfig);
}

const errorToast = (message) => {
    toast.error(message || 'Произошла непредвиденная ошибка', toastConfig);
}

export { successToast, errorToast };