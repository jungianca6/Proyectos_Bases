using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonLectura
    {
        public List<ClienteModel> Ejecutar(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return filtro switch
            {
                "Nombre" => clientes.Where(c => c.Nombre == valor).ToList(),
                "Cedula" => clientes.Where(c => c.Cedula == valor).ToList(),
                "Apellido1" => clientes.Where(c => c.Apellido1 == valor).ToList(),
                "Apellido2" => clientes.Where(c => c.Apellido2 == valor).ToList(),
                "Direccion" => clientes.Where(c => c.Direccion == valor).ToList(),
                "Telefono" => clientes.Where(c => c.Telefono == valor).ToList(),
                "IngresoMensual" => clientes.Where(c => c.IngresoMensual.ToString() == valor).ToList(),
                "TipoDeCliente" => clientes.Where(c => c.TipoDeCliente == valor).ToList(),
                "Usuario" => clientes.Where(c => c.Usuario == valor).ToList(),
                "Contrasena" => clientes.Where(c => c.Contrasena == valor).ToList(),
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
                "NumeroDeCuenta" => cuentas.Where(c => c.NumeroDeCuenta == valor).ToList(),
                "Descripcion" => cuentas.Where(c => c.Descripcion == valor).ToList(),
                "Usuario" => cuentas.Where(c => c.Usuario == valor).ToList(),
                "Moneda" => cuentas.Where(c => c.Moneda == valor).ToList(),
                "TipoDeCuenta" => cuentas.Where(c => c.TipoDeCuenta == valor).ToList(),
                _ => cuentas
            };
        }

        public List<TarjetaModel> LeerTarjetas(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();

            return filtro switch
            {
                "Numero" => tarjetas.Where(t => t.Numero == valor).ToList(),
                "NumeroDeCuenta" => tarjetas.Where(t => t.NumeroDeCuenta == valor).ToList(),
                "TipoDeTarjeta" => tarjetas.Where(t => t.TipoDeTarjeta == valor).ToList(),
                "CCV" => tarjetas.Where(t => t.CCV == valor).ToList(),
                "SaldoDisponible" => tarjetas.Where(t => t.SaldoDisponible.ToString() == valor).ToList(),
                "FechaDeExpiracion" => tarjetas.Where(t => t.FechaDeExpiracion == valor).ToList(),
                _ => tarjetas
            };
        }

        public ClienteModel? BuscarPorUsuario(string usuario)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return clientes.FirstOrDefault(c =>
                c.Usuario?.Equals(usuario, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public CuentaModel? BuscarCuentaPorUsuario(string usuario)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            return cuentas.FirstOrDefault(c =>
                c.Usuario?.Equals(usuario, StringComparison.OrdinalIgnoreCase) == true
            );
        }

    }
}
