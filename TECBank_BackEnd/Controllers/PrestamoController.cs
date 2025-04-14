using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
                JasonEscritura jasonEscritura = new JasonEscritura();
                JasonEditar jasonEditar = new JasonEditar();
                JasonLectura jasonLectura = new JasonLectura();

                AsesorCreditoModel? asesor_de_credito = jasonLectura.BuscarAsesorPorCedula(data.Cedula_Asesor);

                if (asesor_de_credito != null)
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

                    //Se agrega el credito en el asesor
                    asesor_de_credito.Meta_Creditos.Add(data.Monto_Original);

                    jasonEditar.EditarAsesorCredito(asesor_de_credito.Cedula, asesor_de_credito);

                    jasonEscritura.GuardarPrestamo(nuevo_prestamo);

                    ClienteModel cliente_del_prestamo = jasonLectura.BuscarPorCedula(data.Cedula_Cliente);

                    CuentaModel cuenta_modificada = jasonLectura.BuscarCuentaPorUsuario(cliente_del_prestamo.Usuario);

                    cuenta_modificada.Monto = cuenta_modificada.Monto + data.Monto_Original;

                    jasonEditar.EditarCuenta(cuenta_modificada.NumeroDeCuenta, cuenta_modificada);




                    JasonLectura lectura = new JasonLectura();
                    PrestamoModel prestamo = lectura.BuscarPrestamoPorId(nuevo_prestamo.ID_Prestamos);

                    if (prestamo == null)
                        return NotFound("Préstamo no encontrado.");

                    if (!DateTime.TryParseExact(prestamo.FechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaVencimiento))
                        return BadRequest("Formato de fecha inválido. Debe ser 'dd/MM/yyyy'.");

                    DateTime fechaActual = DateTime.Now.Date;

                    if (fechaVencimiento <= fechaActual)
                        return BadRequest("La fecha de vencimiento debe ser futura.");

                    if (prestamo.Saldo_Pendiente <= 0)
                        return BadRequest("El saldo pendiente debe ser mayor a cero.");

                    DateTime fechaInicioPago = (fechaActual.Day > 1)
                        ? new DateTime(fechaActual.Year, fechaActual.Month, 1).AddMonths(1)
                        : new DateTime(fechaActual.Year, fechaActual.Month, 1);

                    DateTime fechaFinPago = new DateTime(fechaVencimiento.Year, fechaVencimiento.Month, 1);
                    int totalMeses = ((fechaFinPago.Year - fechaInicioPago.Year) * 12) + (fechaFinPago.Month - fechaInicioPago.Month) + 1;

                    if (totalMeses <= 0)
                        return BadRequest("No hay meses restantes para generar pagos.");

                    decimal montoMensual = Math.Round(prestamo.Saldo_Pendiente / (decimal)totalMeses, 2);

                    List<CuotaMensual> cuotasMensuales = new List<CuotaMensual>();
                    decimal totalGenerado = 0;

                    for (int i = 0; i < totalMeses; i++)
                    {
                        DateTime fechaPago = fechaInicioPago.AddMonths(i);
                        decimal monto = montoMensual;

                        if (i == totalMeses - 1)
                        {
                            monto = prestamo.Saldo_Pendiente - totalGenerado;
                            monto = Math.Round(monto, 2);
                        }

                        totalGenerado += monto;

                        cuotasMensuales.Add(new CuotaMensual
                        {
                            Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(fechaPago.Month),
                            Anio = fechaPago.Year,
                            FechaPago = fechaPago.ToString("dd/MM/yyyy"),
                            MontoAPagar = (int)monto,
                            Pagado = false // Inicializar en false por defecto
                        });
                    }

                    var calendario = new CalendarioPagoModel
                    {
                        ID_Prestamo = nuevo_prestamo.ID_Prestamos,
                        FechaVencimiento = prestamo.FechaVencimiento,
                        SaldoPendiente = prestamo.Saldo_Pendiente,
                        CuotasMensuales = cuotasMensuales
                    };

                    // Guardar en el JSON
                    JasonEscritura Escritura = new JasonEscritura();
                    Escritura.GuardarCalendarioPago(calendario);



                    return Ok(new { success = true, message = "El prestamo se ha creado con exito" });



                }
                else
                {
                    return Ok(new { success = false, message = "No es un asesor de credito, no puede emitir un prestamo" });
                }

               

            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

       
    }
}