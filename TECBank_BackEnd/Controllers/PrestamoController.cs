using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrestamoController : ControllerBase
    {
        // Método auxiliar para generar ID único
        private string GenerarID()
        {
            Random random = new Random();
            return random.Next(10_000_000, 100_000_000).ToString();
        }

        // Método auxiliar para obtener la fecha y hora actual formateada
        private string ObtenerFechaActual()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        // POST: Movimiento/Pago
        [HttpPost("AgregarPrestamo")]
        public ActionResult Pago([FromBody] AgregarPrestamoDataInputModel data)
        {
            try
            {
                PrestamoModel nuevo_prestamo = new PrestamoModel
                {
                    Monto_Original = data.Monto_Original,
                    Saldo_Pendiente = data.Monto_Original,
                    Cedula_Cliete = data.Cedula_Cliente,
                    Tasa_De_Interes = data.Tasa_De_Interes,
                    FechaVencimiento = data.FechaVencimiento,
                    ID_Prestamos = GenerarID() 
                };

                JasonEscritura jasonEscritura = new JasonEscritura();

                jasonEscritura.GuardarPrestamo(nuevo_prestamo);

                return Ok(new { success = true, message = "El prestamo se ha creado con exito" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

       
    }
}