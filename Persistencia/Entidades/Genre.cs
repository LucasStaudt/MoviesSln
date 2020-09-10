using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistencia.Entidades
{
    public class Genre
    {
        public int GenreId { get; set; }

        [MaxLength(40)]
        public string Name {get; set; }
        public string Description { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
