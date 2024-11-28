document.addEventListener("DOMContentLoaded", () => {
    const tableBody = document.querySelector(".data-table-p tbody");

    // Función para llenar la tabla
    const fillTable = async () => {
        try {
            const response = await fetch(ApiUrlRelease + 'api/Documentos');
            console.log(await response.json());
            if (!response.ok) {
                throw new Error("Error al obtener los datos de la API");
            }
            else {
                console.log("entro");
            }

            const proveedores = await response.json();

            // Limpiar el contenido existente de la tabla
            tableBody.innerHTML = "";

            // Renderizar los datos en la tabla
            proveedores.forEach((proveedor, index) => {
                const row = document.createElement("tr");

                row.innerHTML = `
                <td>${proveedor.proveedorDocumento.rfc}</td>
                <td>${proveedor.proveedorDocumento.nombre}</td>
                <td>
                    <input type="radio" name="status_${index}" id="status_${index}_1" value="1" onclick="toggleStatus(1, ${index})" />
                    <input type="radio" name="status_${index}" id="status_${index}_0" value="0" onclick="toggleStatus(0, ${index})" />
                    <label class="status-label" for="status_${index}_1">✘</label>
                </td>
                <td>
                    <button class="accept-button">Aceptar</button>
                    <button class="reject-button">Rechazar</button>
                </td>
                    `;
                tableBody.appendChild(row);
            });
        } catch (error) {
            console.error("Error al llenar la tabla:", error);
        }
    };
    fillTable();
});
