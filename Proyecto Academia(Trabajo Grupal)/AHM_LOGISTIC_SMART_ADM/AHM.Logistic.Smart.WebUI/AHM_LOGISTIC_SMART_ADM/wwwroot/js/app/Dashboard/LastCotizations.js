var tablacoti = "";
$(document).ready(function () {
    GetLastCotizations();
});
function GetLastCotizations() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Dashboard/LastCotizations",
        data: {
            UserId
        },
    }).done(function (data, index) {

        data.data.forEach(function (item) {
        
            if (item.sta_Id == 1)
                tablacoti += "<tr><td>" + item.cus_Name + "</td><td>" + item.cus_Phone + "</td><td>" + item.cus_Email + "</td><td>" + getFormattedDate(item.cot_DateCreate) + "</td><td><span class='badge badge-warning badge-pill'>" + item.sta_Description +"</span></td></tr>"
            if (item.sta_Id == 3)
                tablacoti += "<tr><td>" + item.cus_Name + "</td><td>" + item.cus_Phone + "</td><td>" + item.cus_Email + "</td><td>" + getFormattedDate(item.cot_DateCreate) + "</td><td><span class='badge badge-success badge-pill'>" + item.sta_Description +"</span></td></tr>"
        });
        document.getElementById("tablacoti").innerHTML = tablacoti;
    })

}
function getFormattedDate(date) {
    var d = new Date(date);
    let year = d.getFullYear();
    let month = (1 + d.getMonth()).toString().padStart(2, '0');
    let day = d.getDate().toString().padStart(2, '0');

    return year + '-' + month + '-' + day;
}

