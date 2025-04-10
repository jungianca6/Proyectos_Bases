using System.Text.Json;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Utilities
{
    public class Jason
    {
        private readonly string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly string archivoClientes;
        private readonly string archivoCuentas;

        public Jason()
        {
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            archivoClientes = Path.Combine(carpeta, "Cliente.json");
            archivoCuentas = Path.Combine(carpeta, "Cuentas.json");
        }

        // Métodos para CLIENTES
        public List<ClienteModel> LeerClientes()
        {
            if (!File.Exists(archivoClientes))
                return new List<ClienteModel>();

            var json = File.ReadAllText(archivoClientes);

            // Verificar si el archivo está vacío
            if (string.IsNullOrWhiteSpace(json))
                return new List<ClienteModel>();

            return JsonSerializer.Deserialize<List<ClienteModel>>(json) ?? new List<ClienteModel>();
        }

        public void GuardarClientes(List<ClienteModel> clientes)
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoClientes, json);
        }

        // Métodos para CUENTAS
        public List<CuentaModel> LeerCuentas()
        {
            if (!File.Exists(archivoCuentas))
                return new List<CuentaModel>();

            var json = File.ReadAllText(archivoCuentas);

            // Verificar si el archivo está vacío
            if (string.IsNullOrWhiteSpace(json))
                return new List<CuentaModel>();

            return JsonSerializer.Deserialize<List<CuentaModel>>(json) ?? new List<CuentaModel>();
        }

        public void GuardarCuentas(List<CuentaModel> cuentas)
        {
            var json = JsonSerializer.Serialize(cuentas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoCuentas, json);
        }
    }
}
