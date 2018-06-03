using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoLocadora.Models
{
    public class Location
    {
        public int Id { get; set; }
        
        public List<Movie> MovieList { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Preencha o CPF")]
        public string CPF { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime CreationDate { get; set; }
    }
}