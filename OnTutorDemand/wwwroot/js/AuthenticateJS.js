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
