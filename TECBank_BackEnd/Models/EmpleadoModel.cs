namespace TECBank_BackEnd.Models
{
    public class EmpleadoModel
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cédula { get; set; }
        public string Rol { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
