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
        private readonly JasonEscritura escrituraJson = new();
        private readonly JasonEditar edicionJson = new();
        private readonly JasonEliminar eliminadorJson = new();

        // Agrega un nuevo empleado
        [HttpPost("AgregarEmpleado")]
        public ActionResult AgregarEmpleado([FromBody] EmpleadoModel data)
        {
            try
            {
                EmpleadoModel nuevoEmpleado = new()
                {
                    Nombre = data.Nombre,
                    Rol = data.Rol,
                    DescripcionDeRol = data.DescripcionDeRol,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    Cedula = data.Cedula,
                    AdminRol = data.AdminRol,
                    FechaDeNacimiento = data.FechaDeNacimiento,
                    Usuario = data.Usuario,
                    Contrasena = data.Contrasena,
                    IngresoMensual = data.IngresoMensual
                };

                escrituraJson.GuardarEmpleado(nuevoEmpleado);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Agrega un nuevo empleado
        [HttpPost("AgregarAsesorDeCredito")]
        public ActionResult AgregarAsesorDeCredito([FromBody] AgregarAsesorDeCreditoDataInputModel data)
        {
            try
            {
                EmpleadoModel nuevoEmpleado = new()
                {
                    Nombre = data.Nombre,
                    Rol = data.Rol,
                    DescripcionDeRol = data.DescripcionDeRol,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    Cedula = data.Cedula,
                    AdminRol = true,
                    FechaDeNacimiento = data.FechaDeNacimiento,
                    Usuario = data.Usuario,
                    Contrasena = data.Contrasena,
                    IngresoMensual = data.IngresoMensual
                };

                AsesorCreditoModel nuevoAsesor = new()
                {

                    Cedula = data.Cedula,
                    Meta_Colones = data.Meta_Colones,
                    Meta_Creditos = []
                };

                escrituraJson.GuardarAsesorCredito(nuevoAsesor);

                escrituraJson.GuardarEmpleado(nuevoEmpleado);

                return Ok(new { success = true, message = "Asesor agregado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Agrega un nuevo empleado
        [HttpPost("EditarAsesorDeCredito")]
        public ActionResult EditarAsesorDeCredito([FromBody] AgregarAsesorDeCreditoDataInputModel data)
        {
            try
            {
                EmpleadoModel nuevoEmpleado = new()
                {
                    Nombre = data.Nombre,
                    Rol = data.Rol,
                    DescripcionDeRol = data.DescripcionDeRol,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    Cedula = data.Cedula,
                    AdminRol = true,
                    FechaDeNacimiento = data.FechaDeNacimiento,
                    Usuario = data.Usuario,
                    Contrasena = data.Contrasena,
                    IngresoMensual = data.IngresoMensual
                };

                AsesorCreditoModel nuevoAsesor = new()
                {

                    Cedula = data.Cedula,
                    Meta_Colones = data.Meta_Colones,
                    Meta_Creditos = data.Meta_Creditos
                };

                edicionJson.EditarAsesorCredito(nuevoAsesor.Cedula,nuevoAsesor);

                return Ok(new { success = true , message = "Asesor modificado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Agrega un nuevo empleado
        [HttpPost("EliminarAsesorDeCredito")]
        public ActionResult EliminarAsesorDeCredito([FromBody] EliminacionEmpleadoDataInputModel data)
        {
            try
            {
                eliminadorJson.EliminarEmpleado(data.Cedula);
                eliminadorJson.EliminarAsesorCredito(data.Cedula);

                return Ok(new { success = true, message = "Asesor modificado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        // Modifica los datos de un empleado
        [HttpPost("ModificarEmpleado")]
        public ActionResult ModificarEmpleado([FromBody] EmpleadoModel data)
        {
            try
            {
                EmpleadoModel empleadoActualizado = new()
                {
                    Nombre = data.Nombre,
                    Rol = data.Rol,
                    DescripcionDeRol = data.DescripcionDeRol,
                    Apellido1 = data.Apellido1,
                    Apellido2 = data.Apellido2,
                    Cedula = data.Cedula,
                    AdminRol = data.AdminRol,
                    FechaDeNacimiento = data.FechaDeNacimiento,
                    Usuario = data.Usuario,
                    Contrasena = data.Contrasena,
                    IngresoMensual = data.IngresoMensual
                };

                edicionJson.EditarEmpleado(data.Cedula, empleadoActualizado);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Elimina un empleado por cédula
        [HttpPost("EliminarEmpleado")]
        public ActionResult EliminarEmpleado([FromBody] EliminacionEmpleadoDataInputModel data)
        {
            try
            {
                bool eliminado = eliminadorJson.EliminarEmpleado(data.Cedula);

                if (eliminado)
                {
                    return Ok(new { success = true, message = "El empleado se eliminó con éxito" });
                }
                else
                {
                    return Ok(new { success = true, message = "No se encontró el empleado a eliminar" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}