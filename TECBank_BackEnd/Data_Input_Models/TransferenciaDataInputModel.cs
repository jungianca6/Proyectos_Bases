﻿namespace TECBank_BackEnd.Data_Input_Models
{
    public class TransferenciaDataInputModel
    {
        public string Nombre { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Monto { get; set; }
        public string Moneda { get; set; }
        public String Cuenta_Emisora { get; set; }
        public String Cuenta_Receptora { get; set; }

    }
}
