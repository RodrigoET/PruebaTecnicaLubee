using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ContractDTO
{
    internal class CreateDetailContractDTO
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
