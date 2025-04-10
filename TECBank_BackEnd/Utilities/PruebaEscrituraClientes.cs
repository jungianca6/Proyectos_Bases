using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaEscrituraClientes
    {
        public void Ejecutar(ClienteModel cliente)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();
            clientes.Add(cliente);
            json.GuardarClientes(clientes);
            Console.WriteLine($"✅ Cliente {cliente.Nombre} agregado correctamente.");
        }

        public void GuardarCuenta(CuentaModel cuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();
            cuentas.Add(cuenta);
            json.GuardarCuentas(cuentas);
            Console.WriteLine($"✅ Cuenta {cuenta.Nombre} agregada correctamente.");
        }
    }
}
