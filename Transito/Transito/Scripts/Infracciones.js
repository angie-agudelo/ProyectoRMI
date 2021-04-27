function validateform() {
    var id_vehiculo = document.crearInfraccion.id_vehiculo.value;
    var fecha = document.crearInfraccion.fecha.value;
    var accionador = document.crearInfraccion.accionador.value;

    if (id_vehiculo == null || id_vehiculo == "") {
        alert("El vehículo es requerido");
        return false;
    } else if (fecha == null || fecha == "") {
        alert("La Fecha infracción es requerida");
        return false;
    }
    else if (accionador == null || accionador == "") {
        alert("El accionador es requerido");
        return false;
    }
    document.getElementById("frmcrearinfraccion").submit();
}