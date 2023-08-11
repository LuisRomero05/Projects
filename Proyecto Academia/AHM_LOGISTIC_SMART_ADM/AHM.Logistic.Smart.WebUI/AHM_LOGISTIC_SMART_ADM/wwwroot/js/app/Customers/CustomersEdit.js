//input que decea agregarles las cualidades del jQuery
var con_PhoneEd = document.querySelector("#cus_Phone");
var cus_AnotherPhoneEd = document.querySelector("#cus_AnotherPhone");

//Paramtros sobre las cualidades del input type tel
var con_PhoneItiEd = window.intlTelInput(con_PhoneEd, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});

var cus_AnotherPhoneItiEd = window.intlTelInput(cus_AnotherPhoneEd, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});

// Ulilizada para elegir que aria va enviar.
var ValidarTele = true;
var ValidarTele2 = true;
//El numero de aria al selecionar el pais, sera guardado en la variable
var contPhoneEd = "";
con_PhoneEd.addEventListener("countrychange", function () {
    var countryData = con_PhoneItiEd.getSelectedCountryData();
    ValidarTele = false;
    contPhoneEd = "+" + countryData.dialCode + " ";
});
var contPhoneEd2 = "";
cus_AnotherPhoneEd.addEventListener("countrychange", function () {
    var countryData = cus_AnotherPhoneItiEd.getSelectedCountryData();
    ValidarTele2 = false;
    contPhoneEd2 = "+" + countryData.dialCode + " ";
});
//el numero de aria al cargar
var countryDataEd = con_PhoneItiEd.getSelectedCountryData();
var ariaInicioContEd = "+" + countryDataEd.dialCode + " ";
var countryDataEd2 = cus_AnotherPhoneItiEd.getSelectedCountryData();
var ariaInicioContEd2 = "+" + countryDataEd2.dialCode + " ";
var done = false;
var idMuni = $("#municipio").val();
var depart = $("#departamento").val();
var est = $("#miCheckbox").val();
var userasig = null;
var iduser = $("#iduser").val();
var NewOption = "<option value=" + 0 + " selected> Por favor seleccione una opción... </option>";

const $miCheckbox = document.querySelector("#miCheckbox2");
//original
$(document).ready(function () {
    GetUsuariosList2();
    GetSubDepartmentsListCreate2();
    GetSubMunicipalitiesListCreate2();
    GetDropdowns();
    GetEstado();
});

function GetEstado() {
    //const $miCheckbox = document.querySelector("#miCheckbox2");
    var valor = false;
    //console.log("data original", est);
    if (est == "Activo") {
        valor = true;
        //console.log("siuuuuuuuuuuu", valor)
        $miCheckbox.checked = true;
    }
    else {
        valor = false;
        //console.log("noooooooooooo", valor)
        $miCheckbox.checked = false;
    }
};



function GetSubDepartmentsListCreate2() {

    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectDepartments2").empty();

        var NewOption = "<option value=" + 0 + " disabled> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, "", data.id, data.data, data.type);
            }
        }
        else {
            data.data.forEach(function (item) {
                if (item.dep_Id == depart) {
                    NewOption += "<option  value=" + item.dep_Id + " selected>" + item.dep_Description + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.dep_Id + ">" + item.dep_Description + "</option>";
                }
            });

        }

        //Agregar las opciones al dropdownlist
        $("#SelectDepartments2").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectDepartments2').after($(message));
    });
}

function GetSubMunicipalitiesListCreate2() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/MuniList",
    }).done(function (data, index) {
        $("#SelectMunicipalities2").empty();
        NewOption = "<option value=" + 0 + " disabled> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, "", data.id, data.data, data.type);
            }
        }
        else {
            data.data.forEach(function (item) {
                if (item.mun_Id == idMuni) {
                    NewOption += "<option  value=" + item.mun_Id + "  selected>" + item.mun_Description + "</option>";
                }
                else {
                    if (item.dep_Id == depart) {
                        NewOption += "<option  value=" + item.mun_Id + ">" + item.mun_Description + "</option>";
                    }
                }

            });

        }
        //Agregar las opciones al dropdownlist
        document.getElementById("SelectMunicipalities2").innerHTML = NewOption;

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectMunicipalities2').after($(message));
    });
}

function GetSubMunicipalitiesListCreate4() {
    var idDepar = $("#SelectDepartments2").val();
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/MuniList",
    }).done(function (data, index) {
        $("#SelectMunicipalities2").empty();
        NewOption = "<option value=" + 0 + " selected> Por favor seleccione una opción... </option>";

        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, "", data.id, data.data, data.type);
            }
        }
        else {
            data.data.forEach(function (item) {
                if (item.dep_Id == idDepar) {
                    NewOption += "<option  value=" + item.mun_Id + ">" + item.mun_Description + "</option>";
                }

            });

        }
        //Agregar las opciones al dropdownlist
        $("#SelectMunicipalities2").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;" name="Mesas">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectMunicipalities2').after($(message));
    });
}

