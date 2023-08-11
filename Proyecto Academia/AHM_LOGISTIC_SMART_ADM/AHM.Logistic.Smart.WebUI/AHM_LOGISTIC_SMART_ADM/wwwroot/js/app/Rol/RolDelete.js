function DeleteRoles(id) {
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
                url: BaseUrl + "/Rol/Delete?Id=" + id + "&Mod=" + UserId,
            }).done(function (data) {
                NotificationDelete(data.success);
                setTimeout(function () {
                    location.reload();
                }, 1500)
            }).fail(function (data) {
                NotificationDelete(false);
            });
        }
    })
}