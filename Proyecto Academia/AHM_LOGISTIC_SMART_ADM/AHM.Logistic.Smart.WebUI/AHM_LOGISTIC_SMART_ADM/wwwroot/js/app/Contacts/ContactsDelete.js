function DeleteContacts(id) {
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
                url: BaseUrl + "/Contacts/Delete?Id=" + id + "&Mod=" + TempUserDefault,
            }).done(function (data) {
                Swal.fire({
                    width: '20%',
                    height: '20%',
                    title: '¡Eliminado!',
                    text: 'Registro eliminado exitosamente',
                    icon: 'success',
                    showConfirmButton: false,
                }); setTimeout(function () {
                    location.reload();
                }, 1500)
            }).fail(function (data) {
                NotificationDelete(false);
            });
        }
    })
}