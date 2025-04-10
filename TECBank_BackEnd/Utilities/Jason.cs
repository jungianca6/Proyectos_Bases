using System.Text.Json;
using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Utilities
{
    public class Jason
    {
        private readonly string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly string archivoClientes;
        private readonly string archivoCuentas;
        private readonly string archivoTarjetas;

        public Jason()
        {
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            archivoClientes = Path.Combine(carpeta, "Cliente.json");
            archivoCuentas = Path.Combine(carpeta, "Cuentas.json");
            archivoTarjetas = Path.Combine(carpeta, "Tarjetas.json");
        }

        // CLIENTES
        public List<ClienteModel> LeerClientes()
        {
            if (!File.Exists(archivoClientes))
                return new List<ClienteModel>();

            var json = File.ReadAllText(archivoClientes);
            if (string.IsNullOrWhiteSpace(json))
                return new List<ClienteModel>();

            return JsonSerializer.Deserialize<List<ClienteModel>>(json) ?? new List<ClienteModel>();
        }

        public void GuardarClientes(List<ClienteModel> clientes)
        {
            var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoClientes, json);
        }

        // CUENTAS
        public List<CuentaModel> LeerCuentas()
        {
            if (!File.Exists(archivoCuentas))
                return new List<CuentaModel>();

            var json = File.ReadAllText(archivoCuentas);
            if (string.IsNullOrWhiteSpace(json))
                return new List<CuentaModel>();

            return JsonSerializer.Deserialize<List<CuentaModel>>(json) ?? new List<CuentaModel>();
        }

        public void GuardarCuentas(List<CuentaModel> cuentas)
        {
            var json = JsonSerializer.Serialize(cuentas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoCuentas, json);
        }

        // TARJETAS
        public List<TarjetaModel> LeerTarjetas()
        {
            if (!File.Exists(archivoTarjetas))
                return new List<TarjetaModel>();

            var json = File.ReadAllText(archivoTarjetas);
            if (string.IsNullOrWhiteSpace(json))
                return new List<TarjetaModel>();

            return JsonSerializer.Deserialize<List<TarjetaModel>>(json) ?? new List<TarjetaModel>();
        }

        public void GuardarTarjetas(List<TarjetaModel> tarjetas)
        {
            var json = JsonSerializer.Serialize(tarjetas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoTarjetas, json);
        }
    }
}
