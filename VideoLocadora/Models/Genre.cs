using System;
using System.ComponentModel.DataAnnotations;

namespace VideoLocadora.Models
{
    public class Genre
    {
        public int Id { get; set; }

		[Display(Name = "Nome")]
		[Required(ErrorMessage = "Preencha o nome")]
        public string Name { get; set; }

        public DateTime CreationDate  { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}