var done = false;
var mun = $("#municipio").val();
$(document).ready(function () {
    GetDropdowns();
    GetSubDepartmentsListCreate();
    GetSubMunicipalitiesListCreate();
});

function GetSubDepartmentsListCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data, index) {
        var id = $("#departamento").val();

        var NewOption = "";
        if (data.data != null) {
            data.data.forEach(function (item) {
                if (item.dep_Id == id) {
                    NewOption += "<option  value=" + item.dep_Id + " selected>" + item.dep_Description + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.dep_Id + ">" + item.dep_Description + "</option>";
                }
            });
        }

        $("#SelectDepartments").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectDepartments').after($(message));
    });
}

$("#SelectDepartments").on('change', function () {
    /*$("#SelectMunicipalities").attr('disabled', false);*/
    mun = 0;
    GetSubMunicipalitiesListCreate();
});

function GetSubMunicipalitiesListCreate() {
    var value = $("#SelectDepartments").val();
    var dep = $("#departamento").val();
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/MuniList",
    }).done(function (data, index) {
        $("#SelectMunicipalities").empty();

        var NewOption = "";
        if (data.data != null) {
            data.data.forEach(function (item) {
                if (item.mun_Id == mun)
                    NewOption += "<option  value=" + item.mun_Id + "  selected>" + item.mun_Description + "</option>";
                else if (item.dep_Id == value || (value == null && item.dep_Id == dep)) {
                    NewOption += "<option  value=" + item.mun_Id + ">" + item.mun_Description + "</option>";
                }
            });
        }
        $("#SelectMunicipalities").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectMunicipalities').after($(message));
    });
}






//input que desea agregarles las cualidades del jQuery
var per_PhoneEd = document.querySelector("#per_Phone");

//Paramtros sobre las cualidades del input type tel
var per_PhoneItiEd = window.intlTelInput(per_Phone, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    /*    separateDialCode: true,*/
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});


// Ulilizada para elegir que area va enviar.
var ValidarTele = true;
//El numero de area al selecionar el pais, sera guardado en la variable
var perPhoneEd = "";
per_PhoneEd.addEventListener("countrychange", function () {
    var countryData = per_PhoneItiEd.getSelectedCountryData();
    ValidarTele = false;
    perPhoneEd = "+" + countryData.dialCode + " ";
});

//el numero de area al cargar
var countryDataEd = per_PhoneItiEd.getSelectedCountryData();
var areaInicioperEd = "+" + countryDataEd.dialCode + " ";









$('#frmEditPersons').on('submit', function (e) {
    LimpiarSpanMessa();
    var result = ValidateEditFrmPersons();
    if (result == true) {
        var telefonoFinal = "";
        if (ValidarTele === true) {
            telefonoFinal = areaInicioperEd + per_PhoneEd.value;
        } else {
            telefonoFinal = perPhoneEd + per_Phone.value;
        }
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
            if (result.isConfirmed) {
                var persons = [
                    { name: "per_Id", value: $('#frmEditPersons #per_Id').val() },
                    { name: "per_Identidad", value: $('#frmEditPersons #per_Identidad').val().trim() },
                    { name: "per_Firstname", value: $('#frmEditPersons #per_Firstname').val().trim() },
                    { name: "per_Secondname", value: $('#frmEditPersons #per_Secondname').val().trim() },
                    { name: "per_LastNames", value: $('#frmEditPersons #per_LastNames').val().trim() },
                    { name: "per_BirthDate", value: $('#frmEditPersons #per_BirthDate').val() },
                    { name: "per_Sex", value: $('#frmEditPersons #per_Sex').val() },
                    { name: "per_Email", value: $('#frmEditPersons #per_Email').val().trim() },
                    { name: "per_Phone", value: telefonoFinal },
                    { name: "per_Direccion", value: $('#frmEditPersons #per_Direccion').val().trim() },
                    { name: "dep_Id", value: $('#frmEditPersons #SelectDepartments').val() },
                    { name: "mun_Id", value: $('#frmEditPersons #SelectMunicipalities').val() },
                    { name: "per_Esciv", value: $('#frmEditPersons #per_Esciv').val() },
                    { name: "per_IdUserCreate", value: UserId },
                    { name: "per_IdUserModified", value: UserId },
                ];


                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/Persons/Edit",
                    data: persons,
                }).done(function (data) {
                    var result = true;
                    result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    if (result) {
                        setTimeout(function () {
                            location.assign(BaseUrl + "/persons/index");
                        }, 1500)
                    }
                });
            }
        })
    }
    e.preventDefault();
    return false;

});

