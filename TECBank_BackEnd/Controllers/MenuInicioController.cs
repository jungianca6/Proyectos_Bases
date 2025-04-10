using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MenuInicioController : ControllerBase
    {
        // Instancia pública para pruebas de lectura de clientes (no se usa directamente en este controlador)
        public PruebaLecturaClientes pruebaLectura;

        // POST: MenuInicio/Registro
        [HttpPost("Registro")]
        public ActionResult Registro([FromBody] ClienteModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                PruebaEscrituraClientes escrituraClientes = new PruebaEscrituraClientes();

                // Ejecutar el método para registrar los datos del cliente
                escrituraClientes.Ejecutar(data);

                // Crear una respuesta indicando éxito
                var response = new { success = true };

                // Retornar la respuesta con código 200 (OK)
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, se construye una respuesta con success = false y el mensaje del error
                var response = new { success = false, message = ex.Message };

                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest(response);
            }
        }

        // POST: MenuInicio/Login
        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginDataInputModel data)
        {
            // Crear una instancia del lector de clientes
            PruebaLecturaClientes lector = new PruebaLecturaClientes();

            // Buscar el cliente por nombre de usuario
            ClienteModel? cliente = lector.BuscarPorUsuario(data.usuario);

            // Verificar si se encontró un cliente con ese usuario
            if (cliente != null)
            {
                // Si la contraseña no coincide, devolver mensaje de error
                if (cliente.Contrasena != data.contrasena)
                {
                    var response = new { success = false, message = "Contraseña incorrecta" };
                    return Ok(response);
                }
                else
                {
                    // Si la contraseña es correcta, devolver el cliente como usuario actual
                    var response = new { success = true, usuario_actual = cliente };
                    return Ok(response);
                }
            }
            else
            {
                // Si el usuario no existe, devolver mensaje de error
                var response = new { success = false, message = "El Usuario no existe" };
                return Ok(response);
            }
        }
    }
}
