﻿using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonEditar
    {
        public void EditarClienteParcial(string cedula, ClienteModel nuevosDatos)
        {
            Jason holas = new Jason();
            var clientes = holas.LeerClientes();

            var clienteExistente = clientes.FirstOrDefault(c => c.Cedula == cedula);

            if (clienteExistente != null)
            {
                if (!string.IsNullOrWhiteSpace(nuevosDatos.Nombre))
                    clienteExistente.Nombre = nuevosDatos.Nombre;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Apellido1))
                    clienteExistente.Apellido1 = nuevosDatos.Apellido1;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Apellido2))
                    clienteExistente.Apellido2 = nuevosDatos.Apellido2;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Direccion))
                    clienteExistente.Direccion = nuevosDatos.Direccion;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Telefono))
                    clienteExistente.Telefono = nuevosDatos.Telefono;

                if (nuevosDatos.IngresoMensual != 0)
                    clienteExistente.IngresoMensual = nuevosDatos.IngresoMensual;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.TipoDeCliente))
                    clienteExistente.TipoDeCliente = nuevosDatos.TipoDeCliente;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Usuario))
                    clienteExistente.Usuario = nuevosDatos.Usuario;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Contrasena))
                    clienteExistente.Contrasena = nuevosDatos.Contrasena;

                if (clienteExistente.AdminRol != nuevosDatos.AdminRol)
                    clienteExistente.AdminRol = nuevosDatos.AdminRol;

                holas.GuardarClientes(clientes);

                Console.WriteLine($"✅ Cliente con cédula {cedula} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un cliente con cédula {cedula}.");
            }
        }

        public void EditarCuenta(string numeroCuenta, CuentaModel nuevosDatos)
        {
            Jason holas = new Jason();
            var cuentas = holas.LeerCuentas();

            var cuentaExistente = cuentas.FirstOrDefault(c => c.NumeroDeCuenta == numeroCuenta);

            if (cuentaExistente != null)
            {
                if (!string.IsNullOrWhiteSpace(nuevosDatos.Descripcion))
                    cuentaExistente.Descripcion = nuevosDatos.Descripcion;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Usuario))
                    cuentaExistente.Usuario = nuevosDatos.Usuario;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Moneda))
                    cuentaExistente.Moneda = nuevosDatos.Moneda;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.TipoDeCuenta))
                    cuentaExistente.TipoDeCuenta = nuevosDatos.TipoDeCuenta;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Nombre))
                    cuentaExistente.Nombre = nuevosDatos.Nombre;

                if (nuevosDatos.Monto != null)
                    cuentaExistente.Monto = nuevosDatos.Monto;

                holas.GuardarCuentas(cuentas);

                Console.WriteLine($"✅ Cuenta {numeroCuenta} actualizada parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró una cuenta con número {numeroCuenta}.");
            }
        }
        public void EditarTarjeta(string numero, TarjetaModel nuevosDatos)
        {
            Jason jason = new Jason();
            var tarjetas = jason.LeerTarjetas();

            var tarjeta = tarjetas.FirstOrDefault(t => t.Numero == numero);

            if (tarjeta != null)
            {
                if (!string.IsNullOrWhiteSpace(nuevosDatos.NumeroDeCuenta))
                    tarjeta.NumeroDeCuenta = nuevosDatos.NumeroDeCuenta;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.TipoDeTarjeta))
                    tarjeta.TipoDeTarjeta = nuevosDatos.TipoDeTarjeta;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.CCV))
                    tarjeta.CCV = nuevosDatos.CCV;

                if (nuevosDatos.SaldoDisponible > 0)
                    tarjeta.SaldoDisponible = nuevosDatos.SaldoDisponible;

                if (nuevosDatos.SaldoDisponible > 0)
                    tarjeta.SaldoDisponible = nuevosDatos.SaldoDisponible;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.FechaDeExpiracion))
                    tarjeta.FechaDeExpiracion = nuevosDatos.FechaDeExpiracion;



                jason.GuardarTarjetas(tarjetas);
                Console.WriteLine($"✅ Tarjeta {numero} actualizada parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró la tarjeta {numero}.");
            }
        }
        public void EditarEmpleado(string cedula, EmpleadoModel nuevosDatos)
        {
            Jason jason = new Jason();
            var empleados = jason.LeerEmpleados();

            var empleado = empleados.FirstOrDefault(e => e.Cedula == cedula);

            if (empleado != null)
            {
                if (!string.IsNullOrWhiteSpace(nuevosDatos.Nombre))
                    empleado.Nombre = nuevosDatos.Nombre;
                if (!string.IsNullOrWhiteSpace(nuevosDatos.Rol))
                    empleado.Rol = nuevosDatos.Rol;
                if (!string.IsNullOrWhiteSpace(nuevosDatos.DescripcionDeRol))
                    empleado.DescripcionDeRol = nuevosDatos.DescripcionDeRol;
                if (!string.IsNullOrWhiteSpace(nuevosDatos.Apellido1))
                    empleado.Apellido1 = nuevosDatos.Apellido1;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Apellido2))
                    empleado.Apellido2 = nuevosDatos.Apellido2;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.FechaDeNacimiento))
                    empleado.FechaDeNacimiento = nuevosDatos.FechaDeNacimiento;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Usuario))
                    empleado.Usuario = nuevosDatos.Usuario;
                if (nuevosDatos.IngresoMensual != 0)
                    empleado.IngresoMensual = nuevosDatos.IngresoMensual;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Contrasena))
                    empleado.Contrasena = nuevosDatos.Contrasena;

                if (empleado.AdminRol != nuevosDatos.AdminRol)
                    empleado.AdminRol = nuevosDatos.AdminRol;

                jason.GuardarEmpleados(empleados);
                Console.WriteLine($"✅ Empleado con cédula {cedula} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró el empleado con cédula {cedula}.");
            }

        }


        public void EditarAsesorCredito(string cedula, AsesorCreditoModel nuevosDatos)
        {
            Jason jason = new Jason();
            var asesores = jason.LeerAsesoresCredito();

            var asesor = asesores.FirstOrDefault(a => a.Cedula == cedula);

            if (asesor != null)
            {

                if (nuevosDatos.Meta_Colones > 0)
                    asesor.Meta_Colones = nuevosDatos.Meta_Colones;

                if (nuevosDatos.Meta_Creditos != null && nuevosDatos.Meta_Creditos.Count > 0)
                    asesor.Meta_Creditos = nuevosDatos.Meta_Creditos;

                jason.GuardarAsesoresCredito(asesores);
                Console.WriteLine($"✅ Asesor de crédito con cédula {cedula} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró el asesor de crédito con cédula {cedula}.");
            }
        }

        public void EditarPrestamo(string idPrestamo, PrestamoModel nuevosDatos)
        {
            Jason jason = new Jason();
            var prestamos = jason.LeerPrestamos();

            var prestamoExistente = prestamos.FirstOrDefault(p => p.ID_Prestamos == idPrestamo);

            if (prestamoExistente != null)
            {
                if (nuevosDatos.Monto_Original != 0)
                    prestamoExistente.Monto_Original = nuevosDatos.Monto_Original;

                if (nuevosDatos.Saldo_Pendiente != 0)
                    prestamoExistente.Saldo_Pendiente = nuevosDatos.Saldo_Pendiente;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Cedula_Cliete))
                    prestamoExistente.Cedula_Cliete = nuevosDatos.Cedula_Cliete;

                if (nuevosDatos.Tasa_De_Interes != 0)
                    prestamoExistente.Tasa_De_Interes = nuevosDatos.Tasa_De_Interes;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.ID_Prestamos))
                    prestamoExistente.ID_Prestamos = nuevosDatos.ID_Prestamos;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.FechaVencimiento))
                    prestamoExistente.FechaVencimiento = nuevosDatos.FechaVencimiento;

                jason.GuardarPrestamos(prestamos);
                Console.WriteLine($"✅ Préstamo con ID {idPrestamo} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un préstamo con ID {idPrestamo}.");
            }
        }




        public void EditarCalendarioPago(string idPrestamo, CalendarioPagoModel nuevosDatos)
        {
            Jason jason = new Jason();
            var calendarios = jason.LeerCalendarioPagos();

            var calendario = calendarios.FirstOrDefault(c => c.ID_Prestamo == idPrestamo);

            if (calendario != null)
            {
                // ID_Prestamo no se debe modificar, ya que es clave

                if (!string.IsNullOrWhiteSpace(nuevosDatos.FechaVencimiento))
                    calendario.FechaVencimiento = nuevosDatos.FechaVencimiento;

                if (nuevosDatos.SaldoPendiente != 0)
                    calendario.SaldoPendiente = nuevosDatos.SaldoPendiente;

                if (nuevosDatos.CuotasMensuales != null && nuevosDatos.CuotasMensuales.Count > 0)
                {
                    calendario.CuotasMensuales = nuevosDatos.CuotasMensuales.Select(c => new CuotaMensual
                    {
                        Mes = c.Mes,
                        Anio = c.Anio,
                        FechaPago = c.FechaPago,
                        MontoAPagar = c.MontoAPagar,
                        Pagado = c.Pagado // Aseguramos incluir este nuevo atributo
                    }).ToList();
                }

                jason.GuardarCalendarioPagos(calendarios);
                Console.WriteLine($"✅ Calendario de pago con ID {idPrestamo} actualizado correctamente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un calendario de pago con ID {idPrestamo}.");
            }
        }




    }
}
