namespace TECBank_BackEnd.Models
{
    public class TransferenciaModel : MovimientoModel
    {
        public String Cuenta_Emisora { get; set; }
        public String Cuenta_Receptora { get; set; }
        public int Monto { get; set; }
    }
}
