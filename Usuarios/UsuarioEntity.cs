using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLearn.Usuario
{
    [Table("Usuario")]
    public class UsuarioEntity
    {
        [Key]
        [Column("ID")]
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
    }
}
