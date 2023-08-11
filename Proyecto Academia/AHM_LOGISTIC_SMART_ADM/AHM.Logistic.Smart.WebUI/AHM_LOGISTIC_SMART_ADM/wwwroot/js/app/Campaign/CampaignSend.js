function SendCampaigns(id) {
    Swal.fire({
        width: '20%',
        height: '20%',
        text: "¿Estás seguro que deseas enviar esta campaña?",
        icon: 'info',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#6c757d',
        cancelButtonColor: '#001f52',
        confirmButtonText: 'Aceptar'

    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: "GET",
                url: BaseUrl + "/Campaign/Send?Id=" + id,
                success: function (data) {
                    var campaign = [
                        { name: "cam_Id", value: data.data.cam_Id },
                        { name: "cus_Id", value: null },
                    ];

                    $.ajax({
                        type: "POST",
                        url: BaseUrl + "/Campaign/SendCampaign",
                        data: campaign,
                    }).done(function (data) {
                        if (data.success == false && data.id == 0 && data.data == null && data.type == 3) {
                            iziToast.warning({
                                message: 'No existe el registro',
                                displayMode: 'replace'
                            });
                        }
                        if (data.success == true && data.id == 0 && data.data != null && data.type == 0)
                        {
                            Swal.fire({
                                width: '20%',
                                height: '20%',
                                title: '¡Enviado!',
                                text: '¡Campaña enviada correctamente!',
                                icon: 'success',
                                showConfirmButton: false,
                            });
                            setTimeout(function () {
                                location.reload();
                            }, 1500)
                        }
                    });
                }
            });
        }
    });
}