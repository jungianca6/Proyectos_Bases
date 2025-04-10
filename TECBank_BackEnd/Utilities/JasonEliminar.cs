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
    }
}
