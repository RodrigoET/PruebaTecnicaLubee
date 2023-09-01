using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ContractDTO
{
    internal class CreateContractDTO
    {
        [Required]
        [StringLength(6)]
        public string CourseCode { get; set; }
        public int StatusId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryMethodId { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        public int GradeId { get; set; }
        public int Commission { get; set; }
        
        public List<CreateDetailContractDTO> Detail { get; set; } = new List<CreateDetailContractDTO>();
    }
}
