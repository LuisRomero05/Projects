$(document).ready(function () {
    $('#DropDownPaisMC').select2({
        dropdownParent: $('#CreateMunicipalities')
    });
    $('#DropDownDepartmentsMC').select2({
        dropdownParent: $('#CreateMunicipalities')
    });
});

//click que abre el modal de crear
$("#OpenModalCreateMunicipalitie").click(function () {

    //llamar las funciones que cargan los dropdownlist
    GetSubPaisListCreateMuni();
    GetSubDepartmentsListCreateMuni();
    cleanMuniciCreate();
    //mostrar el modal
    $('#CreateMunicipalities').modal('show');

    //$('#CrearEmpleado').modal('hide');
});

$("#CreateMunicipalitiesCancelar").click(function () {
    cleanMuniciCreate();
});
function cleanMuniciCreate() {
    $('#CreateMunicipalities #CodeMC').val("");
    $('#CreateMunicipalities #CodeMC').css("border-color", "#eee");
    $('#CreateMunicipalities #DescriptionMC').val("");
    $('#CreateMunicipalities #DescriptionMC').css("border-color", "#eee");
    $('#CreateMunicipalities #DropDownPaisMC').val(0);
    $('#CreateMunicipalities #DropDownPaisMC').css("border-color", "#eee");
    $('#CreateMunicipalities #DropDownDepartmentsMC').val(0);
    $('#CreateMunicipalities #DropDownDepartmentsMC').css("border-color", "#eee");
}

//click que confirma la creación de una persona
$("#CreateMunicipalities #CreateMunicipalitiesConfirmar").click(function (e) {
    e.preventDefault();
    e.stopImmediatePropagation();
    //crear el objeto con los valores seleccionadoss 
    var result = ValidateCreateMdlMunicipalies();
    if (result == true) {


        var data = [
            { name: "mun_Code", value: $("#CreateMunicipalities #CodeMC").val() },
            { name: "mun_Description", value: $("#CreateMunicipalities #DescriptionMC").val().trim() },
            { name: "dep_Id", value: $("#CreateMunicipalities #DropDownDepartmentsMC").val() },
            { name: "mun_IdUserCreate", value: UserId },
            { name: "mun_IdUserModifies", value: null },
        ];
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Muni/Create",
            data: data,
        }).done(function (data) {
            var result = true;
            result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            if (result) {
                setTimeout(function () {
                    location.assign(BaseUrl + "/Muni/index");
                }, 1500)
            }
        })


    }
});

function GetSubPaisListCreateMuni(deprt) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Countries/CountriesList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#DropDownPaisMC").empty();
        //variable que almacena las opciones
        var NewOption = '<option value=' + 0 + ' selected disabled=""> Por favor seleccione una opción... </option>';

        data.data.forEach(function (item) {
            //if (item.dep_Id == id) {
            //    NewOption += "<option value=" + item.dep_Id + " selected>" + item.dep_Description + "</option>";
            //}
            //else {
            NewOption += "<option value=" + item.cou_Id + ">" + item.cou_Description + "</option>";
            /*        }*/
        });

        //Agregar las opciones al dropdownlist
        $("#DropDownPaisMC").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPaisMC').after($(message));
    });
}

function GetSubDepartmentsListCreateMuni() {
    $("#DropDownDepartmentsMC").attr('disabled', false);
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#DropDownDepartmentsMC").empty();
        var idCon = $("#DropDownPaisMC").val();
        //variable que almacena las opciones
        var NewOption = '<option value=' + 0 + ' selected disabled=""> Por favor seleccione una opción... </option>';

        data.data.forEach(function (item) {
            if (item.cou_Id == idCon) {
                NewOption += "<option value=" + item.dep_Id + ">" + item.dep_Description + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#DropDownDepartmentsMC").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownDepartmentsMC').after($(message));
    });
}

function ValidateCreateMdlMunicipalies() {
    LimpiarSpanMessa();
    var result = false;
    var count = 0;

    var CodeMC = $('#CreateMunicipalities #CodeMC');
    var DescriptionMC = $('#CreateMunicipalities #DescriptionMC');
    var DropDownPaisMC = $('#CreateMunicipalities #DropDownPaisMC');
    var divMessaPais = $('#CreateMunicipalities #MessageCrePais');
    var el = $('#CreateMunicipalities #MessageCrePais span.select2-selection.select2-selection--single');
    var DropDownDepartmentsMC = $('#CreateMunicipalities #DropDownDepartmentsMC');
    var divMessaDepart = $('#CreateMunicipalities #MessageCreDeprt');
    var ell = $('#CreateMunicipalities #MessageCreDeprt span.select2-selection.select2-selection--single');

    result = MessagesError(CodeMC, null, 4, 'Codigo municipio');
    if (result == true) { count++; }

    result = MessagesError(DescriptionMC, null, 100, 'Municipio');
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownPaisMC, 'Pais', divMessaPais,el);
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownDepartmentsMC, 'Departamento', divMessaDepart,ell);
    if (result == true) { count++; }

    if (count == 4) {
        return result;
    }
    return false;
}

$('#CreateMunicipalities #CodeMC').on('keypress', function () {
    $('#CreateMunicipalities #CodeMC').css("border-color", "#eee");
});
$('#CreateMunicipalities #DescriptionMC').on('keypress', function () {
    $('#CreateMunicipalities #DescriptionMC').css("border-color", "#eee");
});
$('#CreateMunicipalities #DropDownPaisMC').on('select2:select', function () {
    GetSubDepartmentsListCreateMuni();
    $('#CreateMunicipalities #MessageCrePais span.select2-selection.select2-selection--single').css("border-color", "#eee");
    $("#DropDownDepartmentsMC").attr('disabled', false);
});
$('#CreateMunicipalities #DropDownDepartmentsMC').on('select2:select', function () {
    $('#CreateMunicipalities #MessageCreDeprt span.select2-selection.select2-selection--single').css("border-color", "#eee");
});


$('#CreateMunicipalities #CodeMC').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});
function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}