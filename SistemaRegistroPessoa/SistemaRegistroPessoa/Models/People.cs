using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaRegistroPessoa.Models
{
    public class People
    {
        public People()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        
        [StringLength(11, ErrorMessage = "O CPF deve conter 11 caracteres"), MinLength(11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "O UF é obrigatório")]
        public EnumUf Uf { get; set; }
    }

}
