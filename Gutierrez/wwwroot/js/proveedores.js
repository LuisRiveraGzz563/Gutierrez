async function obtenerProveedores() {
    //LLAMAR A LA API CON FETCH
    try {
        //const response = await fetch(`${ApiUrl_local}Proveedor/proveedores`);
        const response = await fetch(ApiUrlRelease + 'api/Proveedor'); 
        console.log(response);
        if (response.ok) {
            let proveedores = await response.json(); // Convertir la respuesta en JSON
            console.log(proveedores);
            mostrarProveedores(proveedores); // Llamar a la función para mostrar los datos
            console.log("entro");
        } else {
            console.error('Error al obtener proveedores:', response.statusText);
            console.log("no entro");
        }
    } catch (error) {
        console.error('Hubo un problema con la solicitud fetch:', error);
    }
}
//MOSTRAR DATOS
function mostrarProveedores(proveedores)
{
    const tbody = document.querySelector('tbody'); // Selecciona el cuerpo de la tabla
    tbody.innerHTML = ''; // Limpia el contenido previo
    proveedores.forEach(proveedor => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
                <td>${proveedor.numRegistroRepse}</td>
                <td><a href="#" class="link">${proveedor.nombre}</a></td>
                <td><span class="repse-indicator-${proveedor.estado === 'Activo' ? 'green' : 'red'}"></span></td>
                <td>${new Date(proveedor.ultimaFechaModificacion).toLocaleDateString()}</td>
           `;
         
        tbody.appendChild(tr);
    });
}

//LLAMAR A LA FUNCION
document.addEventListener('DOMContentLoaded', obtenerProveedores);