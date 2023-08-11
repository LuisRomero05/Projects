$(document).ready(function () {
    ReporteDinamico();
    $('#div-Fecha').show();
});
let name;

function ReporteDinamico() {
    var accion = "pdf";
    var fechainicio = $("#fechaInicio").val();
    var fechafinaliza = $("#fechaFinal").val();

    if (fechainicio == "" && fechafinaliza == "") {
        fechainicio = '1950-01-01';
        fechafinaliza = '2050-01-02';
    }
    var data = {
        fechainicio: fechainicio,
        fechafinaliza: fechafinaliza,
        accion: accion
    };


    var params = jQuery.param(data);
/*    console.log("parametros", params);*/
    $('#divFrame').show();
    $("#frmReporte").show();
    if ($('#frmReporte').is(':visible')) {
        $('#divCarga').css('display', 'block');
    }
    $("#frmReporte")
        .attr("src", "https://localhost:44369/CampaignGeneral/ByMonth?" + params).show();
}

//$('#TipoReporte').change(function () {
//    var seleccionado = $("#TipoReporte option:selected").val();
//    TipoReporte(seleccionado);
//});

function TipoReporte(valor = '') {
    switch (valor) {
        case 'ByCustomer':
            name = valor;
            $('#div-Fecha').hide();
            //$("#frmReporte").attr("src", "");

            break;
        case 'LastMonth':
            name = valor;
            $('#div-Fecha').hide();
            //$("#frmReporte").attr("src", "");
            break;
    }
}

function LimpiarFechas() {
    document.getElementById('fechaInicio').value = '';
    document.getElementById('fechaFinal').value = '';
}