function GetUsuariosList2() {

    $.ajax({
        type: "GET",
        url: BaseUrl + "/User/UsersList",
    }).done(function (data, index) {
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        data.data.forEach(function (item) {

            if (item.usu_Id == iduser) {
                NewOption += "<option  value=" + item.usu_Id + " selected>" + item.usu_UserName + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.usu_Id + ">" + item.usu_UserName + "</option>";
            }
        });
        $("#SelectUsuario2").append(NewOption);

    }).fail(function () {
    });
}

$('#frmEditCustomer').on('submit', function () {
    var result = ValidateEditFrmCustomer();
    if (result == true) {

        Swal.fire({
            width: '20%',
            height: '20%',
            text: "¿Estás seguro que deseas guardar los cambios?",
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonColor: '#6c757d',
            cancelButtonColor: '#001f52',
            confirmButtonText: 'Aceptar'
        }).then((result) => {

            /*Para agregar el aria al numero*/
            var telefonoFinal = "";
            if (ValidarTele === true) {
                telefonoFinal = ariaInicioContEd + con_PhoneEd.value;
                //console.log("telefonoFinal if", telefonoFinal);
            } else {
                telefonoFinal = contPhoneEd + con_PhoneEd.value;
                //console.log("telefonoFinal else", telefonoFinal);
            }

            var telefonoFinal2 = "";

            if (cus_AnotherPhone.value != "") {


                if (ValidarTele2 === true) {
                    telefonoFinal2 = ariaInicioContEd2 + cus_AnotherPhoneEd.value;
                    //console.log("telefonoFinal if", telefonoFinal);
                } else {
                    telefonoFinal2 = contPhoneEd2 + cus_AnotherPhoneEd.value;
                    //console.log("telefonoFinal else", telefonoFinal2);
                }
            } else {
                telefonoFinal2 = cus_AnotherPhone.value;
            }

            /*end*/

             var valorSQL = false;
    if ($miCheckbox.checked == true) {

        valorSQL = true;
        //console.log("data para sql", valorSQL)
    }
    else {
        valorSQL = false;
        //console.log("data para sql", valorSQL)
    }

            if (result.isConfirmed) {
                var customer = [
                    { name: "cus_Id", value: $('#frmEditCustomer #cus_Id').val() },
                    { name: "cus_AssignedUser", value: $('#frmEditCustomer #SelectUsuario2').val() },
                    { name: "tyCh_Id", value: $('#frmEditCustomer #tyCh_Id').val() },
                    { name: "cus_Name", value: $('#frmEditCustomer #cus_Name').val().trim() },
                    { name: "cus_RTN", value: $('#frmEditCustomer #cus_RTN').val().trim() },
                    { name: "cus_Address", value: $('#frmEditCustomer #cus_Address').val().trim() },
                    { name: "dep_Id", value: $('#frmEditCustomer #SelectDepartments2').val() },
                    { name: "mun_Id", value: $('#frmEditCustomer #SelectMunicipalities2').val() },
                    { name: "cus_Email", value: $('#frmEditCustomer #cus_Email').val().trim() },
                    { name: "cus_Phone", value: telefonoFinal.trim() },
                    { name: "cus_AnotherPhone", value: telefonoFinal2.trim() },
                    { name: "Estado", value: valorSQL },
                    { name: "cus_IdUserCreate", value: TempUserDefault },
                    { name: "cus_IdUserModified", value: TempUserDefault },
                    { name: "cus_DateModified", value: TempUserDefault },
                ];

                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/Customers/Edit",
                    data: customer,
                }).done(function (data) {
                    if (done == false) {
                        done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    }
                    setTimeout(function () {
                        //location.assign(BaseUrl + "/Customers/index");
                        location.reload();
                    }, 1500)
                }).fail(function (data) {
                    if (done == false) {
                        NotificationMessage(data.success, "", data.id, data.data, data.type);
                    }
                });
            }
        })
    }
    return false;
});

