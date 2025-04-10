using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonEscritura
    {
        public void Ejecutar(ClienteModel cliente)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            // Verificar si el cliente ya existe por cédula
            if (clientes.Any(c => c.Cedula == cliente.Cedula))
            {
                Console.WriteLine($"⚠️ Ya existe un cliente con la cédula {cliente.Cedula}. No se puede agregar.");
                return;
            }

            clientes.Add(cliente);
            json.GuardarClientes(clientes);
            Console.WriteLine($"✅ Cliente {cliente.Nombre} agregado correctamente.");
        }

        public void GuardarCuenta(CuentaModel cuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            // Verificar si la cuenta ya existe por número de cuenta
            if (cuentas.Any(c => c.NumeroDeCuenta == cuenta.NumeroDeCuenta))
            {
                Console.WriteLine($"⚠️ Ya existe una cuenta con el número {cuenta.NumeroDeCuenta}. No se puede agregar.");
                return;
            }

            cuentas.Add(cuenta);
            json.GuardarCuentas(cuentas);
            Console.WriteLine($"✅ Cuenta {cuenta.Nombre} agregada correctamente.");
        }
    }
}
