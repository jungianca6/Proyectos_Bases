namespace TECBank_BackEnd.Models
{
    public class TarjetaModel
    {
        public string Número { get; set; }
        public string TipoDeTarjeta { get; set; }
        public DateTime FechaDeExportación { get; set; }
        public string CódigoDeSeguridad { get; set; }
        public decimal SaldoDisponible { get; set; }

        public void IngresarTarjeta() { /* Implementación */ }
        public void ModificarTarjeta() { /* Implementación */ }
        public void EliminarTarjeta() { /* Implementación */ }
    }
}
