﻿namespace TECBank_BackEnd.Models
{
    public class TarjetaModel
    {
        public string Numero { get; set; }
        public string NumeroDeCuenta { get; set; }

        public string TipoDeTarjeta { get; set; }
        public string FechaDeExpiracion { get; set; }
        public string CCV { get; set; }
        public int SaldoDisponible { get; set; }


        public void IngresarTarjeta() { /* Implementación */ }
        public void ModificarTarjeta() { /* Implementación */ }
        public void EliminarTarjeta() { /* Implementación */ }
    }
}
