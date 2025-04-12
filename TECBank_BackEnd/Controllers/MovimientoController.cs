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
                nuevo_pago.Monto = data.Monto;


                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();

                CuentaModel cuenta_emisora = jasonLectura.BuscarCuentaPorNumero(data.NumeroDeCuenta);

                CuentaModel cuenta_para_editar = new CuentaModel();

                if (cuenta_emisora.Monto >= data.Monto)
                {

                    TarjetaModel tarjeta_a_modificar = jasonLectura.BuscarTarjetaPorNumero(nuevo_pago.Numero_de_Tarjeta);

                    int nuevo_monto = tarjeta_a_modificar.SaldoDisponible + data.Monto;

                    TarjetaModel tarjeta_modificada = new TarjetaModel();

                    tarjeta_modificada.SaldoDisponible = nuevo_monto;


                    jasonEditar.EditarTarjeta(nuevo_pago.Numero_de_Tarjeta, tarjeta_modificada);

                    JasonEscritura jasonEscritura = new JasonEscritura();
                    jasonEscritura.GuardarPago(nuevo_pago);


                    var response = new { success = true, message = "El pago se hizo con exito" };

                    cuenta_para_editar.Monto = cuenta_emisora.Monto - data.Monto;

                    jasonEditar.EditarCuenta(cuenta_emisora.NumeroDeCuenta, cuenta_para_editar);

                    // Lógica para obtener datos
                    return Ok(response);

                }
                else {
                    var response = new { success = false, message = "No hay fondos suficientes en la cuenta" };
                    return Ok(response);
                }

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
        public ActionResult Transferencia([FromBody] TransferenciaDataInputModel data)
        {
            try {

                TransferenciaModel nuevo_pago = new TransferenciaModel();
                Random random = new Random();
                string id = random.Next(10_000_000, 100_000_000).ToString(); // Entre 10,000,000 y 99,999,999

                // Asignacion de valores
                nuevo_pago.Nombre = data.Nombre;
                nuevo_pago.Apellido1 = data.Apellido1;
                nuevo_pago.Apellido2 = data.Apellido2;
                nuevo_pago.ID = id;
                nuevo_pago.Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                nuevo_pago.Cuenta_Emisora = data.Cuenta_Emisora;
                nuevo_pago.Cuenta_Receptora = data.Cuenta_Receptora;
                nuevo_pago.Moneda = data.Moneda;
                nuevo_pago.Monto = data.Monto;

                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();

                CuentaModel cuenta_emisora = jasonLectura.BuscarCuentaPorNumero(data.Cuenta_Emisora);

                CuentaModel cuenta_receptora = jasonLectura.BuscarCuentaPorNumero(data.Cuenta_Receptora);

                CuentaModel cuenta_para_editar = new CuentaModel();

                if (cuenta_emisora.Monto >= data.Monto)
                {
                    cuenta_para_editar.Monto = cuenta_receptora.Monto + data.Monto;

                    jasonEditar.EditarCuenta(cuenta_receptora.NumeroDeCuenta, cuenta_para_editar);

                    cuenta_para_editar.Monto = cuenta_emisora.Monto - data.Monto;

                    jasonEditar.EditarCuenta(cuenta_emisora.NumeroDeCuenta, cuenta_para_editar);

                    var response = new { success = true, message = "Transferencia realizada con exito" };
                    return Ok(response);

                }
                else {
                    var response = new { success = false, message = "No hay fondos suficientes para realizar la transferencia" };
                    return Ok(response);
                }
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
