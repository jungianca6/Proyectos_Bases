namespace TECBank_BackEnd.Models
{
    public class MovimientoModel
    {
        public string Nombre { get; set; }

        public string NumeroDeCuenta{ get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string ID { get; set; }
        public string Moneda { get; set; }

        public void GenerarMovimiento() { /* Implementación */ }
    }
}
