using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaEditarClientes
    {
        public void EditarClienteParcial(string cedula, ClienteModel nuevosDatos)
        {
            Jason holas = new Jason();
            var clientes = holas.LeerClientes();

            var clienteExistente = clientes.FirstOrDefault(c => c.Cedula == cedula);

            if (clienteExistente != null)
            {
                // Solo modificar si se proporciona un valor distinto
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
    }
}
