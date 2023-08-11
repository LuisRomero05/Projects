var Stock = [];
var Productos = [];
var Update = [];
function DeleteCotizations(id) {
    Swal.fire({
        width: '20%',
        height: '20%',
        text: "¿Estás seguro de eliminar este registro?",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#001f52',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Aceptar'

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "GET",
                url: BaseUrl + "/Quote/DetailsList?Id=" + id,
            }).done(function (data, index) {
                data.data.forEach(function (item) {
                    Productos.push({ id: item.pro_Id, canti: item.code_Cantidad });
                });
                $.ajax({
                    type: "GET",
                    url: BaseUrl + "/Products/ProductsList",
                }).done(function (data, index) {
                    data.data.forEach(function (item) {
                        Stock.push({ id: item.pro_Id, sto: item.pro_Stock })
                    });
                    Productos.forEach(function (pre) {
                        Stock.forEach(function (pro) {
                            if (pre.id == pro.id) {
                                Update.push({ proid: pre.id, nStock: pre.canti + pro.sto })
                            };
                        })
                    });
                    Update.forEach(function (pre) {
                        var InsertStock = {
                            "pro_Id": 0,
                            "pro_Stock": parseInt(pre.nStock),
                            "pro_IdUserModified": 1
                        };
                        $.ajax({
                            type: "POST",
                            url: BaseUrl + "/quote/UpdateStock?Id=" + parseInt(pre.proid),
                            data: InsertStock,
                            dataType: "json",
                        })
                    });
                    $.ajax({
                        type: "DELETE",
                        url: BaseUrl + "/Cotizations/Delete?Id=" + id + "&Mod=" + TempUserDefault,
                    }).done(function (data) {
                        iziToast.success({
                            message: '¡El registro se ha eliminado exitosamente!'
                        });

                        //NotificationSuccess(data.success);
                        setTimeout(function () {
                            location.assign(BaseUrl + "/quote/index");
                        }, 1500)
                    }).fail(function (data) {
                        NotificationSuccess(false);
                    });
                });
            });                    
        }
    })
}


    


