using TECBank_BackEnd.Utilities;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaLecturaClientes
    {
        public static void Ejecutar()
        {
            var clientes = Holas.LeerClientes();

            Console.WriteLine("📄 Clientes leídos desde el archivo JSON:");
            if (clientes.Count == 0)
            {
                Console.WriteLine("⚠️ No se encontraron clientes en el archivo JSON.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"➡️ {cliente.Nombre} {cliente.Apellido1} | Usuario: {cliente.Usuario}");
                }
            }
        }
    }
}
