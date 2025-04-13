namespace TECBank_BackEnd.Models
{
    public class CalendarioPago
    {
        public int ID { get; set; }
        public int ID_Prestamo { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoAPagar { get; set; }
        public string Mes { get; set; }
        public int Anio { get; set; }
    }
}