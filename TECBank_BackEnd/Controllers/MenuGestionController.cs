using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuGestionController : ControllerBase
    {
        // POST: MenuGestion/AgregarTarjeta
        [HttpPost("AgregarTarjeta")]
        public ActionResult AgregarTarjeta([FromBody] AgregarTarjetaDataInputModel data)
        {
            try
            {
                JasonLectura jasonLectura = new JasonLectura();
                var tarjetas = jasonLectura.LeerTarjetas();  // Asumiendo que este método te da las tarjetas almacenadas

                if (tarjetas.Any(t => t.NumeroDeCuenta == data.numeroDeCuenta && t.Numero == data.numeroDeTarjeta))
                {
                    // Crear una respuesta indicando que la tarjeta ya existe
                    var response = new { success = false, message = "La tarjeta ya existe" };
                    return Ok(response);  // Regresar la respuesta
                }
                else {

                    TarjetaModel nueva_tarjeta = new TarjetaModel();
                    nueva_tarjeta.TipoDeTarjeta = data.tipoDeTarjeta;
                    nueva_tarjeta.SaldoDisponible = data.saldo;
                    nueva_tarjeta.CCV = data.CCV;
                    nueva_tarjeta.NumeroDeCuenta = data.numeroDeCuenta;
                    nueva_tarjeta.FechaDeExpiracion = data.fechaDeExpiracion;
                    nueva_tarjeta.Numero = data.numeroDeTarjeta;

                    JasonEscritura jasonEscritura = new JasonEscritura();

                    jasonEscritura.GuardarTarjeta(nueva_tarjeta);

                    // Crear una respuesta indicando que la tarjeta ya existe
                    var response = new { success = true, message = "Tarjeta Agregada con exito" };
                    return Ok(response);

                }
            }
            catch (Exception ex)
            {

                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }

        // POST: MenuGestion/ModificarTarjeta
        [HttpPost("ModificarTarjeta")]
        public ActionResult ModificarTarjeta([FromBody] AgregarTarjetaDataInputModel data)
        {
            try
            {
                JasonLectura jasonLectura = new JasonLectura();
                var tarjetas = jasonLectura.LeerTarjetas();  // Asumiendo que este método te da las tarjetas almacenadas
                var tarjetaExistente = tarjetas.FirstOrDefault(t => t.NumeroDeCuenta == data.numeroDeCuenta && t.Numero == data.numeroDeTarjeta);

                if (tarjetaExistente != null)
                {

                    TarjetaModel tarjeta_con_cambios = new TarjetaModel();


                    if (!string.IsNullOrWhiteSpace(data.numeroDeTarjeta))
                        tarjeta_con_cambios.Numero = data.numeroDeTarjeta;

                    if (!string.IsNullOrWhiteSpace(data.tipoDeTarjeta))
                        tarjeta_con_cambios.TipoDeTarjeta = data.tipoDeTarjeta;

                    if (!string.IsNullOrWhiteSpace(data.fechaDeExpiracion))
                        tarjeta_con_cambios.FechaDeExpiracion = data.fechaDeExpiracion;

                    if (!string.IsNullOrWhiteSpace(data.CCV))
                        tarjeta_con_cambios.CCV = data.CCV;

                    // Asignar saldo solo si es diferente de cero
                    if (data.saldo != 0)
                        tarjeta_con_cambios.SaldoDisponible = data.saldo;

                    tarjeta_con_cambios.NumeroDeCuenta = data.numeroDeCuenta;


                    Console.WriteLine("Datos de tarjeta_con_cambios:");
                    Console.WriteLine("Número: " + tarjeta_con_cambios.Numero);
                    Console.WriteLine("Tipo de Tarjeta: " + tarjeta_con_cambios.TipoDeTarjeta);
                    Console.WriteLine("Fecha de Expiración: " + tarjeta_con_cambios.FechaDeExpiracion);
                    Console.WriteLine("CCV: " + tarjeta_con_cambios.CCV);
                    Console.WriteLine("Saldo Disponible: " + tarjeta_con_cambios.SaldoDisponible);
                    Console.WriteLine("Número de Cuenta: " + tarjeta_con_cambios.NumeroDeCuenta);


                    JasonEditar jasonEditar = new JasonEditar();
                    jasonEditar.EditarTarjeta(tarjeta_con_cambios.NumeroDeCuenta, tarjeta_con_cambios);

                    var response = new { success = true, message = "La tarjeta se modifico con exito" };
                    return Ok(response);
                }
                else
                {
                    // Si no encontramos ninguna tarjeta con esas condiciones
                    var response = new { success = false, message = "La tarjeta no existe" };
                    return Ok(response);  // O BadRequest() si prefieres marcarlo como error
                }
            }
            catch (Exception ex)
            {

                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }
        // POST: MenuGestion/EliminarTarjeta
        [HttpPost("EliminarTarjeta")]
        public ActionResult EliminarTarjeta([FromBody] EliminacionTarjetaDataInputModel data)
        {
            try
            {
                JasonEliminar jasonEliminar = new JasonEliminar();
                jasonEliminar.EliminarTarjeta(data.numeroDetarjeta);


                return Ok();
            }
            catch (Exception ex)
            {

                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }

    }
}