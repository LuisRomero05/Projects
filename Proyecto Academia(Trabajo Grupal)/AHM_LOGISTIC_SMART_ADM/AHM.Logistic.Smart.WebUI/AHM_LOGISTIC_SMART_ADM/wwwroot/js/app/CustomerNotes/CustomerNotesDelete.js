function DeleteCustomerNote(id) {
    Swal.fire({
        width: '20%',
        height: '20%',
        text: "¿Estás seguro de eliminar este registro?",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Aceptar',
        confirmButtonColor: '#6c757d',
        cancelButtonColor: '#001f52',

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: BaseUrl + "/CustomersNotes/Delete?Id=" + id,
            }).done(function (data) {
                location.reload();
            });
        }
    })
}