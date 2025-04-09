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

        public PruebaLecturaClientes pruebaLectura;

        // POST: MenuInicio/Registro
        [HttpPost("Registro")]
        public ActionResult Deposito([FromBody] ClienteModel data)
        {
            try
            {
                // Lógica para procesar el pago
                PruebaEscrituraClientes escrituraClientes = new PruebaEscrituraClientes();

                escrituraClientes.Ejecutar(data);

                // Supongamos que la operación fue exitosa
                var response = new { success = true }; // Respuesta de éxito
                return Ok(response);  // Enviar respuesta al frontend con la propiedad success
            }
            catch (Exception ex)
            {
                // Si ocurre un error, enviar una respuesta con success: false
                var response = new { success = false, message = ex.Message };
                return BadRequest(response);  // Puedes usar BadRequest para manejar errores
            }

        }

        // POST: MenuInicio/Login
        [HttpPost("Login")]
        public ActionResult Deposito([FromBody] LoginDataInputModel data)
        {
            PruebaLecturaClientes lector = new PruebaLecturaClientes();
            ClienteModel? cliente = lector.BuscarPorUsuario(data.usuario);

            if (cliente != null)
            {
                

                var response = new { success = true , usuario_actual = cliente}; // Respuesta de éxito
                return Ok(response);  // Enviar respuesta al frontend con la propiedad success
            }
            else
            {
                var response = new { success = false, message = "Error" };
                return BadRequest(response);  // Puedes usar BadRequest para manejar errores
            }

        }

    }
}
