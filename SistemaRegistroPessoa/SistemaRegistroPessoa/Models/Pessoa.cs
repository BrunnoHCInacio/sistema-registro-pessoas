using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaRegistroPessoa.Models
{
    public class Pessoa
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        
        [StringLength(11, ErrorMessage = "O CPF deve conter 11 caracteres"), MinLength(11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public EnumUf Uf { get; set; }

    }
}
