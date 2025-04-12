using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

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
            try {
                PagoModel nuevo_pago = new PagoModel();
                Random random = new Random();
                string id = random.Next(10_000_000, 100_000_000).ToString(); // Entre 10,000,000 y 99,999,999

                // Asignacion de valores
                nuevo_pago.Nombre = data.Nombre;
                nuevo_pago.Apellido1 = data.Apellido1;
                nuevo_pago.Apellido2 = data.Apellido2;
                nuevo_pago.ID = id;
                nuevo_pago.Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                nuevo_pago.Numero_de_Tarjeta = data.Numero_de_Tarjeta;
                nuevo_pago.Cuenta_Emisora = data.NumeroDeCuenta;
                nuevo_pago.Numero_de_Tarjeta = data.Numero_de_Tarjeta;
                nuevo_pago.Moneda = data.Moneda;


                JasonLectura jasonLectura = new JasonLectura();



                TarjetaModel tarjeta_a_modificar = jasonLectura.BuscarTarjetaPorNumero(nuevo_pago.Numero_de_Tarjeta);

                int nuevo_monto = tarjeta_a_modificar.SaldoDisponible + data.Monto;

                TarjetaModel tarjeta_modificada = new TarjetaModel();

                tarjeta_modificada.SaldoDisponible = nuevo_monto;

                JasonEditar jasonEditar = new JasonEditar();
                jasonEditar.EditarTarjeta(nuevo_pago.Numero_de_Tarjeta, tarjeta_modificada);

                JasonEscritura jasonEscritura = new JasonEscritura();
                jasonEscritura.GuardarPago(nuevo_pago);

                var response = new { success = true, message = "El pago se hizo con exito" };


                // Lógica para obtener datos
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

        // POST: Movimiento/Transferencia
        [HttpPost("Transferencia")]
        public ActionResult Transferencia([FromBody] MovimientoModel data)
        {
            try { 




            }
            catch (Exception ex)
            {
                // Si ocurre un error, se construye una respuesta con success = false y el mensaje del error
                var response = new { success = false, message = ex.Message };

                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest(response);
            }

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
