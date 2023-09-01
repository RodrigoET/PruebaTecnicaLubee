using Data.DTOs.SchoolDTO;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.SchoolRepo
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<GetSchoolDTO>> GetAllSchools();
        Task<GetSchoolDTO> GetSchoolById(int id);
        Task<IEnumerable<GetSchoolDTO>> GetSchoolByName(string name);
        Task<int> CreateSchool(CreateSchoolDTO createDTO);
        Task<bool> UpdateSchool(UpdateSchoolDTO updateDTO);
        Task<bool> SoftDeleteSchool(int id);
        Task<bool> HardDeleteSchool(int id);
    }
}
