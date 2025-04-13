using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuGestionRolController : ControllerBase
    {

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("CambiarAdminRol")]
        public ActionResult CambiarAdminRol([FromBody] CambiarAdminRolDataInputModel data)
        {
            try
            {

                JasonLectura jasonLectura = new JasonLectura();

                ClienteModel? cliente = jasonLectura.BuscarPorCedula(data.Cedula);

                EmpleadoModel? empleado = jasonLectura.BuscarEmpleadoPorCedula(data.Cedula);

                JasonEditar jasonEditar = new JasonEditar();

                if (cliente != null) { 
                    ClienteModel cliente_modificado = new ClienteModel();
                    cliente_modificado.AdminRol = data.AdminRol;
                    cliente_modificado.Cedula = data.Cedula;

                    jasonEditar.EditarClienteParcial(cliente.Cedula, cliente_modificado);

                    // Crear una respuesta indicando éxito
                    var response = new { success = true , message = "Se agregaron permisos de administrador"};

                    // Retornar la respuesta con código 200 (OK)
                    return Ok(response);

                }
                if (empleado != null)
                {
                    EmpleadoModel empleado_modificado = new EmpleadoModel();
                    empleado_modificado.AdminRol = data.AdminRol;

                    jasonEditar.EditarEmpleado(empleado.Cedula, empleado_modificado);

                    // Crear una respuesta indicando éxito
                    var response = new { success = true, message = "Se agregaron permisos de administrador" };

                    // Retornar la respuesta con código 200 (OK)
                    return Ok(response);
                }
                else {
                    // Crear una respuesta indicando éxito
                    var response = new { success = false, message = "El usuario no existe" };
                    // Retornar la respuesta con código 200 (OK)
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                // Retornar la respuesta con código 400 (BadRequest)
                return BadRequest();
            }
        }

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("CambiarRol")]
        public ActionResult CambiarRol([FromBody] CambiarRolDataInputModel data)
        {
            try
            {

                JasonLectura jasonLectura = new JasonLectura();

                EmpleadoModel? empleado = jasonLectura.BuscarEmpleadoPorCedula(data.Cedula);

                JasonEditar jasonEditar = new JasonEditar();

                if (empleado != null)
                {
                    EmpleadoModel empleado_modificado = new EmpleadoModel();
                    empleado_modificado.Rol = data.Rol;
                    empleado_modificado.DescripcionDeRol = data.DescripcionDeRol;

                    jasonEditar.EditarEmpleado(empleado.Cedula, empleado_modificado);

                    // Crear una respuesta indicando éxito
                    var response = new { success = true, message = "Se cambio el rol con exito" };

                    // Retornar la respuesta con código 200 (OK)
                    return Ok(response);
                }
                else
                {
                    // Crear una respuesta indicando éxito
                    var response = new { success = false, message = "El usuario no existe" };
                    // Retornar la respuesta con código 200 (OK)
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