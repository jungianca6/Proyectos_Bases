namespace TECBank_BackEnd.Models
{
    public class PrestamoModel
    {
        public decimal MontoOriginal { get; set; }
        public decimal Saldo { get; set; }
        public ClienteModel Cliente { get; set; }
        public decimal TasaDeInterés { get; set; }
        public List<DateTime> CalendarioDePagos { get; set; } = new List<DateTime>();
        public DateTime FechaVencimiento { get; set; }

        public void CalcularPagos() { /* Implementación */ }
        public void RealizarPagos(decimal monto) { /* Implementación */ }
        public void RealizarPagosExtraordinarios(decimal monto) { /* Implementación */ }
        public void RecalcularPagos() { /* Implementación */ }
    }
}
