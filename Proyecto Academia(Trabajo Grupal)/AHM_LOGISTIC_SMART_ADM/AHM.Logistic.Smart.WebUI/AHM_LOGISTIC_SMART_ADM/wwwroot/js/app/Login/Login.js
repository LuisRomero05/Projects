var usId;
var persoId;
let isTemporal;
let datosLogin;
var ip;
var UserAgent;
import("https://api.ipify.org?format=jsonp&callback=getIP");
$(document).ready(function () {
    $('#formLogin').on('submit', function () {
        var message = "";
        var result = ValidateLogin()
        if (result == true) {
            var login = [
                { name: "usu_UserName", value: $('#formLogin #usu_UserName').val() },
                { name: "usu_Password", value: $('#formLogin #usu_Password').val() },
            ];
            $.ajax({
                type: "POST",
                url: "https://localhost:44339/User/Logged",
                data: login,
            }).done(function (data) {
                if (data.success == false && data.type == DataType.Warning) {
                    message = '<span style="color: red;">' + data.message + '</span>';
                    $('#formLogin #usu_Password').after($(message).fadeToggle(3000));
                } else if (data.success == false && data.type == DataType.Error) {
                    message = '<span style="color: red;">' + data.message + '</span>';
                    $('#formLogin #usu_Password').after($(message).fadeToggle(3000));
                } else if (data.success == false && data.type == DataType.GatewayTimeout) {
                    message = '<span style="color: red;">' + data.message + '</span>';
                    $('#formLogin #usu_Password').after($(message).fadeToggle(3000));
                } else {
                    var Id = data.usu_Id;
                    var isTemporal = data.isTemporal;
                    sessionStorage.setItem("Id", Id);
                    sessionStorage.setItem("isTemporal", isTemporal);
                    console.log(isTemporal);
                    if (isTemporal === true) {
                        location.assign("https://localhost:44339/usuario/edit?id=" + Id);
                    } else {
                        location.assign("https://localhost:44339/Dashboard/Index");
                    }

                }

            });

        }
        return false;
    });
    ModalContraNueva()
});

function ValidateLogin() {
    var result = false;
    var count = 0;
    var usu_UserName = $('#formLogin #usu_UserName');
    var usu_Password = $('#formLogin #usu_Password');

    result = MessagesError(usu_UserName, null, 20, 'Nombre de Usuario');
    if (result == true) { count++; }
    result = MessagesError(usu_Password, null, 50, 'Contraseña');
    if (result == true) { count++; }
    if (count == 2) {
        return result;
    }
    return false;
}

$('#formLogin #usu_UserName').on('keypress', function () {
    $('#formLogin #usu_UserName').css("border-color", "#eee");
});

$('#formLogin #usu_Password').on('keypress', function () {
    $('#formLogin #usu_Password').css("border-color", "#eee");
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
        //mostrar alerta en caso de error

    });


}


function CerrarModal() {
    $('#RecuperarContraseña').modal('hide');
}