function ValidateEditFrmCustomer() {
    LimpiarSpanMessa();
    var result = true;
    var count = 0;

    //usario a cargo
    var SelectCust2 = $('#frmEditCustomer #SelectUsuario2');
    var DivCust2 = $('#frmEditCustomer #messageCre4');
    var BorCust2 = $('#frmEditCustomer #messageCre4 span.select2-selection.select2-selection--single');

    //tipo de canal
    var tyDropDown2 = $('#frmEditCustomer #tyCh_Id');
    var DivTY2 = $('#frmEditCustomer #messageCre3');
    var BorTY2 = $('#frmEditCustomer #messageCre3 span.select2-selection.select2-selection--single');

    var cus_Name = $('#frmEditCustomer #cus_Name');
    var cus_RTN = $('#frmEditCustomer #cus_RTN');
    var cus_Address = $('#frmEditCustomer #cus_Address');

    //Municipio
    var SelectMuni2 = $('#frmEditCustomer #SelectMunicipalities2');
    var DivMuni2 = $('#frmEditCustomer #messageCre2');
    var BorMuni2 = $('#frmEditCustomer #messageCre2 span.select2-selection.select2-selection--single')


    //Departamento
    var SelectDep2 = $('#frmEditCustomer #SelectDepartments2');
    var DivDep2 = $('#frmEditCustomer #messageCre');
    var BorDep2 = $('#frmEditCustomer #messageCre span.select2-selection.select2-selection--single')

    var cus_Email = $('#frmEditCustomer #cus_Email');
    var cus_Phone = $('#frmEditCustomer #cus_Phone');
    var cont_PhoneMess = $("#frmEditCustomer #cont_PhoneMess1");



    result = MessageErrorDrop(SelectCust2, 'Usuario', DivCust2, BorCust2);
    if (result == true) { count++; }
    result = MessageErrorDrop(tyDropDown2, 'Tipo de Canal de Comunicación', DivTY2, BorTY2);

    if (result == true) { count++; }
    result = MessagesError(cus_Name, 2, 200, 'Nombre');
    if (result == true) { count++; }
    result = MessagesError(cus_RTN, 0, 14, 'RTN');
    if (result == true) { count++; }
    result = MessagesError(cus_Address, 0, 200, 'Dirección');
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectDep2, 'Departamento', DivDep2, BorDep2);

    if (result == true) { count++; }
    result = MessageErrorDrop(SelectMuni2, 'Municipio', DivMuni2, BorMuni2);

    if (result == true) { count++; }
    if (cus_Email.val().indexOf('@', 0) == -1 || cus_Email.val().indexOf('.', 0) == -1) {
        message = '<span style="color: red;" name="Mesas">El campo de Email es inválido</span>';
        cus_Email.after($(message).fadeToggle(3000));
        cus_Email.css("border-color", "red");
        count++;
    }
    result = telMessagesError(cus_Phone, 'Teléfono', con_PhoneItiEd, cont_PhoneMess);
    if (result == true) { count++; }
    if (count == 8) {
        return result;
    }
    return false;
}

$('#frmEditCustomer #SelectUsuario2').on('change', function () {
    $('#frmEditCustomer #SelectUsuario2').css("border-color", "#eee");
});//ID ASIGNADO

$('#frmEditCustomer #tyCh_Id').on('change', function () {
    $('#frmEditCustomer #tyCh_Id').css("border-color", "#eee");
});//ESTADO

$('#frmEditCustomer #cus_Name').on('change', function () {
    $('#frmEditCustomer #cus_Name').css("border-color", "#eee");
});//NOMBRE

$('#frmEditCustomer #cus_RTN').on('keypress', function () {
    $('#frmEditCustomer #cus_RTN').css("border-color", "#eee");
});//RTN

$('#frmEditCustomer #cus_Address').on('change', function () {
    $('#frmEditCustomer #cus_Address').css("border-color", "#eee");
});//direccion

$('#frmEditCustomer #SelectDepartments2').on('change', function () {
    $('#frmEditCustomer #SelectDepartments2').css("border-color", "#eee");
});//departamento

$('#frmEditCustomer #SelectMunicipalities2').on('change', function () {
    $('#frmEditCustomer #SelectMunicipalities2').css("border-color", "#eee");
});//municipio

$('#frmEditCustomer #cus_Email').on('change', function () {
    $('#frmEditCustomer #cus_Email').css("border-color", "#eee");
});//email

$('#frmEditCustomer #cus_Phone').on('keypress', function () {
    $('#frmEditCustomer #cus_Phone').css("border-color", "#eee");
});//telefono

$('#frmEditCustomer #cus_AnotherPhone').on('change', function () {
    $('#frmEditCustomer #cus_AnotherPhone').css("border-color", "#eee");
});//otro telefono

$('#frmEditCustomer #messageCre4').on('select2:select', function () {
    $('#frmEditCustomer #messageCre4 span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#frmEditCustomer #messageCre3').on('select2:select', function () {
    $('#frmEditCustomer #messageCre3 span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#frmEditCustomer #messageCre').on('select2:select', function () {
    $('#frmEditCustomer #messageCre span.select2-selection.select2-selection--single').css("border-color", "#eee");
});


$('#frmEditCustomer #messageCre2').on('select2:select', function () {
    $('#frmEditCustomer #messageCre2 span.select2-selection.select2-selection--single').css("border-color", "#eee");
});



$('#frmEditCustomer #cus_Phone').keyup(function (e) {
    //console.log("Funciona la validacion de letras fuera del if ");
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
        //console.log("Funciona la validacion de letras dentro del if ");
    }
});

$('#frmEditCustomer #cus_AnotherPhone').keyup(function (e) {
    //console.log("Funciona la validacion de letras fuera del if ");
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
        //console.log("Funciona la validacion de letras dentro del if ");
    }
});

function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}

$('#frmEditCustomer #cus_RTN').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});

function GetDropdowns() {
    $("#SelectDepartments2").select2();
    $("#SelectMunicipalities2").select2();
    $('#tyCh_Id').select2();
    $('#SelectUsuario2').select2();

}