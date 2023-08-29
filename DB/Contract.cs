using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(6)]
        public string CourseCode { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        public int NumberOfGraduates { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int DeliveryMethodId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int GradeId { get; set; }

        public int Commission { get; set; }

        public double Total { get; set; }
    }
}
