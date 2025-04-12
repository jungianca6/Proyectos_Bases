using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Data_Input_Models
{
    public class DepositoDataInputModel : MovimientoModel
    {
        public string CuentaEmisora { get; set; }
        public string CuentaDestino { get; set; }

    }
}
