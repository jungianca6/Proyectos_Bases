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

        // POST: MenuInicio/Registro
        [HttpGet("Login")]
        public ActionResult Deposito([FromBody] LoginDataInputModel data)
        {

            ClienteModel cliente = new ClienteModel();




            // Lógica para obtener datos
            return Ok(cliente);
        }

    }
}
