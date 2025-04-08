using TECBank_BackEnd.Utilities;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaEscrituraClientes
    {
        public void Ejecutar(ClienteModel cliente )
        {
            var clientes = Holas.LeerClientes();

            string cedulaNueva = cliente.Cedula;

            if (!clientes.Any(c => c.Cedula == cedulaNueva))
            {
                var nuevoCliente = cliente;


                clientes.Add(nuevoCliente);
                Holas.GuardarClientes(clientes);

                Console.WriteLine("✅ Cliente agregado y guardado en el archivo JSON.");
            }
            else
            {
                Console.WriteLine("⚠️ Ya existe un cliente con esta cédula en el archivo JSON.");
            }
        }
    }
}
