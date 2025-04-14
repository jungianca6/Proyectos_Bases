using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuGestionCuentasController : ControllerBase
    {

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("AgregarCuenta")]
        public ActionResult AgregarCuenta([FromBody] CuentaModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEscritura escrituraJson = new JasonEscritura();

                CuentaModel nueva_cuenta = new CuentaModel();

                Random random = new Random();
                int id = random.Next(10_000_000, 100_000_000); // Entre 10,000,000 y 99,999,999

                nueva_cuenta.Usuario = data.Usuario;
                nueva_cuenta.Nombre = data.Nombre;
                nueva_cuenta.TipoDeCuenta = data.TipoDeCuenta;
                nueva_cuenta.Descripcion = data.Descripcion;
                nueva_cuenta.Moneda = data.Moneda;
                nueva_cuenta.NumeroDeCuenta = id.ToString();

                escrituraJson.GuardarCuenta(nueva_cuenta);

                // Crear una respuesta indicando éxito
                var response = new { success = true };

                // Retornar la respuesta con código 200 (OK)
                return Ok(response);

            }
            catch (Exception ex)
            {
                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("ModificarCuenta")]
        public ActionResult ModificarCuenta([FromBody] CuentaModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEditar edicionJson = new JasonEditar();

                CuentaModel nueva_cuenta = new CuentaModel();

                Random random = new Random();

                nueva_cuenta.NumeroDeCuenta = data.NumeroDeCuenta.ToString();
                nueva_cuenta.Usuario = data.Usuario;
                nueva_cuenta.Nombre = data.Nombre;
                nueva_cuenta.TipoDeCuenta = data.TipoDeCuenta;
                nueva_cuenta.Descripcion = data.Descripcion;
                nueva_cuenta.Moneda = data.Moneda;

                edicionJson.EditarCuenta(nueva_cuenta.NumeroDeCuenta, nueva_cuenta);

                // Crear una respuesta indicando éxito
                var response = new { success = true };

                // Retornar la respuesta con código 200 (OK)
                return Ok(response);

            }
            catch (Exception ex)
            {
                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("EliminarCuenta")]
        public ActionResult EliminarCuenta([FromBody] EliminacionCuentaDataInputModel data)
        {
            try
            {
                JasonEliminar jasonEliminar = new JasonEliminar();

                if (jasonEliminar.EliminarCuenta(data.numeroDeCuenta))
                {

                    var response = new { success = true, message = "La cuenta se elimino con exito" };
                    return Ok(response);
                }
                else
                {
                    var response = new { success = true, message = "La cuenta no se elimino con exito" };
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }

    }
}