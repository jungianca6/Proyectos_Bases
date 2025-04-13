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
        private readonly string archivoDepositos;
        private readonly string archivoEmpleados;
        private readonly string archivoPagos;
        private readonly string archivoRetiros;
        private readonly string archivoTransferencias; // <-- NUEVA LÍNEA
        private readonly string archivoAsesores; // <-- NUEVA LÍNEA
        private readonly string archivoPrestamos; // <-- NUEVA LÍNEA

        public Jason()
        {
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            archivoClientes = Path.Combine(carpeta, "Cliente.json");
            archivoCuentas = Path.Combine(carpeta, "Cuentas.json");
            archivoTarjetas = Path.Combine(carpeta, "Tarjetas.json");
            archivoDepositos = Path.Combine(carpeta, "Deposito.json");
            archivoEmpleados = Path.Combine(carpeta, "Empleados.json");
            archivoPagos = Path.Combine(carpeta, "Pago.json");
            archivoRetiros = Path.Combine(carpeta, "Retiro.json");
            archivoAsesores = Path.Combine(carpeta, "AsesoresCredito.json"); // <-- NUEVA LÍNEA
            archivoPrestamos = Path.Combine(carpeta, "Prestamo.json"); // <-- NUEVA LÍNEA

            archivoTransferencias = Path.Combine(carpeta, "Transferencia.json"); // <-- NUEVA LÍNEA
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

        // DEPOSITOS


        // EMPLEADOS
        public List<EmpleadoModel> LeerEmpleados()
        {
            if (!File.Exists(archivoEmpleados))
                return new List<EmpleadoModel>();

            var json = File.ReadAllText(archivoEmpleados);
            if (string.IsNullOrWhiteSpace(json))
                return new List<EmpleadoModel>();

            return JsonSerializer.Deserialize<List<EmpleadoModel>>(json) ?? new List<EmpleadoModel>();
        }

        public void GuardarEmpleados(List<EmpleadoModel> empleados)
        {
            var json = JsonSerializer.Serialize(empleados, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoEmpleados, json);
        }

        // PAGOS
        public List<PagoModel> LeerPagos()
        {
            if (!File.Exists(archivoPagos))
                return new List<PagoModel>();

            var json = File.ReadAllText(archivoPagos);
            if (string.IsNullOrWhiteSpace(json))
                return new List<PagoModel>();

            return JsonSerializer.Deserialize<List<PagoModel>>(json) ?? new List<PagoModel>();
        }

        public void GuardarPagos(List<PagoModel> pagos)
        {
            var json = JsonSerializer.Serialize(pagos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoPagos, json);
        }

        // RETIROS
        public List<RetiroModel> LeerRetiros()
        {
            if (!File.Exists(archivoRetiros))
                return new List<RetiroModel>();

            var json = File.ReadAllText(archivoRetiros);
            if (string.IsNullOrWhiteSpace(json))
                return new List<RetiroModel>();

            return JsonSerializer.Deserialize<List<RetiroModel>>(json) ?? new List<RetiroModel>();
        }

        public void GuardarRetiros(List<RetiroModel> retiros)
        {
            var json = JsonSerializer.Serialize(retiros, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoRetiros, json);
        }

        // TRANSFERENCIAS
        public List<TransferenciaModel> LeerTransferencias()
        {
            if (!File.Exists(archivoTransferencias))
                return new List<TransferenciaModel>();

            var json = File.ReadAllText(archivoTransferencias);
            if (string.IsNullOrWhiteSpace(json))
                return new List<TransferenciaModel>();

            return JsonSerializer.Deserialize<List<TransferenciaModel>>(json) ?? new List<TransferenciaModel>();
        }

        public void GuardarTransferencias(List<TransferenciaModel> transferencias)
        {
            var json = JsonSerializer.Serialize(transferencias, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoTransferencias, json);
        }

        public List<AsesorCreditoModel> LeerAsesoresCredito()
        {
            if (!File.Exists(archivoAsesores))
                return new List<AsesorCreditoModel>();

            var json = File.ReadAllText(archivoAsesores);
            if (string.IsNullOrWhiteSpace(json))
                return new List<AsesorCreditoModel>();

            return JsonSerializer.Deserialize<List<AsesorCreditoModel>>(json) ?? new List<AsesorCreditoModel>();
        }

        public void GuardarAsesoresCredito(List<AsesorCreditoModel> asesores)
        {
            var json = JsonSerializer.Serialize(asesores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoAsesores, json);
        }

        public List<PrestamoModel> LeerPrestamos()
        {
            if (!File.Exists(archivoPrestamos))
                return new List<PrestamoModel>();

            var json = File.ReadAllText(archivoPrestamos);
            if (string.IsNullOrWhiteSpace(json))
                return new List<PrestamoModel>();

            return JsonSerializer.Deserialize<List<PrestamoModel>>(json) ?? new List<PrestamoModel>();
        }

        public void GuardarPrestamos(List<PrestamoModel> prestamos)
        {
            var json = JsonSerializer.Serialize(prestamos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoPrestamos, json);
        }


    }
}
