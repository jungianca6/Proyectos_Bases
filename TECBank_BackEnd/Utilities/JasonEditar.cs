using TECBank_BackEnd.Models;
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

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Apellido1))
                    empleado.Apellido1 = nuevosDatos.Apellido1;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Apellido2))
                    empleado.Apellido2 = nuevosDatos.Apellido2;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.FechaDeNacimiento))
                    empleado.FechaDeNacimiento = nuevosDatos.FechaDeNacimiento;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Usuario))
                    empleado.Usuario = nuevosDatos.Usuario;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Contraseña))
                    empleado.Contraseña = nuevosDatos.Contraseña;

                jason.GuardarEmpleados(empleados);
                Console.WriteLine($"✅ Empleado con cédula {cedula} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró el empleado con cédula {cedula}.");
            }
        }

        public void EditarPago(string cuentaEmisora, PagoModel nuevosDatos)
        {
            Jason jason = new Jason();
            var pagos = jason.LeerPagos();

            var pago = pagos.FirstOrDefault(p => p.Cuenta_Emisora == cuentaEmisora);

            if (pago != null)
            {
                if (!string.IsNullOrWhiteSpace(nuevosDatos.Cuenta_Emisora))
                    pago.Cuenta_Emisora = nuevosDatos.Cuenta_Emisora;

                if (!string.IsNullOrWhiteSpace(nuevosDatos.Numero_de_Tarjeta))
                    pago.Numero_de_Tarjeta = nuevosDatos.Numero_de_Tarjeta;

                jason.GuardarPagos(pagos);
                Console.WriteLine($"✅ Pago de cuenta emisora {cuentaEmisora} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró el pago con cuenta emisora {cuentaEmisora}.");
            }
        }

        public void EditarRetiro(string cedulaCliente, RetiroModel nuevosDatos)
        {
            Jason jason = new Jason();
            var retiros = jason.LeerRetiros();

            var retiro = retiros.FirstOrDefault(r => r.CuentaARetirar.Usuario == cedulaCliente);

            if (retiro != null && nuevosDatos.CuentaARetirar != null)
            {
                retiro.CuentaARetirar = nuevosDatos.CuentaARetirar;

                jason.GuardarRetiros(retiros);
                Console.WriteLine($"✅ Retiro para cliente {cedulaCliente} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un retiro para cliente {cedulaCliente}.");
            }
        }

        public void EditarDeposito(string cuentaOrigen, DepositoModel nuevosDatos)
        {
            Jason jason = new Jason();
            var depositos = jason.LeerDepositos();

            var deposito = depositos.FirstOrDefault(d => d.CuentaEmisora == cuentaOrigen);

            if (deposito != null)
            {
                if (nuevosDatos.CuentaEmisora != null)
                    deposito.CuentaEmisora = nuevosDatos.CuentaEmisora;

                if (nuevosDatos.CuentaDestino != null)
                    deposito.CuentaDestino = nuevosDatos.CuentaDestino;

                jason.GuardarDepositos(depositos);
                Console.WriteLine($"✅ Depósito desde cuenta {cuentaOrigen} actualizado parcialmente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un depósito desde la cuenta {cuentaOrigen}.");
            }
        }
    }
}
