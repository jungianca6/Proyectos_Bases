namespace TECBank_BackEnd.Models
{
    public class DepositoModel
    {
        public CuentaModel CuentaEmisora { get; set; }
        public CuentaModel CuentaDestino { get; set; }

        public void RealizarDepósito() { /* Implementación */ }
    }

}
