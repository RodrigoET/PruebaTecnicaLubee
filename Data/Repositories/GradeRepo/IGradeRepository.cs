using Data.DTOs.GradeDTO;
using Data.DTOs.SchoolDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.GradesRepo
{
    public interface IGradeRepository
    {
        Task<IEnumerable<GetGradeDTO>> GetAllGrades();
        Task<GetGradeDTO> GetGradeById(int id);
        Task<IEnumerable<GetGradeDTO>> GetGradeByName(string name);
        Task<int> CreateGrade(CreateGradeDTO createDTO);
        Task<bool> UpdateGrade(UpdateGradeDTO updateDTO);
        Task<bool> SoftDeleteGrade(int id);
        Task<bool> HardDeleteGrade(int id);
    }
}
