using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        public bool Deleted { get; set; } = false;

        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
