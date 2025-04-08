using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("MenuInicio/[controller]")]
    public class MenuInicioController : ControllerBase
    {
        // POST: MenuInicio/Registro
        [HttpPost("Registro")]
        public ActionResult Deposito([FromBody] ClienteModel data)
        {

            // Lógica para obtener datos
            return Ok();
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
