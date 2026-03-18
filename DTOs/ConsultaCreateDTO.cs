namespace ConsultorioApi.DTOs
{
    public class ConsultaCreateDTO
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacoes { get; set; }
    }
}
