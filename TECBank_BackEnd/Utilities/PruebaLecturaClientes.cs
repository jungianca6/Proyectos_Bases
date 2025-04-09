using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class PruebaLecturaClientes
    {
        // 🟢 Lista accesible públicamente con todos los datos de los clientes leídos
        public List<ClienteModel> ListaClientesLeidos { get; private set; } = new List<ClienteModel>();

        public List<ClienteModel> Ejecutar(string? etiqueta = null, string? valor = null)
        {
            Holas holas = new Holas();
            var clientes = holas.LeerClientes();

            // Filtrar si se especifica etiqueta y valor
            if (!string.IsNullOrWhiteSpace(etiqueta) && !string.IsNullOrWhiteSpace(valor))
            {
                clientes = clientes.Where(c =>
                    (etiqueta.Equals("Cedula", StringComparison.OrdinalIgnoreCase) && c.Cedula?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("Nombre", StringComparison.OrdinalIgnoreCase) && c.Nombre?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("Apellido1", StringComparison.OrdinalIgnoreCase) && c.Apellido1?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("Apellido2", StringComparison.OrdinalIgnoreCase) && c.Apellido2?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("Direccion", StringComparison.OrdinalIgnoreCase) && c.Direccion?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("Telefono", StringComparison.OrdinalIgnoreCase) && c.Telefono?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("TipoDeCliente", StringComparison.OrdinalIgnoreCase) && c.TipoDeCliente?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("Usuario", StringComparison.OrdinalIgnoreCase) && c.Usuario?.Contains(valor, StringComparison.OrdinalIgnoreCase) == true) ||
                    (etiqueta.Equals("IngresoMensual", StringComparison.OrdinalIgnoreCase) && c.IngresoMensual.ToString() == valor)
                ).ToList();
            }

            // Guardar los clientes completos en la lista interna
            ListaClientesLeidos = clientes;

            // Mostrar solo algunos datos en consola
            Console.WriteLine("📄 Clientes encontrados:");
            if (clientes.Count == 0)
            {
                Console.WriteLine("⚠️ No se encontraron clientes con ese criterio.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"➡️ {cliente.Nombre} {cliente.Apellido1} | Usuario: {cliente.Usuario} | Cédula: {cliente.Cedula}");
                }
            }

            return clientes;
        }
    }
}
