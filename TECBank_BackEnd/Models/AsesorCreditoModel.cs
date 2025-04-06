namespace TECBank_BackEnd.Models
{
    public class AsesorCreditoModel
    {
        public decimal MetaVentas { get; set; }
        public decimal Comision { get; set; }
        public decimal MetaCreditos { get; set; }

        public void EmitirReporte() { /* Implementación */ }
        public void GenerarPrestamo() { /* Implementación */ }
    }
}
