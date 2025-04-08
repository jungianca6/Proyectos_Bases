using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaEditarClientes
    {
        public void EditarCliente(string cedula, ClienteModel nuevosDatos)
        {
            Holas holas = new Holas();
            var clientes = holas.LeerClientes();

            var clienteExistente = clientes.FirstOrDefault(c => c.Cedula == cedula);

            if (clienteExistente != null)
            {
                // Aplicar los cambios desde el objeto nuevosDatos
                clienteExistente.Nombre = nuevosDatos.Nombre;
                clienteExistente.Apellido1 = nuevosDatos.Apellido1;
                clienteExistente.Apellido2 = nuevosDatos.Apellido2;
                clienteExistente.Direccion = nuevosDatos.Direccion;
                clienteExistente.Telefono = nuevosDatos.Telefono;
                clienteExistente.IngresoMensual = nuevosDatos.IngresoMensual;
                clienteExistente.TipoDeCliente = nuevosDatos.TipoDeCliente;
                clienteExistente.Usuario = nuevosDatos.Usuario;
                clienteExistente.Contrasena = nuevosDatos.Contrasena;

                holas.GuardarClientes(clientes);

                Console.WriteLine($"✅ Cliente con cédula {cedula} actualizado correctamente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un cliente con cédula {cedula}.");
            }
        }
    }
}
