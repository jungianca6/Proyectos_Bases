using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuGestionClienteController : ControllerBase
    {

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("AgregarCliente")]
        public ActionResult AgregarCliente([FromBody] ClienteModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEscritura escrituraJson = new JasonEscritura();

                ClienteModel nuevo_Cliente = new ClienteModel();

                Random random = new Random();
                int id = random.Next(10_000_000, 100_000_000); // Entre 10,000,000 y 99,999,999

                nuevo_Cliente.Cedula = data.Cedula;
                nuevo_Cliente.Direccion = data.Direccion;
                nuevo_Cliente.Telefono = data.Telefono;
                nuevo_Cliente.IngresoMensual = data.IngresoMensual;
                nuevo_Cliente.Nombre = data.Nombre;
                nuevo_Cliente.Apellido1 = data.Apellido1;
                nuevo_Cliente.Apellido2 = data.Apellido2;
                nuevo_Cliente.TipoDeCliente = data.TipoDeCliente;
                nuevo_Cliente.Usuario = data.Usuario;
                nuevo_Cliente.Contrasena = data.Contrasena;
                nuevo_Cliente.AdminRol = data.AdminRol;


                // Crear una respuesta indicando éxito
                var response = new { success = true};

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
        [HttpPost("ModificarCliente")]
        public ActionResult ModificarCliente([FromBody] ClienteModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEditar edicionJson = new JasonEditar();

                ClienteModel nuevo_Cliente = new ClienteModel();

                Random random = new Random();

                nuevo_Cliente.Cedula = data.Cedula;
                nuevo_Cliente.Direccion = data.Direccion;
                nuevo_Cliente.Telefono = data.Telefono;
                nuevo_Cliente.IngresoMensual = data.IngresoMensual;
                nuevo_Cliente.Nombre = data.Nombre;
                nuevo_Cliente.Apellido1 = data.Apellido1;
                nuevo_Cliente.Apellido2 = data.Apellido2;
                nuevo_Cliente.TipoDeCliente = data.TipoDeCliente;
                nuevo_Cliente.Usuario = data.Usuario;
                nuevo_Cliente.Contrasena = data.Contrasena;
                nuevo_Cliente.AdminRol = data.AdminRol;


                edicionJson.EditarClienteParcial(nuevo_Cliente.Cedula, nuevo_Cliente);

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
        [HttpPost("EliminarCliente")]
        public ActionResult EliminarCliente([FromBody] EliminacionClienteDataInputModel data)
        {
            try
            {
                JasonEditar JasonEditar = new JasonEditar();

                JasonLectura jasonLectura = new JasonLectura();
                JasonEliminar jasonEliminar = new JasonEliminar();
                ClienteModel? cliente = jasonLectura.BuscarPorCedula(data.Cedula);

                var CuentasA = jasonLectura.LeerCuentas("Usuario", cliente.Usuario);
                var tarjetaEditada = new TarjetaModel
                {
                    NumeroDeCuenta = ""
                };
                if (jasonEliminar.EliminarPorCedula(data.Cedula))
                {
                    
                    foreach (var Cuenta in CuentasA)
                    {

                        var Tarjetas = jasonLectura.LeerTarjetas("NumeroDeCuenta", Cuenta.NumeroDeCuenta);
                        
                        foreach (var tarjeta in Tarjetas)
                        {
                            JasonEditar.EditarTarjeta(tarjeta.Numero, tarjetaEditada);
                        }
                        jasonEliminar.EliminarCuenta(Cuenta.NumeroDeCuenta);
                    }
                    var response = new { success = true, message = "El Cliente se elimino con exito" };
                    return Ok(response);
                }
                else
                {
                    var response = new { success = true, message = "El Cliente no se elimino" };
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