namespace TECBank_BackEnd.Models
{
    public class PagoPrestamoModel : MovimientoModel
    {
        public String CuentaEmisora { get; set; }
        public String IdPrestamo { get; set; }

        public int Monto { get; set; }

    }


}
