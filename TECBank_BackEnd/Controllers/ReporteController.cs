using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReporteController : ControllerBase
    {

        // POST: Movimiento/Pago
        [HttpPost("ReporteAsesores")]
        public ActionResult ReporteAsesores([FromBody] ReporteAsesorDeCreditoDataInputModel data)
        {
            try
            {

                JasonLectura jasonLectura = new JasonLectura();
                AsesorCreditoModel? asesor_de_credito = null;
                EmpleadoModel? empleado = null;

                asesor_de_credito = jasonLectura.BuscarAsesorPorCedula(data.Cedula_Asesor);
                empleado = jasonLectura.BuscarEmpleadoPorCedula(data.Cedula_Asesor);

                if (empleado == null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "El Asesor de credito no existe"
                    });
                }
                if (asesor_de_credito == null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "El Asesor de credito no existe"
                    });
                }

                int suma_montos_entregados = asesor_de_credito.Meta_Creditos.Sum();
                float comision_total_colones = 0;
                float comision_total_dolares = 0;
                List<float> comisiones_colones = new List<float>();
                List<float> comisiones_dolares = new List<float>();

                if (suma_montos_entregados > asesor_de_credito.Meta_Colones)
                {
                    foreach (int credito in asesor_de_credito.Meta_Creditos)
                    {
                        comisiones_colones.Add(credito * 0.03f);
                        comisiones_dolares.Add((credito * 0.03f) / 500);
                        comision_total_dolares += (credito * 0.03f) / 500;
                        comision_total_colones += (credito * 0.03f);
                    }

                    return Ok(new
                    {
                        success = true,
                        message = "Comisiones calculadas correctamente.",
                        nombre = empleado.Nombre,
                        MetaColones = asesor_de_credito.Meta_Colones,
                        MetaDolares = asesor_de_credito.Meta_Colones / 500,
                        TotalCreditosColones = suma_montos_entregados,
                        TotalCreditosDolares = suma_montos_entregados / 500,
                        comisionesColones = comisiones_colones,
                        comisionesDolares = comisiones_dolares,
                        totalColones = comision_total_colones,
                        totalDolares = comision_total_dolares
                    });


                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Comisiones calculadas correctamente.",
                        nombre = empleado.Nombre,
                        MetaColones = asesor_de_credito.Meta_Colones,
                        MetaDolares = asesor_de_credito.Meta_Colones / 500,
                        TotalCreditosColones = 0,
                        TotalCreditosDolares = 0,
                        comisionesColones = new List<float>(),
                        comisionesDolares = new List<float>(),
                        totalColones = 0,
                        totalDolares = 0
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: Movimiento/Pago
        [HttpPost("ReporteMora")]
        public ActionResult ReporteMora([FromBody] ReporteMoraDataInputModel data)
        {
            try
            {
                JasonLectura jasonLectura = new JasonLectura();
                ClienteModel? cliente_mora = jasonLectura.BuscarPorCedula(data.Cedula_Cliente);
                if (cliente_mora == null)
                    return NotFound(new { success = false, message = "Cliente no encontrado" });

                CuentaModel? cuenta = jasonLectura.BuscarCuentaPorUsuario(cliente_mora.Usuario);
                if (cuenta == null)
                    return NotFound(new { success = false, message = "Cuenta no encontrada" });

                List<PrestamoModel> prestamos_del_cliente = jasonLectura.LeerPrestamos("Cedula_Cliete", cliente_mora.Cedula);
                if (prestamos_del_cliente == null || prestamos_del_cliente.Count == 0)
                    return NotFound(new { success = false, message = "No hay préstamos asociados al cliente" });

                var calendarios = jasonLectura.LeerCalendariosPago();
                var prestamosConMora = new List<object>();

                DateTime fechaActual = DateTime.Now;

                foreach (var prestamo in prestamos_del_cliente)
                {
                    var calendario = calendarios.FirstOrDefault(c => c.ID_Prestamo == prestamo.ID_Prestamos);
                    if (calendario == null) continue;

                    var cuotasAtrasadas = calendario.CuotasMensuales
                        .Where(cuota =>
                        {
                            DateTime fechaCuota;
                            if (!DateTime.TryParseExact(cuota.FechaPago, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fechaCuota))
                                return false;
                            return !cuota.Pagado && fechaCuota < fechaActual;
                        })
                        .ToList();

                    if (cuotasAtrasadas.Count > 0)
                    {
                        int montoTotalAtrasado = cuotasAtrasadas.Sum(c => c.MontoAPagar);

                        prestamosConMora.Add(new
                        {
                            ID_Prestamo = prestamo.ID_Prestamos,
                            CuotasAtrasadas = cuotasAtrasadas,
                            MontoTotalAtrasado = montoTotalAtrasado
                        });
                    }
                }
                if (prestamosConMora.Count == 0)
                    return Ok(new { success = true, message = "No hay cuotas en mora", prestamos = new List<object>() });
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Reporte de mora generado con éxito",
                        Nombre = cliente_mora.Nombre,
                        Apellido1 = cliente_mora.Apellido1,
                        Apellido2 = cliente_mora.Apellido2,
                        Cedula = cliente_mora.Cedula,
                        prestamos = prestamosConMora
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}