
var id = $("#municipio").val();
var depart = null;
var user = null;
var canal = null;

$(document).ready(function () {
    GetSubDepartmentsListCreate2();
    GetSubMunicipalitiesListCreate2();
});

function GetSubDepartmentsListCreate2(depart) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data, index) {
        $("#SelectDepartments2").empty();
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";

        data.data.forEach(function (item) {
            if (item.dep_Id == depart) {
                NewOption += "<option  value=" + item.dep_Id + " selected>" + item.dep_Description + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.dep_Id + ">" + item.dep_Description + "</option>";
            }
        });
        $("#SelectDepartments2").append(NewOption);

    }).fail(function () {
        console.log("Error al cargar los Departamentos");
    });
}

$("#SelectDepartments2").on('change', function () {
    $("#SelectMunicipalities2").attr('disabled', false);
    id = 0;
    GetSubMunicipalitiesListCreate2();
});

function GetSubMunicipalitiesListCreate2() {
    var value = $("#SelectDepartments2").val();
    if (value == null) {
        $.ajax({
            type: "GET",
            url: BaseUrl + "/Muni/MuniList",
        }).done(function (data, index) {
            data.data.forEach(function (item) {
                if (item.mun_Id == id) {
                    depart = item.dep_Id
                    GetSubDepartmentsListCreate2(depart)
                }
            });
        });
    }
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/MuniList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectMunicipalities2").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
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

        //Agregar las opciones al dropdownlist
        $("#SelectMunicipalities2").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los Municipios");
    });
}

