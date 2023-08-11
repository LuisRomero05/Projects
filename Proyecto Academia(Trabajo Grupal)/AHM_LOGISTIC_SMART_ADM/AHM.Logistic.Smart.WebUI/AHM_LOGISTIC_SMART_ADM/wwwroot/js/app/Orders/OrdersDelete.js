function DeleteOrders(id) {
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
            Swal.fire({
                width: '20%',
                height: '20%',
                text: "¿Estás seguro? Los cambios no podrán revertirse",
                icon: 'info',
                showCancelButton: true,
                cancelButtonText: 'Cancelar',
                confirmButtonColor: '#6c757d',
                cancelButtonColor: '#1275db',
                confirmButtonText: 'Sí'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "DELETE",
                        url: BaseUrl + "/Orders/Delete?Id=" + id + "&Mod=" + TempUserDefault,
                    }).done(function (data) {
                        iziToast.success({
                            message: '¡El registro se ha eliminado exitosamente!'
                        });
                        //NotificationSuccess(data.success);
                        setTimeout(function () {
                            location.assign(BaseUrl + "/orders/index");
                        }, 1500)
                    }).fail(function (data) {
                        NotificationSuccess(false);
                    });
                }
            })
        }
    })
}


