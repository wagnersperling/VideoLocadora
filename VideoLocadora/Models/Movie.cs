using System;
using System.ComponentModel.DataAnnotations;

namespace VideoLocadora.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }

        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Não há genero selecionado")]
        [Display(Name = "Genero")]
        public int IdGenre { get; set; }
    }
}