namespace TECBank_BackEnd.Models
{
    public class AsesorCreditoModel
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public string Cedula { get; set; }

        public string Fecha_de_Nacimiento { get; set; }

        public int Meta_Colones { get; set; }
        public List<int> Meta_Creditos { get; set; }


        public void EmitirReporte() { /* Implementación */ }
        public void GenerarPrestamo() { /* Implementación */ }
    }
}
