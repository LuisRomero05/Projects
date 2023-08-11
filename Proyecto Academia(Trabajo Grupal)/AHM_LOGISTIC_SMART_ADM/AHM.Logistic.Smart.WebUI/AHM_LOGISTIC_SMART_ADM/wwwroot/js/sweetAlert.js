function showAlert() {
    Swal.fire({
        width: '20%',
        height: '20%',
        text: "¿Estás seguro de eliminar este registro?",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                width: '17%',
                height: '15%',
                title: '¡Eliminado!',
                text: '¡Registro eliminado correctamente!',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            })
        }
    })
}
