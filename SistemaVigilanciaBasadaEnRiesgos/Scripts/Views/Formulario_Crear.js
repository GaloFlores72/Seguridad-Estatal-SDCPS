//var table = $('#tablaLV').DataTable(); // Inicializa DataTables
$(document).ready(function () {

    $('#OrganizacionID').on('change', function () {
        var _oid = $(this).val();
        $.get($.MisUrls.url._ObtieneOrganizacionPorOid, { Id: _oid }, function (data) {
            if (data) {
                $('#oOrganizaciones_OrganizacionID').val(data.OrganizacionID);
                $('#oOrganizacion_Nombre').val(data.Nombre);
                $('#oOrganizacion_Direccion').val(data.Direccion);
                $('#oOrganizacion_GerenteResponsable').val(data.GerenteResponsable);
                $('#oOrganizacion_NCertificadoOMA').val(data.NCertificadoOMA);
                $('#oOrganizacion_Correo').val(data.Correo);
                $('#oOrganizacion_Telefono').val(data.Telefono);
            }
        });

    });

    $(".color-selector").change(function () {
        // Obtener el valor seleccionado
        var estadoId = $(this).val();
        var valores = "";
        $(this).parents("tr").find("td").each(function () {
            valores += $(this).html() + "\n";
        });

        let fila = $(this).closest("tr"); // Obtiene la fila padre
        let respuestaId = fila.find("td:eq(0)").text(); // Primera columna (DetalleRespuestaID)
        let orientacionId = fila.find("td:eq(1)").text(); // Segunda columna (RespuestaOrientacionID)       
        //Ajax Post
        
       
    });


})

function modalConstataciones(_id) {
    if (_id > 0) {
        $('#modalConstataciones').modal('show');
    }
}

function modalConstatacionNuevo() {
    $('#modalConstataciones').modal('hide');
    $('#modalConstatacion').modal('show');
}

function salirConstatacion() {
    $('#modalConstataciones').modal('show');
    $('#modalConstatacion').modal('hide');
}

function cambia_Color(elemento) {

    var selectedColor = elemento.val();
    alert(selectedColor);
    elemento.style.backgroundColor = '#ffcccc'; // Color cuando el mouse está sobre la celda
}