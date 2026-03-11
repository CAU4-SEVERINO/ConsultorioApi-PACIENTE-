using System.ComponentModel.DataAnnotations;

namespace ConsultorioApi.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Cpf { get; set; }
    }
}