$(document).ready(function () {
    GetSubDepartmentsListCreate2();
    ReporteDinamico();
    console.log("entro al js de reporte de cust");
    //$('#div-Fecha').show();
    //$('#div-Cliente').show();
});
let name;

//function MostrarParamsFecha() {
//    //    $('#div-Id').hide();
//    $('#div-Fecha').show();
//    $('#div-Depto').show();

//    //    $('#div-Genaral').hide();
//    //    $('#btnExportar').hide();
//    GetSubDepartmentsListCreate2();
//    console.log("entro al js de reporte de cust");
//    //    $('#btnExportarFecha').hide();
//    //    $('#btnExportarGeneral').hide();
//}




function ReporteDinamico() {
    var accion = "pdf";
    var seleccionado = $("#SelectDepartments2 option:selected").val();
    var fechainicio = $("#fechaInicio").val();
    var fechafinaliza = $("#fechaFinal").val();
    if (seleccionado == null) {
        seleccionado = 0;
    }
    if (fechainicio == "" && fechafinaliza == "") {
        fechainicio = '1950-01-01';
        fechafinaliza = '1950-01-02';
    }
    var data = {
        Id: seleccionado,
        fechainicio: fechainicio,
        fechatermina: fechafinaliza,
        accion: accion
    };

    var params = jQuery.param(data);
    $('#divFrame').show();
    $("#frmReporte").show();
    if ($('#frmReporte').is(':visible')) {
        $('#divCarga').css('display', 'block');
    }
    $("#frmReporte")
        .attr("src", "https://localhost:44369/CustomersReport/CustomersReportDynamic?" + params).show();
}


//function Mostrar() {
//    var accion = "pdf";
//    var data = {
//        accion: accion
//    };
//    var params = jQuery.param(data);
//    $('#divFrame').show();
//    $("#frmReporte").show();
//    if ($('#frmReporte').is(':visible')) {
//        $('#divCarga').css('display', 'block');
//    }
//    $("#frmReporte")
//        .attr("src", "ehttps://localhost:44369/CustomersReport/LastMonth?" + params).show();
//}

//function MostrarPorDepto() {
//    var seleccionado = $("#SelectDepartments2 option:selected").val();
//    var accion = "pdf";
//    var data = {
//        Id: seleccionado,
//        accion: accion
//    };
//    var params = jQuery.param(data);
//    $('#divFrame').show();
//    $("#frmReporte").show();
//    if ($('#frmReporte').is(':visible')) {
//        $('#divCarga').css('display', 'block');
//    }
//    $("#frmReporte")
//        .attr("src", "https://localhost:44369/CustomersReport/ByDepto?" + params).show();

//}

//function MostrarPorFecha() {
//    var fechainicio = $("#fechaInicio").val();
//    var fechafinaliza = $("#fechaFinal").val();
//    var accion = "pdf";
//    var data = {
//        fechainicio: fechainicio,
//        fechafinaliza: fechafinaliza,
//        accion: accion
//    };
//    if (fechainicio != "" && fechafinaliza != "") {
//        $('#validarExistencia').text("");
//        if (fechainicio < fechafinaliza) {
//            $('#validarExistencia').text("");
//    var params = jQuery.param(data);
//    $('#divFrame').show();
//    $("#frmReporte").show();
//    if ($('#frmReporte').is(':visible')) {
//        $('#divCarga').css('display', 'block');
//    }
//    $("#frmReporte")
//        .attr("src", "https://localhost:44369/CustomersReport/ByDate?" + params).show();
//        }
//        else {
//            $('#validarExistencia').text('¡La fecha de inicio es superior a la de finalizar!');
//        }
//    } else {
//        $('#validarExistencia').text('¡Todos los campos son necesarios!');
//    }
//}


$('#TipoReporte').change(function () {
    var seleccionado = $("#TipoReporte option:selected").val();
    TipoReporte(seleccionado);
});

function TipoReporte(valor = '') {
    switch (valor) {
        case 'ByDepto':
            name = valor;
            $('#div-Fecha').hide();
            $('#div-Depto').show();
            GetSubDepartmentsListCreate2();
            //$("#frmReporte").attr("src", "");

            break;
        case 'LastMonth':
            name = valor;
            $('#div-Fecha').hide();
            $('#div-Depto').hide();
            //$("#frmReporte").attr("src", "");
            Mostrar();
            break;
        case 'ByDate':
            name = valor;
            $('#div-Depto').hide();
            MostrarParamsFecha();
            break;
    }
}

function GetSubDepartmentsListCreate2() {//funcion que trae los departamentos
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectDepartments22").empty();
        var id = $("#departamento").val();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";

        data.data.forEach(function (item) {
            if (item.dep_Id == id) {
                NewOption += "<option  value=" + item.dep_Id + " selected>" + item.dep_Description + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.dep_Id + ">" + item.dep_Description + "</option>";
            }
        });
        //Agregar las opciones al dropdownlist
        $("#SelectDepartments2").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los Departamentos");
    });
}

//function Mostrar() {
//    //$('#div-Id').hide();
//    $('#div-Genaral').show();
//    var usu_Id = $("#usu_Id").val();
//    var rol_Id = $("#rol_Id").val();
//    var accion = "pdf";
//    var data = {
//        usu_Id: usu_Id,
//        rol_Id: rol_Id,
//        accion: accion
//    };
//    var params = jQuery.param(data);
//    $('#divFrame').show();
//    $("#frmReporte").show();
//    if ($('#frmReporte').is(':visible')) {
//        $('#divCarga').css('display', 'block');
//    }
//    $('#btnExportar').show();
//    $("#frmReporte")
//        .attr("src", "/Reports/DescargarReporte" + name + "?" + params + "#toolbar=0")
//        .show();
//    $('#btnExportarFecha').hide();
//    $('#btnExportarGeneral').show();
//}


function LimpiarDropwDown() {
    document.getElementById('fechaInicio').value = '';
    document.getElementById('fechaFinal').value = '';

    document.getElementById('SelectDepartments2').value = 0;
}