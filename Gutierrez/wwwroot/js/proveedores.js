async function obtenerProveedores() {
    //LLAMAR A LA API CON FETCH
    try {
        const response = await fetch(ApiUrlRelease + 'api/Proveedor'); 
        if (response.ok) {
            let proveedores = await response.json(); // Convertir la respuesta en JSON
            mostrarProveedores(proveedores); // Llamar a la función para mostrar los datos
        } else {
            console.error('Error al obtener proveedores:', response.statusText);
        }
    } catch (error) {
        console.error('Hubo un problema con la solicitud fetch:', error);
    }
}
//MOSTRAR DATOS
async function mostrarProveedores(proveedores)
{
    const tbody = document.querySelector('tbody'); // Selecciona el cuerpo de la tabla
    tbody.innerHTML = ''; // Limpia el contenido previo
    proveedores.forEach(proveedor => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
                <td>${proveedor.rfc}</td>
                <td><a href="#" class="link">${proveedor.nombre}</a></td>
                <td><span class="repse-indicator-${proveedor.estado === 'Activo' ? 'green' : 'red'}"></span></td>
                <td>${new Date(proveedor.ultimaFechaModificacion).toLocaleDateString()}</td>
           `;
         
        tbody.appendChild(tr);
    });
}

//LLAMAR A LA FUNCION
document.addEventListener('DOMContentLoaded', obtenerProveedores);