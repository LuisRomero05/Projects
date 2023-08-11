function DeleteCustomers(id) {
    Swal.fire({
        width: '20%',
        height: '20%',
        text: "¿Estás seguro de eliminar este registro?",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#6c757d',
        cancelButtonColor: '#001f52',
        confirmButtonText: 'Aceptar'

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: BaseUrl + "/Customers/Delete?Id=" + id + "&Mod=" + UserId,
            }).done(function (data) {
                var result = true;
                result = NotificationDelete(data.success, "No se puede eliminar ya que el registro se encuentra en uso.", data.id, data.data, data.type)
                if (result) {
                    Swal.fire({
                        width: '20%',
                        height: '20%',
                        title: '¡Eliminado!',
                        text: '¡Registro eliminado correctamente!',
                        icon: 'success',
                        showConfirmButton: false,
                    });
                    setTimeout(function () {
                        location.reload();
                    }, 1500)
                }
            });
        }
    })
}