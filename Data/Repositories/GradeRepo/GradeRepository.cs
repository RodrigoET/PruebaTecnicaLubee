using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTOs.GradeDTO;
using DB;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.GradesRepo
{
    public class GradeRepository : IGradeRepository
    {
        private readonly PruebaContext _context;
        private readonly IMapper _mapper;

        public GradeRepository(PruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetGradeDTO>> GetAllGrades()
        {
            var gradesList = await _context.Grades
                .Where(g => !g.Deleted)
                .ProjectTo<GetGradeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return gradesList;
        }

        public async Task<GetGradeDTO> GetGradeById(int id)
        {
            var grade = await _context.Grades
                .Where(g => !g.Deleted && g.Id == id)
                .ProjectTo<GetGradeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return grade;
        }

        public async Task<IEnumerable<GetGradeDTO>> GetGradeByName(string name)
        {
            var gradesList = await _context.Grades
                .Where(g => !g.Deleted && g.Name.Contains(name))
                .ProjectTo<GetGradeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return gradesList;
        }

        public async Task<int> CreateGrade(CreateGradeDTO createDTO)
        {
            Grade modelo = _mapper.Map<Grade>(createDTO);
            int createdId = 0;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Grades.AddAsync(modelo);
                    await _context.SaveChangesAsync();
                    createdId = modelo.Id;
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return createdId;
        }

        public async Task<bool> UpdateGrade(UpdateGradeDTO updateDTO)
        {
            bool updated = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var grade = _context.Grades.AsNoTracking().FirstOrDefault(g => g.Id == updateDTO.Id);
                    if (grade != null)
                    {
                        grade = _mapper.Map<Grade>(updateDTO);
                        _context.Grades.Update(grade);
                        updated = await _context.SaveChangesAsync() > 0;
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return updated;
        }

        public async Task<bool> SoftDeleteGrade(int id)
        {
            bool deleted = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var model = await _context.Grades.Where(s => s.Id == id).FirstOrDefaultAsync();
                    if (model != null)
                    {
                        model.Deleted = true;
                        _context.Grades.Update(model);
                        deleted = await _context.SaveChangesAsync() > 0;
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return deleted;
        }

        public async Task<bool> HardDeleteGrade(int id)
        {
            bool deleted = false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Grade grade = await _context.Grades.FindAsync(id);
                    if (grade != null)
                    {
                        _context.Remove(grade);
                        deleted = await _context.SaveChangesAsync() > 0;
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return deleted;
        }

        
    }
}
