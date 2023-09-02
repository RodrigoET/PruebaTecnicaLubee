using Data.DTOs.ItemXContractDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ContractDTO
{
    public class GetContractDTO
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string School { get; set; }
        public string SchoolLevel { get; set; }
        public string SchoolGrade { get; set; }
        public string SchoolLocation { get; set; }
        public double Total { get; set; }
        public DateTime DeliveryDate { get; set; }

        public List<GetItemXContractDTO> Detail { get; set; } = new List<GetItemXContractDTO>();
    }
}
