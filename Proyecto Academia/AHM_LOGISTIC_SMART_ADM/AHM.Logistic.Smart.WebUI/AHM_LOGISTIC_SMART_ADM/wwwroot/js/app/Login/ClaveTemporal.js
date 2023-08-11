var usId;
var persoId;
let isTemporal;
let datosLogin;
$(document).ready(function () {
    ModalContraNueva()
});


function ShowModalRecuperarContraseña() {
    $('#RecuperarContraseña').modal('show');
}

function CloseModalRecuperarContraseña() {
    $('#RecuperarContraseña').modal('hide');
}

function RecoveryPass() {
    let correo = $('#Correotxt').val();
    $.ajax({
        type: "POST",
        url: "https://localhost:44339/User/PasswordRecovery?correo=" + correo,
        dataType: "json",
    }).done(function (data) {
        console.log(data.data);
        if (data.data == null) {
            iziToast.error({
                message: 'El correo que ingreso no esta vinculado a ninguna cuenta existente'
            });
        }
        else {
            iziToast.success({
                message: '¡Se ha enviado un correo con la contraseña de recuperacion!'
            });
            setTimeout(function () {
                location.assign("https://localhost:44339");
            }, 4000)
        }


    }).fail(function () {


    });
}


function ModalContraNueva() {

    isTemporal = sessionStorage.getItem("isTemporal");

    if (isTemporal === 'true') {
        console.log("xd");
        $('#NuevaPassword').modal('show');
        document.getElementById('inputusua').readOnly = true;
        document.getElementById('DropDownRoles').disabled = true;
    } else {

    }

}
function CerrarModalTemp() {
    $('#NuevaPassword').modal('hide');
}
function EnviarPassword() {
    var password = $('#DashPass').val();
    var InsertPassword = {
        "usu_UserName": null,
        "usu_Id": usId,
        "Per_Id": persoId,
        "rol_Id": null,
        "usu_Password": password,
        "NewPassword": null,
        "ConfirmContraseña": null
    };
    $.ajax({
        type: "POST",
        data: InsertPassword,
        url: BaseUrl + "/User/ChangePasswordDash",
    }).done(function (data, index) {

        console.log("xd sirvio");
    }).fail(function () {
        
    });


}


function CerrarModal() {
    $('#RecuperarContraseña').modal('hide');
}