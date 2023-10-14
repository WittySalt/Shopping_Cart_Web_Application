let uname_elem, pword_elem, form_elem, msg_elem;

window.onload = function () {

    form_elem = document.getElementById("loginForm");
    msg_elem = document.getElementById("msg");

    if (!form_elem || !msg_elem) {
        return;
    }

    uname_elem = document.getElementById("username");
    pword_elem = document.getElementById("password");

    form_elem.addEventListener("submit", formSubmit);
}

function formSubmit(event) {

    showMsg(false, '');

    let username = uname_elem.value;
    let password = pword_elem.value;

    if (!username || !password) {
        showMsg(true, "Please enter a valid username or password.");
        event.preventDefault();
    }
    else {
        this.submit();}
}

function showMsg(isVisible, msg) {
    msg_elem.innerHTML = '';

    if (isVisible) {
        var alertDiv = document.createElement('div');
        alertDiv.classList.add('alert', 'alert-danger');
        alertDiv.textContent = msg;

        msg_elem.appendChild(alertDiv);

        msg_elem.style.display = 'block';
    }
    else {
        msg_elem.style.display = 'none';
    }
    
}