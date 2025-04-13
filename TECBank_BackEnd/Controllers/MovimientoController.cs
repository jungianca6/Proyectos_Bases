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
        [HttpPost("Pago")]
        public ActionResult Pago([FromBody] PagoDataInputModel data)
        {
            try
            {
                PagoModel nuevo_pago = new PagoModel
                {
                    Nombre = data.Nombre,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    ID = GenerarID(),
                    Fecha = ObtenerFechaActual(),
                    Numero_de_Tarjeta = data.Numero_de_Tarjeta,
                    Cuenta_Emisora = data.NumeroDeCuenta,
                    Moneda = data.Moneda,
                    Monto = data.Monto
                };

                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();
                JasonEscritura jasonEscritura = new JasonEscritura();

                CuentaModel cuenta_emisora = jasonLectura.BuscarCuentaPorNumero(data.NumeroDeCuenta);

                if (cuenta_emisora.Monto >= data.Monto)
                {
                    TarjetaModel tarjeta = jasonLectura.BuscarTarjetaPorNumero(data.Numero_de_Tarjeta);
                    tarjeta.SaldoDisponible += data.Monto;
                    jasonEditar.EditarTarjeta(data.Numero_de_Tarjeta, tarjeta);

                    cuenta_emisora.Monto -= data.Monto;
                    jasonEditar.EditarCuenta(cuenta_emisora.NumeroDeCuenta, cuenta_emisora);

                    jasonEscritura.GuardarPago(nuevo_pago);

                    return Ok(new { success = true, message = "El pago se hizo con exito" });
                }
                else
                {
                    return Ok(new { success = false, message = "No hay fondos suficientes en la cuenta" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: Movimiento/Transferencia
        [HttpPost("Transferencia")]
        public ActionResult Transferencia([FromBody] TransferenciaDataInputModel data)
        {
            try
            {
                TransferenciaModel nueva_transferencia = new TransferenciaModel
                {
                    Nombre = data.Nombre,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    ID = GenerarID(),
                    Fecha = ObtenerFechaActual(),
                    Cuenta_Emisora = data.Cuenta_Emisora,
                    Cuenta_Receptora = data.Cuenta_Receptora,
                    Moneda = data.Moneda,
                    Monto = data.Monto
                };

                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();
                JasonEscritura jasonEscritura = new JasonEscritura();

                var cuenta_emisora = jasonLectura.BuscarCuentaPorNumero(data.Cuenta_Emisora);
                var cuenta_receptora = jasonLectura.BuscarCuentaPorNumero(data.Cuenta_Receptora);

                if (cuenta_emisora == null)
                    return Ok(new { success = false, message = "La cuenta origen no existe" });

                if (cuenta_receptora == null)
                    return Ok(new { success = false, message = "La cuenta destino no existe" });

                if (cuenta_emisora.Monto < data.Monto)
                    return Ok(new { success = false, message = "No hay fondos suficientes para realizar la transferencia" });

                cuenta_receptora.Monto += data.Monto;
                jasonEditar.EditarCuenta(cuenta_receptora.NumeroDeCuenta, cuenta_receptora);

                cuenta_emisora.Monto -= data.Monto;
                jasonEditar.EditarCuenta(cuenta_emisora.NumeroDeCuenta, cuenta_emisora);

                jasonEscritura.GuardarTransferencia(nueva_transferencia);

                return Ok(new { success = true, message = "Transferencia realizada con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: Movimiento/Retiro
        [HttpPost("Retiro")]
        public ActionResult Retiro([FromBody] RetiroDataInputModel data)
        {
            try
            {
                RetiroModel nuevo_retiro = new RetiroModel
                {
                    Nombre = data.Nombre,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    ID = GenerarID(),
                    Fecha = ObtenerFechaActual(),
                    CuentaARetirar = data.CuentaARetirar,
                    Moneda = data.Moneda,
                    Monto = data.Monto
                };

                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();
                JasonEscritura jasonEscritura = new JasonEscritura();

                CuentaModel cuenta_a_retirar = jasonLectura.BuscarCuentaPorNumero(data.CuentaARetirar);

                if (cuenta_a_retirar.Monto >= data.Monto)
                {
                    cuenta_a_retirar.Monto -= data.Monto;
                    jasonEditar.EditarCuenta(cuenta_a_retirar.NumeroDeCuenta, cuenta_a_retirar);
                    jasonEscritura.GuardarRetiro(nuevo_retiro);

                    return Ok(new { success = true, message = "Retiro hecho con exito" });
                }
                else
                {
                    return Ok(new { success = false, message = "No se realizo el retiro, fondos insuficientes" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: Movimiento/ListadoDeMovimientos
        [HttpPost("ListadoDeMovimientos")]
        public ActionResult ListadoDeMovimientos([FromBody] ListadoDeMovmientosDataInputModel data)
        {
            try
            {
                JasonLectura jasonLectura = new JasonLectura();

                var retiros = jasonLectura.LeerRetiros("NumeroDeCuenta", data.NumeroDeCuenta);
                var transferencias = jasonLectura.LeerTransferencias("NumeroDeCuenta", data.NumeroDeCuenta);
                var pagos = jasonLectura.LeerPagos("NumeroDeCuenta", data.NumeroDeCuenta);

                return Ok(new { success = true, retiros, pagos, transferencias });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: Movimiento/ListadoDeCompras
        [HttpPost("ListadoDeCompras")]
        public ActionResult ListadoDeCompras([FromBody] ListadoDeComprasDataInputModel data)
        {
            try
            {
                JasonLectura jasonLectura = new JasonLectura();

                var retiros = jasonLectura.LeerRetirosPorFechaYCuenta(data.fechaInicio, data.fechaFinal, data.numeroDeCuenta);
                var transferencias = jasonLectura.LeerTransferenciasPorFechaYCuenta(data.fechaInicio, data.fechaFinal, data.numeroDeCuenta);
                var pagos = jasonLectura.LeerPagosPorFechaYCuenta(data.fechaInicio, data.fechaFinal, data.numeroDeCuenta);

                return Ok(new { success = true, retiros, pagos, transferencias });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}