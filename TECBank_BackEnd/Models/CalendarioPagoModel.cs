namespace TECBank_BackEnd.Models
{
    public class CalendarioPagoModel
    {
        public string ID_Prestamo { get; set; }
        public string FechaVencimiento { get; set; }
        public int SaldoPendiente { get; set; }
        public List<CuotaMensual> CuotasMensuales { get; set; }
    }

    public class CuotaMensual
    {
        public string Mes { get; set; }
        public int Anio { get; set; }
        public string FechaPago { get; set; }
        public int MontoAPagar { get; set; }
    }
}
