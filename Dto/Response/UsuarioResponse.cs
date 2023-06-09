using System.ComponentModel.DataAnnotations;

namespace SafeLearn.Dto
{
    public class UsuarioResponse
    {
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string NomeMae { get; set; }

        public string Foto { get; set; }

        [Required]
        public string Senha { get; set; }
        public bool Suspeito { get; set; }
    }
}
