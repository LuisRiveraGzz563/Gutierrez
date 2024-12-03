let ListaUsuarios = [];
const tableBody = document.querySelector(".data-table-p tbody");
let UsuarioAdd = document.getElementById("AgregarUser").addEventListener("click", function () {
    window.location.replace("/Admin/Usuario/AgregarUsuario");
});
async function filtro() {
    let busqueda = document.querySelector('.search-input').value;
    let usuarios = ListaUsuarios.filter(x =>
        x.rfc.toLowerCase().includes(busqueda.toLowerCase()) ||
        x.nombre.toLowerCase().includes(busqueda.toLowerCase()) ||
        x.proveedor.toLowerCase().includes(busqueda.toLowerCase())
    )
    rellenarUsuarios(usuarios);
}

async function rellenarUsuarios(usuarios) {
    // Limpiar el contenido existente de la tabla
    tableBody.innerHTML = "";
    // Renderizar los datos en la tabla
    usuarios.forEach((usuario) => {
        const row = document.createElement("tr");

        // Determinar el texto y la clase del botón según el estatus
        const estatusTexto = usuario.estatus ? "DESBLOQUEAR" : "BLOQUEAR";
        const claseBoton = usuario.estatus ? "desbloquear" : "bloquear";

        row.innerHTML = `
            <td>${usuario.rfc || "N/A"}</td>
            <td>${usuario.proveedor || "N/A"}</td>
            <td>${usuario.nombre}</td>
            <td>
                <button class="toggle-document ${claseBoton}">${estatusTexto}</button>
            </td>
            <td>
                <button class="delete-btn" data-id="${usuario.id}">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </td>
        `;

        tableBody.appendChild(row);
    });
}


document.addEventListener("DOMContentLoaded", async () => {
    // Función para llenar la tabla
    const fillTable = async () => {
        try {
            const response = await fetch("https://gutierrez.labsystec.net/api/Usuarios");
            if (!response.ok) {
                throw new Error("Error al obtener los datos de la API");
            }
            const usuarios = await response.json();
            ListaUsuarios = usuarios;
            rellenarUsuarios(usuarios);
            addEventListeners();
        } catch (error) {
            console.error("Error al llenar la tabla:", error);
        }
    };

    // Función para agregar event listeners a los botones de acción
    const addEventListeners = () => {
        document.querySelectorAll(".delete-btn").forEach((button) => {
            button.addEventListener("click", async (e) => {
                const userId = e.target.closest("button").dataset.id;
                if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
                    try {
                        const response = await fetch(`https://gutierrez.labsystec.net/api/Usuarios/Eliminar/${userId}`,
                            {
                                method: "DELETE"
                            });

                        if (response.ok) {
                            alert("Usuario eliminado exitosamente");
                            fillTable(); // Refrescar la tabla
                        } else {
                            throw new Error("Error al eliminar el usuario");
                        }
                    } catch (error) {
                        console.error("Error al eliminar el usuario:", error);
                        alert("No se pudo eliminar el usuario");
                    }
                }
            });
        });

    };

    fillTable();
});