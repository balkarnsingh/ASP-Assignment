using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Movie Title can not exceed 50 characters.")]
        [Required]
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public double Rating { get; set; }
        [MaxLength(1024, ErrorMessage = "Movie Title can not exceed 1024 characters.")]
        [Required]
        public string Description { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
    }
}
