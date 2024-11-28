async function obtenerProveedores() {
    //LLAMAR A LA API CON FETCH
    try {
        const response = await fetch('https://gutierrez.labsystec.net/api/Proveedor'); 
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
async function verDetalles(proveedor)
{
    localStorage.setItem('nombre', proveedor.nombre);
    window.location.replace('Proveedor/DetalleProveedor');
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
                <td><a class="link">${proveedor.nombre}</a></td>
                <td><span class="repse-indicator-${proveedor.estado === 'Activo' ? 'green' : 'red'}"></span></td>
                <td>${new Date(proveedor.ultimaFechaModificacion).toLocaleDateString()}</td>
           `;
        let clon = tr.cloneNode(true);
        clon.querySelector(".link").onclick = () => verDetalles(proveedor);
        tbody.appendChild(clon);
    });
}

//LLAMAR A LA FUNCION
document.addEventListener('DOMContentLoaded', obtenerProveedores);