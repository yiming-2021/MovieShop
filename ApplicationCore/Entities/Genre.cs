using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; } // attribute named 'Id' will be automatically set PK by default of migration

        [MaxLength(64)]
        public string Name { get; set; }

        public ICollection<MovieGenre> Movies{ get; set; }

    }
}
