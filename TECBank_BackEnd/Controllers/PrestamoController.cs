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

                    return Ok(new { success = true, message = "El prestamo se ha creado con exito" });

                }else
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