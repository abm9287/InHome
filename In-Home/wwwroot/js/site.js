var principal = new Principal();

var user = new User();
//Función imageUser esta funcion recibirá el parámetro que es el evento evt
var imageUser = (evt) => {
    user.archivo(evt, "imageUser");
}

$().ready(() => {
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
});