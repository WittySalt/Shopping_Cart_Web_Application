let uname_elem, pword_elem;

window.onload = function () {

    let form_elem = document.getElementById("loginForm");
    if (!form_elem) {
        return;
    }
    form.addEventListener("submit", formSubmit);
}


function showMsg(isVisible, msg) {
    let msg_elem = document.getElementById("msg");

    if (!msg_elem) {
        return;
    }

}


//uname_elem = document.getElementById("username");
        //pword_elem = document.getElementById("password");
        //if (uname_elem.value == "" || pword_elem.value =="") {
        //    event.preventDefault();
        //    alert("Please enter a valid username or password.");
        //}