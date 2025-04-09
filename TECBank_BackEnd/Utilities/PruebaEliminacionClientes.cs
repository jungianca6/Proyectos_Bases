using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaEliminacionClientes
    {
        public void EliminarPorCedula(string cedula)
        {
            Holas holas = new Holas();
            var clientes = holas.LeerClientes();

            var clienteAEliminar = clientes.FirstOrDefault(c => c.Cedula == cedula);

            if (clienteAEliminar != null)
            {
                clientes.Remove(clienteAEliminar);
                holas.GuardarClientes(clientes);
                Console.WriteLine($"🗑️ Cliente con cédula {cedula} eliminado correctamente.");
            }
            else
            {
                Console.WriteLine($"⚠️ No se encontró un cliente con cédula {cedula}.");
            }
        }

       
    }
}
