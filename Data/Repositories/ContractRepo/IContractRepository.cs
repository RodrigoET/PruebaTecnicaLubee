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
        Task<IEnumerable<GetContractDTO>> GetAllGrades();
        Task<GetGradeDTO> GetGradeById(int id);
        Task<IEnumerable<GetGradeDTO>> GetGradeByName(string name);
        Task<bool> CreateGrade(CreateGradeDTO createDTO);
        Task<bool> UpdateGrade(UpdateGradeDTO updateDTO);
        Task<bool> SoftDeleteGrade(int id);
        Task<bool> HardDeleteGrade(int id);
    }
}
