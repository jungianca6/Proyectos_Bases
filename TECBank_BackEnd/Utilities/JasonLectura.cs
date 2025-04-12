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
                "AdminRol" =>
                    bool.TryParse(valor, out var parsedValor)
                        ? clientes.Where(c => c.AdminRol == parsedValor).ToList()
                        : new List<ClienteModel>(),

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
                "Monto" => cuentas.Where(c => c.Monto == valor).ToList(),
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

        public List<DepositoModel> LeerDepositos(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var depositos = json.LeerDepositos();

            return filtro switch
            {
                "ID" => depositos.Where(d => d.ID == valor).ToList(),
                "Apellido1" => depositos.Where(d => d.Apellido1 == valor).ToList(),
                "Apellido2" => depositos.Where(d => d.Apellido2 == valor).ToList(),
                "Monto" => depositos.Where(d => d.Monto.ToString() == valor).ToList(),
                "Moneda" => depositos.Where(d => d.Moneda == valor).ToList(),
                "Fecha" => depositos.Where(d => d.Fecha == valor).ToList(),
                "CuentaEmisora" => depositos.Where(d => d.CuentaEmisora == valor).ToList(),
                "CuentaDestino" => depositos.Where(d => d.CuentaDestino == valor).ToList(),
                _ => depositos
            };
        }

        public List<EmpleadoModel> LeerEmpleados(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var empleados = json.LeerEmpleados();

            return filtro switch
            {
                "Cedula" => empleados.Where(e => e.Cedula == valor).ToList(),
                "Nombre" => empleados.Where(e => e.Nombre == valor).ToList(),
                "Apellido1" => empleados.Where(e => e.Apellido1 == valor).ToList(),
                "Apellido2" => empleados.Where(e => e.Apellido2 == valor).ToList(),
                "Rol" => empleados.Where(e => e.Rol == valor).ToList(),
                "DescripcionDeRol" => empleados.Where(e => e.DescripcionDeRol == valor).ToList(),

                "FechaDeNacimiento" => empleados.Where(e => e.FechaDeNacimiento.ToString() == valor).ToList(),
                "Usuario" => empleados.Where(e => e.Usuario == valor).ToList(),
                "Contraseña" => empleados.Where(e => e.Contraseña == valor).ToList(),
                "AdminRol" =>
                    bool.TryParse(valor, out var parsedValor)
                        ? empleados.Where(c => c.AdminRol == parsedValor).ToList()
                        : new List<EmpleadoModel>(),
                _ => empleados
            };
        }

        public List<PagoModel> LeerPagos(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var pagos = json.LeerPagos();

            return filtro switch
            {
                "ID" => pagos.Where(p => p.ID == valor).ToList(),
                "Apellido1" => pagos.Where(p => p.Apellido1 == valor).ToList(),
                "Apellido2" => pagos.Where(p => p.Apellido2 == valor).ToList(),
                "Monto" => pagos.Where(p => p.Monto.ToString() == valor).ToList(),
                "Moneda" => pagos.Where(p => p.Moneda == valor).ToList(),
                "Fecha" => pagos.Where(p => p.Fecha == valor).ToList(),
                "Cuenta_Emisora" => pagos.Where(p => p.Cuenta_Emisora == valor).ToList(),
                "Numero_de_Tarjeta" => pagos.Where(p => p.Numero_de_Tarjeta == valor).ToList(),
                _ => pagos
            };
        }

        public List<RetiroModel> LeerRetiros(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var retiros = json.LeerRetiros();

            return filtro switch
            {
                "ID" => retiros.Where(r => r.ID == valor).ToList(),
                "CuentaARetirar" => retiros.Where(r => r.CuentaARetirar == valor).ToList(),
                "Apellido1" => retiros.Where(r => r.Apellido1 == valor).ToList(),
                "Apellido2" => retiros.Where(r => r.Apellido2 == valor).ToList(),
                "Monto" => retiros.Where(r => r.Monto.ToString() == valor).ToList(),
                "Moneda" => retiros.Where(r => r.Moneda == valor).ToList(),
                "Fecha" => retiros.Where(r => r.Fecha == valor).ToList(),
                _ => retiros
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

        public ClienteModel? BuscarPorCedula(string cedula)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return clientes.FirstOrDefault(c =>
                c.Cedula?.Equals(cedula, StringComparison.OrdinalIgnoreCase) == true
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

        public CuentaModel? BuscarCuentaPorNumero(string numero_de_cuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            return cuentas.FirstOrDefault(c =>
                c.NumeroDeCuenta?.Equals(numero_de_cuenta, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public TarjetaModel? BuscarTarjetaPorNumero(string numero_tarjeta)
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();

            return tarjetas.FirstOrDefault(t =>
                t.Numero?.Equals(numero_tarjeta, StringComparison.OrdinalIgnoreCase) == true
            );
        }

    }
}
