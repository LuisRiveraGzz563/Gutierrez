﻿let ListaProveedores = [];
const tableBody = document.querySelector(".data-table-p tbody");
async function filtro() {
    let nombre = document.getElementById('lblnombre').value;
    let rfc = document.getElementById('lblrfc').value;
    let proveedores = ListaProveedores.filter(proveedor =>
        proveedor.nombre.toLowerCase().includes(nombre.toLowerCase())
        && proveedor.rfc.toLowerCase().includes(rfc.toLowerCase())
    );
    llenarTabla(proveedores);
}

async function llenarTabla(proveedores) {
    console.log(tablebody);
    // Limpiar el contenido existente de la tabla
    tableBody.innerHTML = "";
    // Renderizar los datos en la tabla
    proveedores.forEach((proveedor, index) => {
        const row = document.createElement("tr");
        row.innerHTML = `
                <td>${proveedor.rfc}</td>
                <td>${proveedor.nombre}</td>
                <td>
                    <input type="radio" name="status_${proveedor.estado}" id="status_${proveedor.estado}_1" value="1" onclick="toggleStatus(1, ${proveedor.estado})" />
                    <input type="radio" name="status_${proveedor.estado}" id="status_${proveedor.estado}_0" value="0" onclick="toggleStatus(0, ${proveedor.estado})" />
                    <label class="status-label" for="status_${proveedor.estado}_1">✘</label>
                </td>
                <td>
                    <button class="accept-button">Aceptar</button>
                    <button class="reject-button">Rechazar</button>                                                               
                </td>                                                               
                    `;
        let clon = row.cloneNode(true);
        if (proveedor.estado == 1) {
            clon.querySelector('.status-label').innerHTML = '✔';
            clon.querySelector('.status-label').style.color = "forestGreen";
        }
        tableBody.appendChild(clon);
    });
}

document.addEventListener("DOMContentLoaded", async () => {

    try {
        const response = await fetch(ApiUrlRelease + 'api/Proveedor/Solicitudes');
        if (!response.ok) {
            throw new Error("Error al obtener los datos de la API");
        }
        else {
            console.log("entro");
        }
        const proveedores = await response.json();
        ListaProveedores = proveedores;
        llenarTabla(proveedores);
    } catch (error) {
        console.error("Error al llenar la tabla:", error);
    }
});
