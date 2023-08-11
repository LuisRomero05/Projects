function NotificationMessage(success, action, id, data, type) {
    if (success == true && id == 0 && data != null && type == DataType.Success) {
        iziToast.success({
            message: action,
            displayMode: 'replace'
        });
        return true;
    }
    if (success == true && id == 0 && data == null && type == DataType.Success) {
        iziToast.success({
            message: action,
            displayMode: 'replace'
        });
        return true;
    }
    if (success == false && id == 0 && data == null && type == DataType.Error) {
        iziToast.warning({
            title: 'Advertencia',
            message: action,
            displayMode: 'replace'
        });
        return false;
    }    
    if (success == false && id == 0 && data == null && type == DataType.Warning) {
        iziToast.warning({
            title: 'Advertencia',
            message: action,
            displayMode: 'replace'
        });
        return false;
    }
    if (success == false && id == 0 && data == null && type == DataType.GatewayTimeout) {
        iziToast.Error({
            title: 'Advertencia',
            message: action,
            displayMode: 'replace'
        });
        return false;
    }
}

function MessageDeleteDepen(nombre) {
    Swal.fire({
        width: '20%',
        height: '20%',
        title: '¡Advertencia!',
        text: '¡El registro de ' + nombre + ' tiene dependecia con otras vistas!',
        icon: 'warning',
        showConfirmButton: true,
        showCancelButton: false,
        confirmButtonText: 'Acepta',
        confirmButtonColor: '#001f52',
    });
}

function NotificationDelete(success, action, id, data, type) {
    if (success == true && id == 0 && data != null && type == 0) {        
        return true;
    }
    if (success == false && id == 0 && data == null && type == 3) {
        iziToast.warning({
            title: 'Advertencia',
            message: action,
            displayMode: 'replace'
        });
        return false;
    }
    if (success == true && id == 0 && data == null && type == 0) {
        iziToast.warning({
            title: 'Advertencia',
            message: action,
            displayMode: 'replace'
        });
        return false;
    }
    if (success == false && id == 0 && data == null && type == 2) {
        iziToast.warning({
            title: 'Advertencia',
            message: action,
            displayMode: 'replace'
        });
        return false;
    }
}