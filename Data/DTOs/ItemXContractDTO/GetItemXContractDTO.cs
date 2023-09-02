using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ItemXContractDTO
{
    public class GetItemXContractDTO
    {
        public string Item { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total 
        {
            get
            {
                return (double) Price * Quantity;
            }
        }
    }
}
