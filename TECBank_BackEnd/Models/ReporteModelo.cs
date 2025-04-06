namespace TECBank_BackEnd.Models
{
    public class ReporteModelo
    {
        public string TipoDeReporte { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cédula { get; set; }
        public ReporteMetaModel ReporteMeta { get; set; }

        public void EmitirReporte() { /* Implementación */ }
    }
}
