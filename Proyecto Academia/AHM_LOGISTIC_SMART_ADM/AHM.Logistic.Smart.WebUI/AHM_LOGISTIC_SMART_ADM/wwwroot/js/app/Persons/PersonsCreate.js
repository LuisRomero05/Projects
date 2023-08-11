var done = false;
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
                    NewOption += "<option  value=" + item.dep_Id + ">" + item.dep_Description + "</option>";
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
    $("#SelectMunicipalities").attr('disabled', false);
    GetSubMunicipalitiesListCreate();
});

function GetSubMunicipalitiesListCreate() {
    var value = $("#SelectDepartments").val();
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/MuniList",
    }).done(function (data, index) {
        $("#SelectMunicipalities").empty();
        var NewOption = "";
        if (data.data != null) {
            data.data.forEach(function (item) {
                if (item.dep_Id == value) {
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
var per_Phone = document.querySelector("#per_Phone");

//Paramtros sobre las cualidades del input type tel
var per_PhoneIti = window.intlTelInput(per_Phone, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    /*    separateDialCode: true,*/
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});

//El numero de area al selecionar el pais, sera guardado en la variable
var perPhone = "";
per_Phone.addEventListener("countrychange", function () {
    var countryData = per_PhoneIti.getSelectedCountryData();
    perPhone = "+" + countryData.dialCode + " ";
});

//el numero de area al cargar
var countryData = per_PhoneIti.getSelectedCountryData();
var areaInicioper = "+" + countryData.dialCode + " ";







$('#frmCreatePersons').on('submit', function (e) {
    LimpiarSpanMessa();
    var result = ValidateFrmPersons();
    if (result == true) {
        var telefonoFinal = "";
        if (perPhone === "") {
            telefonoFinal = areaInicioper + per_Phone.value;
        } else {
            telefonoFinal = perPhone + per_Phone.value;
        }
        var persons = [
            { name: "per_Identidad", value: $('#frmCreatePersons #per_Identidad').val().trim() },
            { name: "per_Firstname", value: $('#frmCreatePersons #per_Firstname').val().trim() },
            { name: "per_Secondname", value: $('#frmCreatePersons #per_Secondname').val().trim() },
            { name: "per_LastNames", value: $('#frmCreatePersons #per_LastNames').val().trim() },
            { name: "per_BirthDate", value: $('#frmCreatePersons #per_BirthDate').val() },
            { name: "per_Sex", value: $('#frmCreatePersons #per_Sex').val() },
            { name: "per_Email", value: $('#frmCreatePersons #per_Email').val().trim() },
            { name: "per_Phone", value: telefonoFinal },
            { name: "per_Direccion", value: $('#frmCreatePersons #per_Direccion').val().trim() },
            { name: "dep_Id", value: $('#frmCreatePersons #SelectDepartments').val() },
            { name: "mun_Id", value: $('#frmCreatePersons #SelectMunicipalities').val() },
            { name: "per_Esciv", value: $('#frmCreatePersons #per_Esciv').val() },
            { name: "per_IdUserCreate", value: UserId },
            { name: "per_IdUserModified", value: null },
        ];
        $.ajax({
            type: "POST",
            url: BaseUrl + "/persons/create",
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
    e.preventDefault();
    return false;

});

var telVali = false;
function talvalidad() {
    var iti = intlTelInput(input);
    var isValid = iti.isValidNumber();
    return isValid;
};

function ValidateFrmPersons() {
    LimpiarSpanMessa();
    var result = false;
    var count = 0;
    var per_Identidad = $('#frmCreatePersons #per_Identidad');
    var per_Firstname = $('#frmCreatePersons #per_Firstname');
    var per_LastNames = $('#frmCreatePersons #per_LastNames');
    var per_BirthDate = $('#frmCreatePersons #per_BirthDate');
    var per_Sex = $('#frmCreatePersons #per_Sex');
    var per_Email = $('#frmCreatePersons #per_Email');
    var per_Phone = $('#frmCreatePersons #per_Phone');
    var per_PhoneMess = $("#frmCreatePersons #per_PhoneMess");
    var per_Direccion = $('#frmCreatePersons #per_Direccion');
    var SelectDepartments = $('#frmCreatePersons #SelectDepartments');
    var SelectMunicipalities = $('#frmCreatePersons #SelectMunicipalities');
    var per_Esciv = $('#frmCreatePersons #per_Esciv');
    var caract = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);

    var divSexo = $('#frmCreatePersons #divSexo');
    var divEsta = $('#frmCreatePersons #divEsta');
    var divDepar = $('#frmCreatePersons #divDepar');
    var divMuni = $('#frmCreatePersons #divMuni');

    var borSexo = $('#frmCreatePersons #divSexo span.select2-selection.select2-selection--single');
    var borEsta = $('#frmCreatePersons #divEsta span.select2-selection.select2-selection--single');
    var borDepar = $('#frmCreatePersons #divDepar span.select2-selection.select2-selection--single');
    var borMuni = $('#frmCreatePersons #divMuni span.select2-selection.select2-selection--single');


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
        message = '<span style="color: red;" name="Mesas">No puede registrar si la persona posee mas de 125 años.</span>';
        per_BirthDate.after($(message).fadeToggle(3000));
        per_BirthDate.css("border-color", "red");
        count++;
    }
    if (inputdate > limitdateYoung) {
        message = '<span style="color: red;" name="Mesas">No puede registrar si la persona es menor de edad.</span>';
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
    result = telMessagesError(per_Phone, 'Teléfono', per_PhoneIti, per_PhoneMess);
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

$('#frmCreatePersons #per_Identidad').on('keypress', function () {
    $('#frmCreatePersons #per_Identidad').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Firstname').on('keypress', function () {
    $('#frmCreatePersons #per_Firstname').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Secondname').on('keypress', function () {
    $('#frmCreatePersons #per_Secondname').css("border-color", "#eee");
});
$('#frmCreatePersons #per_LastNames').on('keypress', function () {
    $('#frmCreatePersons #per_LastNames').css("border-color", "#eee");
});
$('#frmCreatePersons #per_BirthDate').on('change', function () {
    $('#frmCreatePersons #per_BirthDate').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Email').on('keypress', function () {
    $('#frmCreatePersons #per_Email').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Phone').on('keypress', function () {
    $('#frmCreatePersons #per_Phone').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Direccion').on('keypress', function () {
    $('#frmCreatePersons #per_Direccion').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Sex').on('select2:select', function () {
    $('#frmCreatePersons #divSexo span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmCreatePersons #SelectDepartments').on('select2:select', function () {
    $('#frmCreatePersons #divDepar span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmCreatePersons #SelectMunicipalities').on('select2:select', function () {
    $('#frmCreatePersons #divMuni span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmCreatePersons #per_Esciv').on('select2:select', function () {
    $('#frmCreatePersons #divEsta span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#frmCreatePersons #per_Identidad').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});

$('#frmCreatePersons #per_Phone').keyup(function (e) {
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