using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Data_Input_Models
{
    public class DepositoDataInputModel
    {
        public string Nombre { get; set; }

        public string NumeroDeCuenta { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public float Monto { get; set; }
        public string ID { get; set; }
        public string Moneda { get; set; }
        public CuentaModel CuentaEmisora { get; set; }
        public CuentaModel CuentaDestino { get; set; }

    }
}
