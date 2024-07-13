document.addEventListener('DOMContentLoaded', function () {
    const roleSelect = document.getElementById('role');
    const additionalFields = document.getElementById('additional-fields');

    roleSelect.addEventListener('change', function () {
        if (this.value === 'teacher') {
            additionalFields.style.display = 'block';
        } else {
            additionalFields.style.display = 'none';
        }
    });

    const togglePassword = document.getElementById('togglePassword');
    const password = document.querySelector('input[type="password"]');

    if (togglePassword) {
        togglePassword.addEventListener('click', function () {
            const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
            password.setAttribute('type', type);
            this.classList.toggle('fa-eye');
            this.classList.toggle('fa-eye-slash');
        });
    }

    const togglePassword2 = document.getElementById('togglePassword2');
    const password2 = document.querySelector('.password-2');

    if (togglePassword2) {
        togglePassword2.addEventListener('click', function () {
            const type = password2.getAttribute('type') === 'password' ? 'text' : 'password';
            password2.setAttribute('type', type);
            this.classList.toggle('fa-eye');
            this.classList.toggle('fa-eye-slash');
        });
    }
});
