document.addEventListener("DOMContentLoaded", () => {
    const tableBody = document.querySelector(".data-table-d tbody");

    // Función para agregar una nueva fila
    const addRow = (nombreProveedor, meses) => {
        const newRow = document.createElement("tr");

        // Columna del nombre del proveedor
        const nombreCell = document.createElement("td");
        nombreCell.textContent = nombreProveedor;
        newRow.appendChild(nombreCell);

        // Columnas para cada mes
        meses.forEach((mes) => {
            const monthCell = document.createElement("td");
            const link = document.createElement("a");
            link.href = "visualizador.html"; // Enlace a cambiar según sea necesario
            link.className = "status-item";

            const icon = document.createElement("i");
            icon.className =     "fas fa-file-alt";
            link.appendChild(icon);

            const text = document.createTextNode(` ${mes}`);
            link.appendChild(text);

            monthCell.appendChild(link);
            newRow.appendChild(monthCell);
        });

        // Agregar la fila a la tabla
        tableBody.appendChild(newRow);
    };

    // Ejemplo: Agregar una nueva fila
    const mesesPendientes = Array(12).fill("Pendiente"); // 12 meses con "Pendiente"
    addRow("Nuevo proveedor", mesesPendientes);
    console.log("XD");
});
document.getElementById("add-row-btn").addEventListener("click", () => {
    const mesesPendientes = Array(12).fill("Pendiente");
    addRow("Proveedor Manual", mesesPendientes);
});
