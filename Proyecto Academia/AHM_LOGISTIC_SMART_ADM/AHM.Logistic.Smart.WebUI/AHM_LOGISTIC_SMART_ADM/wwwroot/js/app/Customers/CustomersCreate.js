//telefono#1
//input que decea agregarles las cualidades del jQuery
var cus_Phone = document.querySelector("#cus_Phone");

//Paramtros sobre las cualidades del input type tel
var cont_PhoneIti1 = window.intlTelInput(cus_Phone, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    /*    separateDialCode: true,*/
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});

//El numero de aria al selecionar el pais, sera guardado en la variable
var contPhone1 = "";
cus_Phone.addEventListener("countrychange", function () {
    var countryData = cont_PhoneIti1.getSelectedCountryData();
    contPhone1 = "+" + countryData.dialCode + " ";
    //    console.log("contPhone: ", contPhone);
});

//el numero de aria al cargar
var countryData = cont_PhoneIti1.getSelectedCountryData();
var ariaInicioCont = "+" + countryData.dialCode + " ";

//telefono#2
//input que decea agregarles las cualidades del jQuery
var cus_AnotherPhone = document.querySelector("#cus_AnotherPhone");

//Paramtros sobre las cualidades del input type tel
var cont_PhoneIti2 = window.intlTelInput(cus_AnotherPhone, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    /*    separateDialCode: true,*/
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});

//El numero de aria al selecionar el pais, sera guardado en la variable
var contPhone2 = "";
cus_AnotherPhone.addEventListener("countrychange", function () {
    var countryData = cont_PhoneIti2.getSelectedCountryData();
    contPhone2 = "+" + countryData.dialCode + " ";
    //    console.log("contPhone: ", contPhone);
});

//el numero de aria al cargar
var countryData = cont_PhoneIti2.getSelectedCountryData();
var ariaInicioCont = "+" + countryData.dialCode + " ";

var done = false;
var id = $("#SelectMunicipalities2").val();
var depart = $("#SelectDepartments2").val();
$(document).ready(function () {
    GetDropdowns();
    GetSubDepartmentsListCreate2();
    GetSubMunicipalitiesListCreate2();
    GetUsuariosList();
});

function GetSubDepartmentsListCreate2(depart) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectDepartments2").empty();
        //var NewOption = "";
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
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
        $("#SelectDepartments2").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectDepartments2').after($(message));
    });
}

function GetSubMunicipalitiesListCreate2() {
    $("#SelectMunicipalities2").attr('disabled', false);
    //id = 0;
    //GetSubMunicipalitiesListCreate2();

    var value = $("#SelectDepartments2").val();
    if (value == null) {
        $.ajax({
            type: "GET",
            url: BaseUrl + "/Muni/MuniList",
        }).done(function (data, index) {
            if (data.data == null) {
                if (done == false) {
                    done = NotificationMessage(data.success, "", data.id, data.data, data.type);
                }
            }
            else {
                data.data.forEach(function (item) {
                    if (item.mun_Id == id) {
                        depart = item.dep_Id
                        GetSubDepartmentsListCreate2(depart)
                    }
                });
            }
        });
    }
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/MuniList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectMunicipalities2").empty();
        //variable que almacena las opciones
        //var NewOption = "";
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, "", data.id, data.data, data.type);
            }
        }
        else {
            data.data.forEach(function (item) {
                if (item.mun_Id == id) {
                    NewOption += "<option  value=" + item.mun_Id + "  selected>" + item.mun_Description + "</option>";
                }
                else {
                    if (item.dep_Id == value) {
                        NewOption += "<option  value=" + item.mun_Id + ">" + item.mun_Description + "</option>";
                    }
                }
            });
        }

        //Agregar las opciones al dropdownlist
        $("#SelectMunicipalities2").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectMunicipalities2').after($(message));
    });
}

function GetUsuariosList() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/User/UsersList",
    }).done(function (data, index) {
        $("#SelectUsuario2").empty();
        var id = $("#User").val();
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        data.data.forEach(function (item) {
            if (item.usu_Id == UserId) {
                NewOption += "<option  value=" + item.usu_Id + " selected>" + item.usu_UserName + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.usu_Id + ">" + item.usu_UserName + "</option>";
            }
        });
        $("#SelectUsuario2").append(NewOption);
    }).fail(function () {
        console.log("Error al cargar los Departamentos");
    });
}

