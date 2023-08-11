var done = false;
$(document).ready(function () {
    FulltbCustomers();
});

function FulltbCustomers() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Customers/CustomersList",
    }).done(function (data) {
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                setTimeout(function () {
                    location.assign(BaseUrl + "/campaign/index");
                }, 1500)
            }
        }
        else {
            data.data.forEach(function (item, index, array) {
                if (item.cus_receive_email == true) {
                    $('#customerSend #table-content').append(
                        '<tr><td>' + item.cus_Id + '</td>' +
                        '<td>' + item.cus_AssignedUser + '</td>' +
                        '<td>' + item.cus_Name + '</td>' +
                        '<td>' + item.tyCh_Description + '</td>' +
                        '<td>' + item.cus_RTN + '</td>' +
                        '<td>' + item.dep_Description + '</td>' +
                        '<td>' + item.mun_Description + '</td>' +
                        '<td>' + item.cus_Email + '</td>' +
                        '<td>' + item.cus_Phone + '</td>' +
                        '<td>' + item.cus_AnotherPhone + '</td></tr>'
                    );
                }
            });
        }
    }).fail(function (data) {
        NotificationMessage(data.success, "", data.id, data.data, data.type);
        setTimeout(function () {
            location.assign(BaseUrl + "/campaign/index");
        }, 1500)
    });
}
