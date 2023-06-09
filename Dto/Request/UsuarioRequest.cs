using SafeLearn.Usuario;
using System.ComponentModel.DataAnnotations;

namespace SafeLearn.Dto
{
    public class UsuarioRequest
    {
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

        public UsuarioEntity ToModel()
        {
            return new UsuarioEntity()
            {
                Nome = Nome,
                Sobrenome = Sobrenome,
                DataNascimento = DataNascimento,
                Foto = Foto,
                CPF = CPF,
                ID = 0,
                NomeMae = NomeMae,
                Senha = Senha,
            };
        }
    }
}
