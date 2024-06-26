const registerButton = document.getElementById("register");
const loginButton = document.getElementById("login");
const container = document.getElementById("container");

registerButton.addEventListener("click", () => {
    container.classList.add("right-panel-active");
});

loginButton.addEventListener("click", () => {
    container.classList.remove("right-panel-active");
});
document.getElementById('role').addEventListener('change', function () {
    var additionalFields = document.getElementById('additional-fields');
    if (this.value === 'teacher') {
        additionalFields.style.display = 'block';
    } else {
        additionalFields.style.display = 'none';
    }
});

document.getElementById('birthdate').addEventListener('focus', function (event) {
    event.target.placeholder = 'mm/dd/yyyy';
});

document.getElementById('birthdate').addEventListener('blur', function (event) {
    event.target.placeholder = 'Ngày sinh';
});

document.addEventListener('DOMContentLoaded', function () {
    const togglePassword = document.getElementById('togglePassword');
    const password = document.getElementById('password');

    togglePassword.addEventListener('click', function () {
        // Toggle the type attribute
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);

        // Toggle the eye / eye slash icon
        this.classList.toggle('fa-eye');
        this.classList.toggle('fa-eye-slash');
    });

    const togglePassword2 = document.getElementById('togglePassword2');
    const password2 = document.querySelector('.password-2');

    togglePassword2.addEventListener('click', function () {
        // Toggle the type attribute
        const type = password2.getAttribute('type') === 'password' ? 'text' : 'password';
        password2.setAttribute('type', type);

        // Toggle the eye / eye slash icon
        this.classList.toggle('fa-eye');
        this.classList.toggle('fa-eye-slash');
    });
});

