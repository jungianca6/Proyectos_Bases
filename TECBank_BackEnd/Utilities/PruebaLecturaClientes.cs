using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaLecturaClientes
    {
        public List<ClienteModel> Ejecutar(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return filtro switch
            {
                "Nombre" => clientes.Where(c => c.Nombre == valor).ToList(),
                "Cedula" => clientes.Where(c => c.Cedula == valor).ToList(),
                _ => clientes
            };
        }

        public List<CuentaModel> LeerCuentas(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            return filtro switch
            {
                "Nombre" => cuentas.Where(c => c.Nombre == valor).ToList(),
                "NúmeroDeCuenta" => cuentas.Where(c => c.NúmeroDeCuenta == valor).ToList(),
                _ => cuentas
            };
        }

        // Método para buscar por usuario (lo que solicitaste)
        public ClienteModel? BuscarPorUsuario(string usuario)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            // Buscar coincidencia exacta por usuario
            var cliente = clientes.FirstOrDefault(c =>
                c.Usuario?.Equals(usuario, StringComparison.OrdinalIgnoreCase) == true
            );

            return cliente;
        }
    }
}
