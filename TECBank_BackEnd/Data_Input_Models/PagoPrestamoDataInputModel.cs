namespace TECBank_BackEnd.Data_Input_Models
{
    public class PagoPrestamoDataInputModel
    {
        public string Nombre { get; set; }

        public string NumeroDeCuenta { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Monto { get; set; }
        public string Moneda { get; set; }
        public String IdPrestamo { get; set; }

    }
}
