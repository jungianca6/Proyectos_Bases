using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;
using TECBank_BackEnd.Utilities;

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
        [HttpPost("PagoTarjeta")]
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

                    return Ok(new { success = true, message = "El pago de la tarjeta se hizo con exito" });
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


        [HttpPost("PagoPrestamo")]
        public ActionResult PagoPrestamo([FromBody] PagoPrestamoDataInputModel data)
        {
            try
            {
                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();
                JasonEscritura jasonEscritura = new JasonEscritura();
                Jason json = new Jason(); // Se usa para leer calendarios

                CuentaModel cuenta_emisora = jasonLectura.BuscarCuentaPorNumero(data.NumeroDeCuenta);

                if (cuenta_emisora.Monto >= (int)data.Monto)
                {
                    DateTime fechaActual = DateTime.Now;

                    // Obtener el calendario de pagos del préstamo
                    var calendarios = json.LeerCalendarioPagos();
                    var calendario = calendarios.FirstOrDefault(c => c.ID_Prestamo == data.IdPrestamo);

                    if (calendario == null)
                        return BadRequest(new { success = false, message = "No se encontró el calendario de pagos." });

                    // Buscar la siguiente cuota NO pagada
                    var siguienteCuota = calendario.CuotasMensuales
                        .Where(cm => !cm.Pagado)
                        .OrderBy(cm => DateTime.ParseExact(cm.FechaPago, "dd/MM/yyyy", null))
                        .FirstOrDefault();

                    if (siguienteCuota == null)
                        return BadRequest(new { success = false, message = "Todas las cuotas ya han sido pagadas." });

                    DateTime fechaPagoCuota = DateTime.ParseExact(siguienteCuota.FechaPago, "dd/MM/yyyy", null);
                    decimal montoIngresado = data.Monto;
                    decimal montoCuota = siguienteCuota.MontoAPagar;
                    bool estaAtrasado = fechaActual > fechaPagoCuota;

                    if (montoIngresado < montoCuota && estaAtrasado)
                    {
                        return Ok(new
                        {
                            success = false,
                            message = $"⚠️ La cuota correspondiente al {siguienteCuota.Mes} {siguienteCuota.Anio} ya venció. Debe pagar el monto completo de ₡{montoCuota}."
                        });
                    }

                    decimal montoFinal = montoIngresado;
                    bool seAplicoInteres = false;

                    if (estaAtrasado && montoIngresado >= montoCuota)
                    {
                        decimal interes = 0.10m;
                        montoFinal += montoFinal * interes;
                        seAplicoInteres = true;
                    }

                    int montoFinalEntero = (int)Math.Round(montoFinal);

                    // Crear y guardar el pago
                    PagoPrestamoModel nuevo_pago = new PagoPrestamoModel
                    {
                        Nombre = data.Nombre,
                        Apellido1 = data.Apellido1,
                        Apellido2 = data.Apellido2,
                        ID = GenerarID(),
                        Fecha = fechaActual.ToString("dd/MM/yyyy"),
                        IdPrestamo = data.IdPrestamo,
                        Moneda = data.Moneda,
                        Monto = montoFinalEntero,
                        CuentaEmisora = data.NumeroDeCuenta
                    };

                    jasonEscritura.GuardarPagoPrestamo(nuevo_pago);

                    // Actualizar préstamo
                    PrestamoModel prestamo = jasonLectura.BuscarPrestamoPorId(data.IdPrestamo);
                    prestamo.Saldo_Pendiente -= montoFinalEntero;
                    jasonEditar.EditarPrestamo(data.IdPrestamo, prestamo);

                    // Actualizar cuenta
                    cuenta_emisora.Monto -= montoFinalEntero;
                    jasonEditar.EditarCuenta(cuenta_emisora.NumeroDeCuenta, cuenta_emisora);

                    // 🔄 Marcar la cuota pagada si se pagó completo
                    for (int i = 0; i < calendario.CuotasMensuales.Count; i++)
                    {
                        var cuota = calendario.CuotasMensuales[i];
                        if (!cuota.Pagado)
                        {
                            if (montoIngresado >= cuota.MontoAPagar || !estaAtrasado)
                            {
                                cuota.Pagado = true;
                                calendario.CuotasMensuales[i] = cuota; // Aseguramos la actualización
                            }
                            break;
                        }
                    }


                    calendario.SaldoPendiente -= montoFinalEntero;
                    jasonEditar.EditarCalendarioPago(calendario.ID_Prestamo, calendario);

                    return Ok(new
                    {
                        success = true,
                        message = "✅ El pago se hizo con éxito.",
                        interesAplicado = seAplicoInteres,
                        montoFinalCobrado = montoFinalEntero
                    });
                }
                else
                {
                    return Ok(new { success = false, message = "❌ No hay fondos suficientes en la cuenta." });
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
        // POST: Movimiento/Transferencia
        [HttpPost("TransferenciaAdmin")]
        public ActionResult TransferenciaAdmin([FromBody] TransferenciaDataInputModel data)
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
                    Cuenta_Emisora = "0",
                    Cuenta_Receptora = data.Cuenta_Receptora,
                    Moneda = data.Moneda,
                    Monto = data.Monto
                };

                JasonLectura jasonLectura = new JasonLectura();
                JasonEditar jasonEditar = new JasonEditar();
                JasonEscritura jasonEscritura = new JasonEscritura();

                var cuenta_emisora = jasonLectura.BuscarCuentaPorNumero(data.Cuenta_Emisora);
                var cuenta_receptora = jasonLectura.BuscarCuentaPorNumero(data.Cuenta_Receptora);

                if (cuenta_receptora == null)
                    return Ok(new { success = false, message = "La cuenta destino no existe" });


                cuenta_receptora.Monto += data.Monto;
                jasonEditar.EditarCuenta(cuenta_receptora.NumeroDeCuenta, cuenta_receptora);


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

                var retiros = jasonLectura.LeerRetiros("CuentaARetirar", data.NumeroDeCuenta);
                var transferencias = jasonLectura.LeerTransferencias("Cuenta_Emisora", data.NumeroDeCuenta);
                var pagos_tarjetas = jasonLectura.LeerPagos("Cuenta_Emisora", data.NumeroDeCuenta);
                var pagos_prestamos = jasonLectura.LeerPagosPrestamo("CuentaEmisora", data.NumeroDeCuenta);

                return Ok(new { success = true, retiros, pagos_tarjetas, pagos_prestamos, transferencias });
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
                var pagos_tarjetas = jasonLectura.LeerPagosPorFechaYCuenta(data.fechaInicio, data.fechaFinal, data.numeroDeCuenta);
                var pagos_prestamos = jasonLectura.LeerPagosPorFechaYCuenta(data.fechaInicio, data.fechaFinal, data.numeroDeCuenta);

                return Ok(new { success = true, retiros, pagos_tarjetas, pagos_prestamos, transferencias });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}