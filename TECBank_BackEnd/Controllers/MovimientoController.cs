using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientoController : ControllerBase
    {
        // POST: Movimiento/Pago
        [HttpPost("Pago")]
        public ActionResult Pago([FromBody] MovimientoModel data)
        {
            // Lógica para obtener datos
            return Ok();
        }

        // POST: Movimiento/Transferencia
        [HttpPost("Transferencia")]
        public ActionResult Transferencia([FromBody] MovimientoModel data)
        {
            // Lógica para obtener datos
            return Ok();
        }

        // POST: Movimiento/Retiro
        [HttpPost("Retiro")]
        public ActionResult Retiro([FromBody] MovimientoModel data)
        {
            // Lógica para obtener datos
            return Ok();
        }

        // POST: Movimiento/Deposito
        [HttpPost("Deposito")]
        public ActionResult Deposito([FromBody] MovimientoModel data)
        {
            // Lógica para obtener datos
            return Ok();
        }

    }
}
