namespace TECBank_BackEnd.Models
{
    public class CuentaModel
    {   
        public string NúmeroDeCuenta { get; set; }
        public string Descripción { get; set; }
        public string Moneda { get; set; }
        public string TipoDeCuenta { get; set; }
        public string Nombre { get; set; }
        public List<TarjetaModel> Tarjetas { get; set; } = new List<TarjetaModel>();
        public List<MovimientoModel> Movimientos { get; set; } = new List<MovimientoModel>();

        public void Retirar(decimal monto) { /* Implementación */ }
        public void Depositar(decimal monto) { /* Implementación */ }
        public void Transferir(CuentaModel cuentaDestino, decimal monto) { /* Implementación */ }
        public void PagarTarjeta(TarjetaModel tarjeta, decimal monto) { /* Implementación */ }
        public List<MovimientoModel> DarListaCompras() { /* Implementación */ return new List<MovimientoModel>(); }
        public List<MovimientoModel> DarMovimientos() { /* Implementación */ return new List<MovimientoModel>(); }
        public void GenerarReporte() { /* Implementación */ }
    }
}