$('#frmCreateCustomer').on('submit', function () {
    var result = ValidateFrmCustomer();
    if (result == true) {

        var telefonoFinal1 = "";
        if (contPhone1 === "") {
            telefonoFinal1 = ariaInicioCont + cus_Phone.value;
        } else {
            telefonoFinal1 = contPhone1 + cus_Phone.value;
        }
        var telefonoFinal2 = "";
        if (cus_AnotherPhone.value != "") {

            if (contPhone2 === "") {
                telefonoFinal2 = ariaInicioCont + cus_AnotherPhone.value;
            } else {
                telefonoFinal2 = contPhone2 + cus_AnotherPhone.value;
            }
        } else {
            telefonoFinal2 = cus_AnotherPhone.value;
        }
        var customer = [
            { name: "cus_AssignedUser", value: $('#frmCreateCustomer #SelectUsuario2').val() },
            { name: "tyCh_Id", value: $('#frmCreateCustomer #tyCh_Id').val() },
            { name: "cus_Name", value: $('#frmCreateCustomer #cus_Name').val().trim() },
            { name: "cus_RTN", value: $('#frmCreateCustomer #cus_RTN').val().trim() },
            { name: "cus_Address", value: $('#frmCreateCustomer #cus_Address').val().trim() },
            { name: "dep_Id", value: $('#frmCreateCustomer #SelectDepartments2').val() },
            { name: "mun_Id", value: $('#frmCreateCustomer #SelectMunicipalities2').val() },
            { name: "cus_Email", value: $('#frmCreateCustomer #cus_Email').val().trim() },
            { name: "cus_Phone", value: telefonoFinal1.trim() },
            { name: "cus_AnotherPhone", value: telefonoFinal2.trim() },
            { name: "cus_IdUserCreate", value: UserId },
            { name: "cus_IdUserModified", value: null },
        ];

        $.ajax({
            type: "POST",
            url: BaseUrl + "/Customers/Create",
            data: customer,
        }).done(function (data) {
            var result = true;
            result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            if (result) {
                prueba = data.data.cus_Id;
                setTimeout(function () {
                    location.assign("https://localhost:44339/customers/edit/" + prueba);
                    //location.assign(BaseUrl + "/Customers/Index");
                }, 1500)
            }
        });
    }
    return false;

});

function ValidateFrmCustomer() {
    LimpiarSpanMessa();
    var result = true;
    var count = 0;
    //usario a cargo
    var SelectCust = $('#frmCreateCustomer #SelectUsuario2');
    var DivCust = $('#frmCreateCustomer #messageCre4');
    var BorCust = $('#frmCreateCustomer #messageCre4 span.select2-selection.select2-selection--single');
    //tipo de canal
    var tyDropDown = $('#frmCreateCustomer #tyCh_Id');
    var DivTY = $('#frmCreateCustomer #messageCre3');
    var BorTY = $('#frmCreateCustomer #messageCre3 span.select2-selection.select2-selection--single');

    var cus_Name = $('#frmCreateCustomer #cus_Name');//nombre
    var cus_RTN = $('#frmCreateCustomer #cus_RTN');//RTN
    var cus_Address = $('#frmCreateCustomer #cus_Address');//DIRECCION
    //Departamento
    var SelectDep = $('#frmCreateCustomer #SelectDepartments2');
    var DivDep = $('#frmCreateCustomer #messageCre');
    var BorDep = $('#frmCreateCustomer #messageCre span.select2-selection.select2-selection--single')
    //Municipio
    var SelectMuni = $('#frmCreateCustomer #SelectMunicipalities2');
    var DivMuni = $('#frmCreateCustomer #messageCre2');
    var BorMuni = $('#frmCreateCustomer #messageCre2 span.select2-selection.select2-selection--single')

    var cus_Email = $('#frmCreateCustomer #cus_Email');//email
    //telefono1
    var cus_Phone = $('#frmCreateCustomer #cus_Phone');
    var cont_PhoneMess1 = $("#frmCreateCustomer #cont_PhoneMess1");

    result = MessageErrorDrop(SelectCust, 'Usuario', DivCust, BorCust);
    if (result == true) { count++; }

    result = MessageErrorDrop(tyDropDown, 'Tipo de Canal de Comunicación', DivTY, BorTY);
    if (result == true) { count++; }
    result = MessagesError(cus_Name, 2, 200, 'Nombre');
    if (result == true) { count++; }
    result = MessagesError(cus_RTN, 0, 14, 'RTN');
    if (result == true) { count++; }
    result = MessagesError(cus_Address, 0, 200, 'Dirección');
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectDep, 'Departamento', DivDep, BorDep);
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectMuni, 'Municipio', DivMuni, BorMuni);
    if (result == true) { count++; }

    if (cus_Email.val().indexOf('@', 0) == -1 || cus_Email.val().indexOf('.', 0) == -1) {
        message = '<span style="color: red;" name="Mesas">El campo de Email es inválido  </span>';
        cus_Email.after($(message).fadeToggle(3000));
        cus_Email.css("border-color", "red");
        count++;
    }
    result = telMessagesError(cus_Phone, 'Teléfono', cont_PhoneIti1, cont_PhoneMess1);
    if (result == true) { count++; }
    if (count == 8) {
        return result;
    }
    return false;
}

