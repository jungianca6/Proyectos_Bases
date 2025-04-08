using System.Text.Json;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Utilities
{
    public class Holas
    {
        private readonly string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly string archivo;

        public Holas()
        {
            // Asegura que la carpeta Data exista
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            archivo = Path.Combine(carpeta, "Cliente.json");
        }

        public List<ClienteModel> LeerClientes()
        {
            if (!File.Exists(archivo))
                return new List<ClienteModel>();

            var json = File.ReadAllText(archivo);
            return JsonSerializer.Deserialize<List<ClienteModel>>(json) ?? new List<ClienteModel>();
        }

        public void GuardarClientes(List<ClienteModel> clientes)
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivo, json);
        }
    }
}
