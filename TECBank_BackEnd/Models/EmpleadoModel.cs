namespace TECBank_BackEnd.Models
{
    public class EmpleadoModel
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }

        public string DescripcionDeRol { get; set; }


        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cedula { get; set; }
        public bool AdminRol { get; set; }
        public string FechaDeNacimiento { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
