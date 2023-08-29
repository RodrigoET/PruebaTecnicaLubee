using AutoMapper;
using Data.DTOs.SchoolDTO;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Data.Repositories.SchoolRepo
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly PruebaContext _context;
        private readonly IMapper _mapper;

        public SchoolRepository(PruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetSchoolDTO>> GetAllSchools()
        {
            IEnumerable<School> schoolsList = await _context.Schools.Where(s => !s.Deleted).ToListAsync();

            return _mapper.Map<IEnumerable<GetSchoolDTO>>(schoolsList);
            //throw new NotImplementedException();
        }

        public Task<GetSchoolDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetSchoolDTO> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateSchool(CreateSchoolDTO createDTO)
        {
            School modelo = _mapper.Map<School>(createDTO);
            bool creado = false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Schools.AddAsync(modelo);
                    creado = await _context.SaveChangesAsync() > 0;

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            
            return creado;
            //throw new NotImplementedException();
        }

        public async Task<bool> UpdateSchool(CreateSchoolDTO school)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteSchool(int id)
        {
            bool eliminado = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var modelo = await _context.Schools.Where(s => s.Id == id).FirstOrDefaultAsync();
                    if (modelo != null)
                    {
                        modelo.Deleted = true;
                        _context.Schools.Update(modelo);
                        eliminado = await _context.SaveChangesAsync() > 0;
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return eliminado;
        }

        //public async Task<bool> HardDeleteSchool(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return false;
        //    }
        //    var villa = await _context.
           
        //}
    }
}
