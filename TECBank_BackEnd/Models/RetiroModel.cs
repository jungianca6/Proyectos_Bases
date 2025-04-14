namespace TECBank_BackEnd.Models
{
    public class RetiroModel : MovimientoModel
    {
        public string CuentaARetirar { get; set; }

        public int Monto { get; set; }
    }
}
