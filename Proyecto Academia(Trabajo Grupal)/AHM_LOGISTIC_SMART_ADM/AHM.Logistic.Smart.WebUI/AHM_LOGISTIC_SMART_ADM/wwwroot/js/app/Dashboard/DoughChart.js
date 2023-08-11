var nombre = [];
var ordenes = [];
$(document).ready(function () {
    GetCustomers()
});

function GetCustomers() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Dashboard/GetCustomers",
    }).done(function (data, index) {

        data.data.forEach(function (item) {
            nombre.push(item.nombre);
            ordenes.push(item.cantidad);
        });
        DoughChart();
    })

}

function DoughChart() {
    var ctxD = document.getElementById("doughnutChart").getContext('2d');
    var myLineChart = new Chart(ctxD, {
        type: 'doughnut',
        data: {
            labels: nombre,
            datasets: [{
                data: ordenes,
                backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
                hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
            }]
        },
        options: {
            responsive: true
        }
    });

}