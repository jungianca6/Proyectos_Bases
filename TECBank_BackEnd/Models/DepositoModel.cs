namespace TECBank_BackEnd.Models
{
    public class DepositoModel : MovimientoModel
    {
        public string CuentaEmisora { get; set; }
        public string CuentaDestino { get; set; }

        public string Monto { get; set; }

        public void RealizarDepósito() { /* Implementación */ }
    }

}
