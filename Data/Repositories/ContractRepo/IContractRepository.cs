using Data.DTOs.ContractDTO;
using Data.DTOs.GradeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ContractRepo
{
    public interface IContractRepository
    {
        Task<IEnumerable<GetContractDTO>> GetAllContracts();
        Task<GetContractDTO> GetContractById(int id);
        Task<IEnumerable<GetContractDTO>> GetContractByName(string name);
        Task<int> CreateContract(CreateContractDTO createDTO);
        Task<bool> SoftDeleteContract(int id);
        Task<bool> HardDeleteContract(int id);
    }
}
