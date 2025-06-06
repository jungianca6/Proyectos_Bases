﻿using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Data_Input_Models
{
    public class RetiroDataInputModel
    {
        public string Nombre { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Monto { get; set; }
        public string ID { get; set; }
        public string Moneda { get; set; }
        public string CuentaARetirar { get; set; }

    }
}
