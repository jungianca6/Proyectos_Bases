namespace TECBank_BackEnd.Models
{
    public class CuentaModel
    {   
        public string NumeroDeCuenta { get; set; }
        public string Descripción { get; set; }
        public string Moneda { get; set; }
        public string TipoDeCuenta { get; set; }
        public string Nombre { get; set; }

        public void Retirar(decimal monto) { /* Implementación */ }
        public void Depositar(decimal monto) { /* Implementación */ }
        public void Transferir(CuentaModel cuentaDestino, decimal monto) { /* Implementación */ }
        public void PagarTarjeta(TarjetaModel tarjeta, decimal monto) { /* Implementación */ }
        public void GenerarReporte() { /* Implementación */ }
    }
}
