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
        private readonly JasonEscritura escrituraJson = new();
        private readonly JasonLectura lector = new();

        // POST: MenuInicio/Registro
        [HttpPost("Registro")]
        public ActionResult Registro([FromBody] ClienteModel data)
        {
            try
            {
                // Guardar cliente
                escrituraJson.GuardarCliente(data);

                // Crear y guardar cuenta asociada al nuevo cliente
                CuentaModel nuevaCuenta = new()
                {
                    Usuario = data.Usuario,
                    Nombre = data.Nombre,
                    TipoDeCuenta = "",
                    Descripcion = "",
                    Moneda = "Colones",
                    NumeroDeCuenta = GenerarNumeroDeCuenta()
                };

                escrituraJson.GuardarCuenta(nuevaCuenta);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: MenuInicio/Login
        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginDataInputModel data)
        {
            try
            {
                ClienteModel? cliente = lector.BuscarPorUsuario(data.usuario);

                if (cliente == null)
                    return Ok(new { success = false, message = "El Usuario no existe" });

                if (cliente.Contrasena != data.contrasena)
                    return Ok(new { success = false, message = "Contraseña incorrecta" });

                CuentaModel cuenta = lector.BuscarCuentaPorUsuario(data.usuario);

                return Ok(new { success = true, usuario_actual = cliente, cuenta_actual = cuenta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Función auxiliar para generar un número de cuenta aleatorio
        private string GenerarNumeroDeCuenta()
        {
            return new Random().Next(10_000_000, 100_000_000).ToString();
        }
    }
}