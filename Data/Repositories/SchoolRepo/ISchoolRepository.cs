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
        Task<GetSchoolDTO> GetById(int id);
        Task<GetSchoolDTO> GetByName(string name);
        Task<bool> CreateSchool(CreateSchoolDTO school);
        Task<bool> UpdateSchool(CreateSchoolDTO school);
        Task<bool> SoftDeleteSchool(int id);
        Task<bool> HardDeleteSchool(int id);
    }
}
