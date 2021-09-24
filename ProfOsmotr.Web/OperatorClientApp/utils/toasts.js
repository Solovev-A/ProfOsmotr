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
    toast.success('Операция успешно завершена', { ...toastConfig, autoClose: 2000 });
}

const errorToast = (message) => {
    toast.error(message || 'Произошла непредвиденная ошибка', toastConfig);
}

const handleResponseWithToasts = (response, showSuccessToast = false) => {
    if (!response || response.success === false) {
        errorToast(response?.message);
    }
    else if (showSuccessToast) {
        successToast();
    }
    return response;
}

export { successToast, errorToast, handleResponseWithToasts };