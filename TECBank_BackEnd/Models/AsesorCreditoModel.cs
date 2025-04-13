namespace TECBank_BackEnd.Models
{
    public class AsesorCreditoModel
    {

        public string Cedula { get; set; }


        public int Meta_Colones { get; set; }
        public List<int> Meta_Creditos { get; set; }


        public void EmitirReporte() { /* Implementación */ }
        public void GenerarPrestamo() { /* Implementación */ }
    }
}
