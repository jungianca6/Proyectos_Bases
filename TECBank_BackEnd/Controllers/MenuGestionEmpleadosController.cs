using Microsoft.AspNetCore.Mvc;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuGestionEmpleadosController : ControllerBase
    {

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("AgregarEmpleado")]
        public ActionResult AgregarEmpleado([FromBody] EmpleadoModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEscritura escrituraJson = new JasonEscritura();

                EmpleadoModel nuevo_empleado = new EmpleadoModel();

                Random random = new Random();
                int id = random.Next(10_000_000, 100_000_000); // Entre 10,000,000 y 99,999,999

                nuevo_empleado.Nombre = data.Nombre;
                nuevo_empleado.Rol = data.Rol;
                nuevo_empleado.DescripcionDeRol = data.DescripcionDeRol;
                nuevo_empleado.Apellido1 = data.Apellido1;
                nuevo_empleado.Apellido2 = data.Apellido2;
                nuevo_empleado.Cedula = data.Cedula;
                nuevo_empleado.AdminRol = data.AdminRol;
                nuevo_empleado.FechaDeNacimiento = data.FechaDeNacimiento; nuevo_empleado.FechaDeNacimiento = data.FechaDeNacimiento;
                nuevo_empleado.Usuario = data.Usuario;
                nuevo_empleado.Contraseña = data.Contraseña;

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
        [HttpPost("ModificarEmpleado")]
        public ActionResult ModificarEmpleado([FromBody] EmpleadoModel data)
        {
            try
            {
                // Crear una instancia del escritor de clientes de prueba
                JasonEditar edicionJson = new JasonEditar();

                EmpleadoModel nueva_empleadoA = new EmpleadoModel();

                Random random = new Random();

                nueva_empleadoA.Nombre = data.Nombre;
                nueva_empleadoA.Rol = data.Rol;
                nueva_empleadoA.DescripcionDeRol = data.DescripcionDeRol;
                nueva_empleadoA.Apellido1 = data.Apellido1;
                nueva_empleadoA.Apellido2 = data.Apellido2;
                nueva_empleadoA.Cedula = data.Cedula;
                nueva_empleadoA.AdminRol = data.AdminRol;
                nueva_empleadoA.FechaDeNacimiento = data.FechaDeNacimiento; nueva_empleadoA.FechaDeNacimiento = data.FechaDeNacimiento;
                nueva_empleadoA.Usuario = data.Usuario;
                nueva_empleadoA.Contraseña = data.Contraseña;

                edicionJson.EditarEmpleado(nueva_empleadoA.Cedula, nueva_empleadoA);

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
        [HttpPost("EliminarEmpleado")]
        public ActionResult EliminarEmpleado([FromBody] EliminacionEmpleadoDataInputModel data)
        {
            try
            {
                JasonEliminar jasonEliminar = new JasonEliminar();

                if (jasonEliminar.EliminarEmpleado(data.Cedula))
                {

                    var response = new { success = true, message = "La empleado se elimino con exito" };
                    return Ok(response);
                }
                else
                {
                    var response = new { success = true, message = "La empleado no se elimino" };
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