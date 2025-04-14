using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Data_Input_Models
{
    public class AgregarPrestamoDataInputModel
    {
        public int Monto_Original { get; set; }
        public string Cedula_Cliente { get; set; }
        public decimal Tasa_De_Interes { get; set; }
        public string FechaVencimiento { get; set; }

        public string Cedula_Asesor { get; set; }
    }
}
