namespace ConsultorioApi.DTOs
{
    public class ConsultaResponseDTO
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacoes { get; set; }

        public string PacienteNome { get; set; }
        public string MedicoNome { get; set; }
        public string ConsultorioNome { get; set; }
    }
}
