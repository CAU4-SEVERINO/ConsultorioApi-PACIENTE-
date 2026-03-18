using System.Text.Json.Serialization;

namespace ConsultorioApi.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        [JsonIgnore]
        public Paciente? Paciente { get; set; }
        public int MedicoId { get; set; }
        [JsonIgnore]
        public Medico? Medico { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacoes { get; set; }
    }
}
