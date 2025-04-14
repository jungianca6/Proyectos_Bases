using TECBank_BackEnd.Models;

namespace TECBank_BackEnd.Data_Input_Models
{
    public class AgregarAsesorDeCreditoDataInputModel
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
        public string Contrasena { get; set; }

        public int IngresoMensual { get; set; }

        public int Meta_Colones { get; set; }
        public List<int> Meta_Creditos { get; set; }

    }
}
