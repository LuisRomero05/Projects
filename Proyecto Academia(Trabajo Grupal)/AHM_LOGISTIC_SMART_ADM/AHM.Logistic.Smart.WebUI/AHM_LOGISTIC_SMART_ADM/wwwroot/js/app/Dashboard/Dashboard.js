$(document).ready(function () {
    GetMetrics();
});

function GetMetrics() {
    //$.ajax({
    //    type: "GET",
    //    url: BaseUrl + "/Dashboard/DashBoardGetMetrics?Id=" + 1,
    //}).done(function (data, index) {
    //    console.log("si entra");
    //    console.log("data",data);
    //}).fail(function () {
    //    //mostrar alerta en caso de error
    //    console.log("Error al cargar los Clientes");
    //});
    $.ajax({
        url: BaseUrl + "/Dashboard/DashBoardGetMetrics",
        method: "GET",
        data: {
            UserId
        },
        success: function (response, status, xhr) {
            $("#coti").append(response.data.cotizations);
            $("#venta").append(response.data.sales);
            $("#cln").append(response.data.customers);
            $("#campa").append(response.data.campaigns);
            console.log("response", response.data);
        }
    });


}
