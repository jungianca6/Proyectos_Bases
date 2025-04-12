using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonEliminar
    {
        public void EliminarPorCedula(string cedula)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();
            var clienteEliminado = clientes.RemoveAll(c => c.Cedula == cedula);
            json.GuardarClientes(clientes);

            Console.WriteLine(clienteEliminado > 0
                ? $"🗑️ Cliente con cédula {cedula} eliminado correctamente."
                : $"⚠️ No se encontró cliente con cédula {cedula}.");
        }

        public void EliminarCuenta(string numeroCuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();
            var cuentaEliminada = cuentas.RemoveAll(c => c.NumeroDeCuenta == numeroCuenta);
            json.GuardarCuentas(cuentas);

            Console.WriteLine(cuentaEliminada > 0
                ? $"🗑️ Cuenta {numeroCuenta} eliminada correctamente."
                : $"⚠️ No se encontró cuenta con número {numeroCuenta}.");
        }

        public void EliminarTarjeta(string numero)
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();
            var tarjetaEliminada = tarjetas.RemoveAll(t => t.Numero == numero);
            json.GuardarTarjetas(tarjetas);

            Console.WriteLine(tarjetaEliminada > 0
                ? $"🗑️ Tarjeta {numero} eliminada correctamente."
                : $"⚠️ No se encontró tarjeta con número {numero}.");
        }
                public void EliminarDeposito(string idDeposito)
        {
            Jason json = new Jason();
            var depositos = json.LeerDepositos();
            var depositoEliminado = depositos.RemoveAll(d => d.ID == idDeposito); // Usando ID heredado de MovimientoModel
            json.GuardarDepositos(depositos);

            Console.WriteLine(depositoEliminado > 0
                ? $"🗑️ Depósito {idDeposito} eliminado correctamente."
                : $"⚠️ No se encontró el depósito con ID {idDeposito}.");
        }

        public void EliminarEmpleado(string idEmpleado)
        {
            Jason json = new Jason();
            var empleados = json.LeerEmpleados();
            var eliminados = empleados.RemoveAll(e => e.Cedula == idEmpleado);
            json.GuardarEmpleados(empleados);

            Console.WriteLine(eliminados > 0
                ? $"🗑️ Empleado con cédula {idEmpleado} eliminado correctamente."
                : $"⚠️ No se encontró el empleado con cédula {idEmpleado}.");
        }

        public void EliminarPago(string idPago)
        {
            Jason json = new Jason();
            var pagos = json.LeerPagos();
            var pagoEliminado = pagos.RemoveAll(p => p.ID == idPago); // Usando ID heredado de MovimientoModel
            json.GuardarPagos(pagos);

            Console.WriteLine(pagoEliminado > 0
                ? $"🗑️ Pago {idPago} eliminado correctamente."
                : $"⚠️ No se encontró el pago con ID {idPago}.");
        }

        public void EliminarRetiro(string idRetiro)
        {
            Jason json = new Jason();
            var retiros = json.LeerRetiros();
            var retiroEliminado = retiros.RemoveAll(r => r.ID == idRetiro); // Usando ID heredado de MovimientoModel
            json.GuardarRetiros(retiros);

            Console.WriteLine(retiroEliminado > 0
                ? $"🗑️ Retiro {idRetiro} eliminado correctamente."
                : $"⚠️ No se encontró el retiro con ID {idRetiro}.");
        }
    }
}
