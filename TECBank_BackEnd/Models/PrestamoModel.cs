namespace TECBank_BackEnd.Models
{
    public class PrestamoModel
    {
        public int Monto_Original { get; set; }
        public int Saldo_Pendiente { get; set; }
        public string Cedula_Cliete { get; set; }
        public decimal Tasa_De_Interes { get; set; }
        public string ID_Prestamos { get; set; }

        public int Pagos { get; set; }

        public string FechaVencimiento { get; set; }

        public void CalcularPagos() { /* Implementación */ }
        public void RealizarPagos(decimal monto) { /* Implementación */ }
        public void RealizarPagosExtraordinarios(decimal monto) { /* Implementación */ }
        public void RecalcularPagos() { /* Implementación */ }
    }
}
