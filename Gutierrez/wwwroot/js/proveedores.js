// async function DescargarDatos() {
//     try {
//         // Realizar la solicitud a la API
//         const response = await fetch(`${ApiUrl_local}Proveedor/proveedores`);

//         // Verificar si la respuesta es exitosa
//         if (response.ok) {
//             // Convertir la respuesta a JSON
//             const proveedores = await response.json();

//             // Llamar a una función para actualizar la tabla con los datos
//             actualizarTabla(proveedores);
//         } else {
//             console.error("Error al obtener los datos:", response.statusText);
//         }
//     } catch (error) {
//         // Manejo de errores
//         console.error("Hubo un problema con la solicitud fetch:", error);
//     }
// }
// // Función para actualizar dinámicamente la tabla con los datos recibidos
// function actualizarTabla(proveedores) {
//     const tbody = document.querySelector("tbody"); // Seleccionar el cuerpo de la tabla
//     // Limpiar contenido previo del tbody
//     tbody.innerHTML = "";
//     // Iterar sobre los proveedores y crear filas para la tabla
//     proveedores.forEach(proveedor => {
//         const row = document.createElement("tr");
//         row.innerHTML = `
//             <td>${proveedor.id}</td>
//             <td><a id="DetalleProveedor" class="link">${proveedor.nombre}</a></td>
//             <td><span class="repse-indicator-${proveedor.indicador}"></span></td>
//             <td>${proveedor.fecha}</td>
//         `;
//         tbody.appendChild(row);
//     });
// }
// // Llamar a la función para obtener los datos y mostrar en la tabla
// DescargarDatos();

async function obtenerProveedores() {
    //LLAMAR A LA API CON FETCH
    try {
        //const response = await fetch(`${ApiUrl_local}Proveedor/proveedores`);
        const response = await fetch(ApiUrl_local+'proveedor'); // Cambia la URL según tu entorno
        if (response.ok) {
            const proveedores = await response.json(); // Convertir la respuesta en JSON
            mostrarProveedores(proveedores); // Llamar a la función para mostrar los datos
            console.log("entro");
        } else {
            console.error('Error al obtener proveedores:', response.statusText);
            console.log("noo entro");
        }
    } catch (error) {
        console.error('Hubo un problema con la solicitud fetch:', error);
    }
}
//MOSTRAR DATOS
function mostrarProveedores(proveedores) {
    const tbody = document.querySelector('tbody'); // Selecciona el cuerpo de la tabla
    tbody.innerHTML = ''; // Limpia el contenido previo
    console.log(proveedores)
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


let Proovedor = document.getElementById("DetalleProveedor").addEventListener("click", function () {
    window.location.replace("/Admin/DetalleProveedor");
});