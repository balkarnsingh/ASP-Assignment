using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Cast
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public string Biography { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
