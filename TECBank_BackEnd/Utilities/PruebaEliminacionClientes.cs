using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaEliminacionClientes
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
            var cuentaEliminada = cuentas.RemoveAll(c => c.NúmeroDeCuenta == numeroCuenta);
            json.GuardarCuentas(cuentas);

            Console.WriteLine(cuentaEliminada > 0
                ? $"🗑️ Cuenta {numeroCuenta} eliminada correctamente."
                : $"⚠️ No se encontró cuenta con número {numeroCuenta}.");
        }
    }
}