function ValidateEditFrmPersons() {
    var result = true;
    var count = 0;
    var per_Identidad = $('#frmEditPersons #per_Identidad');
    var per_Firstname = $('#frmEditPersons #per_Firstname');
    var per_LastNames = $('#frmEditPersons #per_LastNames');
    var per_BirthDate = $('#frmEditPersons #per_BirthDate');
    var per_Sex = $('#frmEditPersons #per_Sex');
    var per_Email = $('#frmEditPersons #per_Email');
    var per_Phone = $('#frmEditPersons #per_Phone');
    var per_PhoneMess = $("#frmEditPersons #per_PhoneMessEd");
    var per_Direccion = $('#frmEditPersons #per_Direccion');
    var SelectDepartments = $('#frmEditPersons #SelectDepartments');
    var SelectMunicipalities = $('#frmEditPersons #SelectMunicipalities');
    var per_Esciv = $('#frmEditPersons #per_Esciv');
    var caract = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);

    var divSexo = $('#frmEditPersons #divSexo');
    var divEsta = $('#frmEditPersons #divEsta');
    var divDepar = $('#frmEditPersons #divDepar');
    var divMuni = $('#frmEditPersons #divMuni');

    var borSexo = $('#frmEditPersons #divSexo span.select2-selection.select2-selection--single');
    var borEsta = $('#frmEditPersons #divEsta span.select2-selection.select2-selection--single');
    var borDepar = $('#frmEditPersons #divDepar span.select2-selection.select2-selection--single');
    var borMuni = $('#frmEditPersons #divMuni span.select2-selection.select2-selection--single');

    var limitdateYoung = new Date();
    var limitdateOld = new Date();
    limitdateYoung = limitdateYoung.getFullYear() - 18;
    limitdateOld = limitdateOld.getFullYear() - 125;
    var inputdate = new Date(per_BirthDate.val());
    inputdate = inputdate.getFullYear();
    result = MessagesError(per_Identidad, 13, 13, 'Identidad');
    if (result == true) { count++; }
    result = MessagesError(per_Firstname, null, 20, 'Primer Nombre');
    if (result == true) { count++; }
    result = MessagesError(per_LastNames, null, 20, 'Apellidos');
    if (result == true) { count++; }
    if (inputdate < limitdateOld) {
        message = '<span style="color: red;">No puede registrar si la persona posee mas de 125 años.</span>';
        per_BirthDate.after($(message).fadeToggle(3000));
        per_BirthDate.css("border-color", "red");
        count++;
    }
    if (inputdate > limitdateYoung) {
        message = '<span style="color: red;">No puede registrar si la persona es menor de edad.</span>';
        per_BirthDate.after($(message).fadeToggle(3000));
        per_BirthDate.css("border-color", "red");
        count++;
    }
    result = MessagesError(per_BirthDate, null, null, 'Fecha Nacimiento');
    if (result == true) { count++; }
    result = MessageErrorDrop(per_Sex, 'Sexo', divSexo, borSexo);
    if (result == true) { count++; }

    if (caract.test(per_Email.val()) == false) {
        message = '<span style="color: red;" name="Mesas">El campo de email es inválido</span>';
        per_Email.after($(message).fadeToggle(3000));
        per_Email.css("border-color", "red");
        count++;
    }
    result = telMessagesError(per_Phone, 'Teléfono', per_PhoneItiEd, per_PhoneMess);
    if (result == true) { count++; }
    result = MessagesError(per_Direccion, null, 200, 'Dirección');
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectDepartments, 'Departamentos', divDepar, borEsta);
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectMunicipalities, 'Municipios', divMuni, borDepar);
    if (result == true) { count++; }
    result = MessageErrorDrop(per_Esciv, 'Estado Civil', divEsta, borMuni);
    if (result == true) { count++; }
    if (count == 10) {
        return result;
    }
    return false;
}

$('#frmEditPersons #per_Identidad').on('keypress', function () {
    $('#frmEditPersons #per_Identidad').css("border-color", "#eee");
});
$('#frmEditPersons #per_Firstname').on('keypress', function () {
    $('#frmEditPersons #per_Firstname').css("border-color", "#eee");
});
$('#frmEditPersons #per_Secondname').on('keypress', function () {
    $('#frmEditPersons #per_Secondname').css("border-color", "#eee");
});
$('#frmEditPersons #per_LastNames').on('keypress', function () {
    $('#frmEditPersons #per_LastNames').css("border-color", "#eee");
});
$('#frmEditPersons #per_BirthDate').on('change', function () {
    $('#frmEditPersons #per_BirthDate').css("border-color", "#eee");
});
$('#frmEditPersons #per_Email').on('keypress', function () {
    $('#frmEditPersons #per_Email').css("border-color", "#eee");
});
$('#frmEditPersons #per_Phone').on('keypress', function () {
    $('#frmEditPersons #per_Phone').css("border-color", "#eee");
});
$('#frmEditPersons #per_Direccion').on('keypress', function () {
    $('#frmEditPersons #per_Direccion').css("border-color", "#eee");
});
$('#frmEditPersons #per_Sex').on('select2:select', function () {
    $('#frmEditPersons #divSexo span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmEditPersons #SelectDepartments').on('select2:select', function () {
    $('#frmEditPersons #divDepar span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmEditPersons #SelectMunicipalities').on('select2:select', function () {
    $('#frmEditPersons #divMuni span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmEditPersons #per_Esciv').on('select2:select', function () {
    $('#frmEditPersons #divEsta span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#frmEditPersons #per_Identidad').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});

$('#frmEditPersons #per_Phone').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});

function GetDropdowns() {
    $("#SelectDepartments").select2();
    $("#SelectMunicipalities").select2();
    $('#per_Sex').select2();
    $('#per_Esciv').select2();
}