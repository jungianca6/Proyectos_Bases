using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MenuInicioController : ControllerBase
    {
        // POST: MenuInicio/Registro
        [HttpPost("Registro")]
        public ActionResult Registro([FromBody] ClienteModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEscritura escrituraJson = new JasonEscritura();

                // Ejecutar el método para registrar los datos del cliente
                escrituraJson.Ejecutar(data);

                CuentaModel nueva_cuenta = new CuentaModel();

                Random random = new Random();
                int id = random.Next(10_000_000, 100_000_000); // Entre 10,000,000 y 99,999,999

                nueva_cuenta.Usuario = data.Usuario;
                nueva_cuenta.Nombre = data.Nombre;
                nueva_cuenta.TipoDeCuenta = "";
                nueva_cuenta.Descripcion = "";
                nueva_cuenta.Moneda = "Colones";
                nueva_cuenta.NumeroDeCuenta = id.ToString();

                escrituraJson.GuardarCuenta(nueva_cuenta);

                // Crear una respuesta indicando éxito
                var response = new { success = true };

                // Retornar la respuesta con código 200 (OK)
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

        // POST: MenuInicio/Login
        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginDataInputModel data)
        {
            // Crear una instancia del lector de clientes
            JasonLectura lector = new JasonLectura();

            // Buscar el cliente por nombre de usuario
            ClienteModel? cliente = lector.BuscarPorUsuario(data.usuario);

            // Verificar si se encontró un cliente con ese usuario
            if (cliente != null)
            {
                // Si la contraseña no coincide, devolver mensaje de error
                if (cliente.Contrasena != data.contrasena)
                {
                    var response = new { success = false, message = "Contraseña incorrecta" };
                    return Ok(response);
                }
                else
                {
                    CuentaModel cuenta = lector.BuscarCuentaPorUsuario(data.usuario);
                    // Si la contraseña es correcta, devolver el cliente como usuario actual
                    var response = new { success = true, usuario_actual = cliente, cuenta_actual = cuenta };
                    return Ok(response);
                }
            }
            else
            {
                // Si el usuario no existe, devolver mensaje de error
                var response = new { success = false, message = "El Usuario no existe" };
                return Ok(response);
            }
        }
    }
}
