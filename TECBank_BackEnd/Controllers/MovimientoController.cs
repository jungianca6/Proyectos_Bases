using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientoController : ControllerBase
    {
        // POST: Movimiento/Pago
        [HttpPost("Pago")]
        public ActionResult Pago([FromBody] PagoDataInputModel data)
        {
            PagoModel nuevo_pago = new PagoModel();
            nuevo_pago.Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            nuevo_pago.Numero_de_Tarjeta = data.Numero_de_Tarjeta;


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
