namespace TECBank_BackEnd.Models
{
    public class ClienteModel
    {
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal IngresoMensual { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string TipoDeCliente { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public bool AdminRol { get; set; }

    }
}
