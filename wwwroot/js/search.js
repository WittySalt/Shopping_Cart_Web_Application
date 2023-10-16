function search() {
    var searchString = document.getElementById('searchString').value;
    window.location.href = `/Cart/Index?searchString=${searchString}`;
}
//gain searchString from id