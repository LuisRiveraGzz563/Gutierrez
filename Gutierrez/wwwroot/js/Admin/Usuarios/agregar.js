
// Función para enviar datos del formulario
async function enviarFormulario(event) {
    event.preventDefault(); // Evitar recargar la página

    // Recopilar datos del formulario
    const usuario = document.getElementById("usuario").value;
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirm-password").value;
    const registroRepse = document.getElementById("registro-repse").value;
    const proveedor = document.getElementById("proveedor").value; // Este es el IdRol
    const email = document.getElementById("email").value;
    const telefono = document.getElementById("telefono").value;
    const contacto = document.getElementById("contacto").value;
    const regimen = document.querySelector('input[name="contacto"]').value; // Corregido para tipo de régimen

    // Validación básica
    if (!usuario || !password || !confirmPassword || !registroRepse || !proveedor || !email || !telefono || !contacto || !regimen) {
        alert("Por favor, completa todos los campos obligatorios.");
        return;
    }

    if (password !== confirmPassword) {
        alert("Las contraseñas no coinciden.");
        return;
    }

    // Asegúrate de que `proveedor` sea un número o el tipo esperado para `IdRol`
    const idRol = parseInt(proveedor, 10); // Asegura que `IdRol` sea un número

    // Crear objeto para enviar con los datos necesarios
    const datos = {
        IdRol: idRol, // Asegúrate de enviar el valor adecuado para IdRol
        Correo: email,
        Contraseña: password,  // El nombre de la propiedad debe ser 'Contraseña'
        Nombre: usuario,       // Asegúrate de que 'Nombre' es el campo correcto para el nombre de usuario
    };

    try {
        console.log("Entró"); // Log para confirmar que se está intentando consumir la API

        // Realizar el fetch para agregar el usuario
        const response = await fetch("https://gutierrez.labsystec.net/api/documentos/Agregar", {
            method: "POST",
            body: formData,
        });

        // Manejo de la respuesta
        if (response.ok) {
            const result = await response.json();
            console.log("Respuesta de la API:", result); // Mostrar la respuesta
            alert("Formulario enviado correctamente.");
            // Redirigir o limpiar formulario si es necesario
        } else {
            const errorData = await response.json();
            console.log("Detalles del error:", errorData); // Mostrar detalles del error en la consola
            alert(`Error: ${errorData.message || "Algo salió mal"}`);
        }
    } catch (error) {
        console.error("Error al enviar el formulario:", error);
        alert("No se pudo enviar el formulario. Inténtalo nuevamente.");
    }
}

// Agregar eventos al cargar la página
document.addEventListener("DOMContentLoaded", () => {
    // Botón Agregar
    const btnAgregar = document.querySelector(".btn-add");
    if (btnAgregar) {
        btnAgregar.addEventListener("click", enviarFormulario);
    }

    // Mostrar/Ocultar contraseñas
    const toggleButtons = document.querySelectorAll(".toggle-password");
    toggleButtons.forEach((button) => {
        button.addEventListener("click", () => {
            const target = button.getAttribute("data-target");
            const input = document.getElementById(target);
            if (input.type === "password") {
                input.type = "text";
                button.classList.remove("fa-eye");
                button.classList.add("fa-eye-slash");
            } else {
                input.type = "password";
                button.classList.remove("fa-eye-slash");
                button.classList.add("fa-eye");
            }
        });
    });
});