$('#frmCreateCustomer #SelectUsuario2').on('change', function () {
    $('#frmCreateCustomer #SelectUsuario2').css("border-color", "#eee");
});//ID ASIGNADO

$('#frmCreateCustomer #tyCh_Id').on('change', function () {
    $('#frmCreateCustomer #tyCh_Id').css("border-color", "#eee");
});//ESTADO

$('#frmCreateCustomer #cus_Name').on('keypress', function () {
    $('#frmCreateCustomer #cus_Name').css("border-color", "#eee");
});//NOMBRE

$('#frmCreateCustomer #cus_RTN').on('change', function () {
    $('#frmCreateCustomer #cus_RTN').css("border-color", "#eee");
});//RTN

$('#frmCreateCustomer #cus_Address').on('change', function () {
    $('#frmCreateCustomer #cus_Address').css("border-color", "#eee");
});//direccion

$('#frmCreateCustomer #SelectDepartments2').on('change', function () {
    $('#frmCreateCustomer #SelectDepartments2').css("border-color", "#eee");
});//departamento

$('#frmCreateCustomer #SelectMunicipalities2').on('change', function () {
    $('#frmCreateCustomer #SelectMunicipalities2').css("border-color", "#eee");
});//municipio

$('#frmCreateCustomer #cus_Email').on('change', function () {
    $('#frmCreateCustomer #cus_Email').css("border-color", "#eee");
});//email

$('#frmCreateCustomer #cus_Phone').on('keypress', function () {
    $('#frmCreateCustomer #cus_Phone').css("border-color", "#eee");
});//telefono

$('#frmCreateCustomer #cus_AnotherPhone').on('keypress', function () {
    $('#frmCreateCustomer #cus_AnotherPhone').css("border-color", "#eee");
});//otro telefono

$('#frmCreateCustomer #cus_RTN').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});

$('#frmCreateCustomer #cus_Phone').keyup(function (e) {
    //console.log("Funciona la validacion de letras fuera del if ");
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
        //console.log("Funciona la validacion de letras dentro del if ");
    }
});

$('#frmCreateCustomer #cus_AnotherPhone').keyup(function (e) {
    //console.log("Funciona la validacion de letras fuera del if ");
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
        //console.log("Funciona la validacion de letras dentro del if ");
    }
});

$('#frmCreateCustomer #messageCre4').on('select2:select', function () {
    $('#frmCreateCustomer #messageCre4 span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#frmCreateCustomer #messageCre3').on('select2:select', function () {
    $('#frmCreateCustomer #messageCre3 span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#frmCreateCustomer #messageCre').on('select2:select', function () {
    $('#frmCreateCustomer #messageCre span.select2-selection.select2-selection--single').css("border-color", "#eee");
});


$('#frmCreateCustomer #messageCre2').on('select2:select', function () {
    $('#frmCreateCustomer #messageCre2 span.select2-selection.select2-selection--single').css("border-color", "#eee");
});


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}

function GetDropdowns() {
    $("#SelectDepartments2").select2();
    $("#SelectMunicipalities2").select2();
    $('#SelectUsuario2').select2();
    $('#tyCh_Id').select2();
}

