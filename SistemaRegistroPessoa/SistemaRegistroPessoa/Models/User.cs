using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaRegistroPessoa.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "O email é obrigatória")]
        [EmailAddress(ErrorMessage = "O formato não é valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A senha deve conter no mínimo 8 caracteres", MinimumLength = 8)]
        public string Password { get; set; }
    }

    public class RegisterUser
    {
        [Required(ErrorMessage = "O email é obrigatória")]
        [EmailAddress(ErrorMessage = "O formato não é valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A senha deve conter no mínimo 8 caracteres", MinimumLength = 8)]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

    }
}
