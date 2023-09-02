using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.SchoolDTO
{
    public class CreateSchoolDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Level { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
    }
}
