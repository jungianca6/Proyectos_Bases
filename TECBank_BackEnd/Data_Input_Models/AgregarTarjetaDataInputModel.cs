using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Data_Input_Models
{
    public class AgregarTarjetaDataInputModel
    {
        public string numeroDeTarjeta { get; set; }

        public string tipoDeTarjeta { get; set; }

        public string fechaDeExpiracion { get; set; }
        public string CCV { get; set; }
        public int saldo { get; set; }

        public string numeroDeCuenta { get; set; }

    }
}
