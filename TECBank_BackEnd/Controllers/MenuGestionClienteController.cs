using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TECBank_BackEnd.Data_Input_Models;
using TECBank_BackEnd.Models;
using TECBank_BackEnd.Pruebas;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuGestionClienteController : ControllerBase
    {

        // Función auxiliar para generar un número de cuenta aleatorio
        private string GenerarNumeroDeCuenta()
        {
            return new Random().Next(10_000_000, 100_000_000).ToString();
        }


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

                escrituraJson.GuardarCliente(nuevo_Cliente);

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
        [HttpPost("CalendarioPrestamo")]
        public ActionResult CalendarioPrestamo([FromBody] CalendarioPrestamoDataInputModel data)
        {
            try
            {
                JasonLectura lectura = new JasonLectura();
                PrestamoModel prestamo = lectura.BuscarPrestamoPorId(data.ID_Prestamos);

                if (prestamo == null)
                    return NotFound("Préstamo no encontrado.");

                if (!DateTime.TryParseExact(prestamo.FechaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaVencimiento))
                    return BadRequest("Formato de fecha inválido. Debe ser 'dd/MM/yyyy'.");

                DateTime fechaActual = DateTime.Now.Date;

                if (fechaVencimiento <= fechaActual)
                    return BadRequest("La fecha de vencimiento debe ser futura.");

                if (prestamo.Saldo_Pendiente <= 0)
                    return BadRequest("El saldo pendiente debe ser mayor a cero.");

                // Si ya pasó el día 1 del mes actual, comenzamos desde el mes siguiente
                DateTime fechaInicioPago = (fechaActual.Day > 1)
                    ? new DateTime(fechaActual.Year, fechaActual.Month, 1).AddMonths(1)
                    : new DateTime(fechaActual.Year, fechaActual.Month, 1);

                // SIEMPRE incluir el mes del vencimiento
                DateTime fechaFinPago = new DateTime(fechaVencimiento.Year, fechaVencimiento.Month, 1);

                int totalMeses = ((fechaFinPago.Year - fechaInicioPago.Year) * 12) + (fechaFinPago.Month - fechaInicioPago.Month) + 1;

                if (totalMeses <= 0)
                    return BadRequest("No hay meses restantes para generar pagos.");

                decimal montoMensual = Math.Round(prestamo.Saldo_Pendiente / (decimal)totalMeses, 2);

                List<object> calendario = new List<object>();
                decimal totalGenerado = 0;

                for (int i = 0; i < totalMeses; i++)
                {
                    DateTime fechaPago = fechaInicioPago.AddMonths(i);
                    decimal monto = montoMensual;

                    // Ajuste en la última cuota
                    if (i == totalMeses - 1)
                    {
                        monto = prestamo.Saldo_Pendiente - totalGenerado;
                        monto = Math.Round(monto, 2);
                    }

                    totalGenerado += monto;

                    calendario.Add(new
                    {
                        Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(fechaPago.Month),
                        Anio = fechaPago.Year,
                        FechaPago = fechaPago.ToString("dd/MM/yyyy"),
                        MontoAPagar = monto
                    });
                }

                return Ok(new
                {
                    ID_Prestamo = data.ID_Prestamos,
                    FechaVencimiento = prestamo.FechaVencimiento,
                    SaldoPendiente = prestamo.Saldo_Pendiente,
                    CuotasMensuales = calendario
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: MenuGestionCuentas/AgregarCuenta
        [HttpPost("EliminarCliente")]
        public ActionResult EliminarCliente([FromBody] EliminacionClienteDataInputModel data)
        {
            try
            {
                JasonEditar JasonEdicion = new JasonEditar();

                JasonLectura jasonLectura = new JasonLectura();
                JasonEliminar jasonEliminar = new JasonEliminar();
                ClienteModel? cliente = jasonLectura.BuscarPorCedula(data.Cedula);

                var CuentasA = jasonLectura.LeerCuentas("Usuario", cliente.Usuario);


                var tarjetaEditada = new TarjetaModel
                {
                    NumeroDeCuenta = "0" // Esto no tiene ningun sentido
                };

                if (jasonEliminar.EliminarPorCedula(data.Cedula))
                {
                    
                    foreach (var Cuenta in CuentasA)
                    {

                        var Tarjetas = jasonLectura.LeerTarjetas("NumeroDeCuenta", Cuenta.NumeroDeCuenta);
                        
                            foreach (var tarjeta in Tarjetas)
                        {
                            JasonEdicion.EditarTarjeta(tarjeta.Numero, tarjetaEditada);
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