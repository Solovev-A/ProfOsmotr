import Util from './util/common';


function initUserLogin() {
    const URI_API_USER_LOGIN = '/api/user/login';
    const CLASSNAME_DISPLAY_NONE = 'd-none';

    const form = document.login;
    const loginErrorMessage = document.querySelector('.alert-danger');
    const loginButton = document.getElementById('LoginButton');
    loginButton.addEventListener('click', onLogin);

    async function onLogin(e) {
        e.preventDefault();
        loginButton.disabled = true;
        loginErrorMessage.classList.add(CLASSNAME_DISPLAY_NONE);

        const formData = new FormData(form);
        const data = Object.fromEntries(formData);

        const result = await Util.postData(URI_API_USER_LOGIN, data);
        if (result) {
            if (result.succeed === true) {
                location.replace('/');
            }
            else {
                showErrorMessage();
            }
        }
        loginButton.disabled = false;
    }

    function showErrorMessage() {
        loginErrorMessage.classList.remove(CLASSNAME_DISPLAY_NONE);       
    }
}


export default initUserLogin;