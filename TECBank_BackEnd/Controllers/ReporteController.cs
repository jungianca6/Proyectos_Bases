using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

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
                AsesorCreditoModel? asesor_de_credito = jasonLectura.BuscarAsesorPorCedula(data.Cedula_Asesor);
                EmpleadoModel? empleado = jasonLectura.BuscarEmpleadoPorCedula(data.Cedula_Asesor);



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
                        MetaDolares = asesor_de_credito.Meta_Colones / 500 ,
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
    }
}
