using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonEliminar
    {
        public bool EliminarPorCedula(string cedula)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();
            var clienteEliminado = clientes.RemoveAll(c => c.Cedula == cedula);
            json.GuardarClientes(clientes);
            return clienteEliminado > 0;
        }

        public bool EliminarCuenta(string numeroCuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();
            var cuentaEliminada = cuentas.RemoveAll(c => c.NumeroDeCuenta == numeroCuenta);
            json.GuardarCuentas(cuentas);
            return cuentaEliminada > 0;
        }

        public bool EliminarTarjeta(string numero)
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();
            var tarjetaEliminada = tarjetas.RemoveAll(t => t.Numero == numero);
            json.GuardarTarjetas(tarjetas);
            return tarjetaEliminada > 0;
        }


        public bool EliminarEmpleado(string idEmpleado)
        {
            Jason json = new Jason();
            var empleados = json.LeerEmpleados();
            var eliminados = empleados.RemoveAll(e => e.Cedula == idEmpleado);
            json.GuardarEmpleados(empleados);
            return eliminados > 0;
        }

        public bool EliminarAsesorCredito(string cedula)
        {
            Jason json = new Jason();
            var asesores = json.LeerAsesoresCredito();
            var eliminados = asesores.RemoveAll(a => a.Cedula == cedula);
            json.GuardarAsesoresCredito(asesores);
            return eliminados > 0;
        }


    }
}
