using System.ComponentModel.DataAnnotations;

namespace VideoLocadora.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Preencha o e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}