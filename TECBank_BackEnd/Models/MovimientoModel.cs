namespace TECBank_BackEnd.Models
{
    public class MovimientoModel
    {
        public string Nombre { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Fecha { get; set; }
        public string ID { get; set; }
        public string Moneda { get; set; }

        public void GenerarMovimiento() { /* Implementación */ }
    }
}
