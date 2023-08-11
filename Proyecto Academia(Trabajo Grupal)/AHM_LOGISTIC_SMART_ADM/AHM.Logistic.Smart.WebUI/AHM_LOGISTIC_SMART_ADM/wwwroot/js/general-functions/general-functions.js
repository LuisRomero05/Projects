function logoutConfirm(permissions, Imageprofile, usu_Id) {
    Swal.fire({
        width: '20%',
        height: '20%',
        text: "¿Estás seguro que deseas cerrar sesión?",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#6c757d',
        cancelButtonColor: '#001f52',
        confirmButtonText: 'Aceptar'

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "GET",
                url: BaseUrl + "/User/Logout",
            }).done(function (data) {
                if (data = true) {
                    location.assign(BaseUrl + "/")
                }
            });
        }
    });
};
