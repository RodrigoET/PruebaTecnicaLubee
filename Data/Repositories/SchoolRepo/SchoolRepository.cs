﻿using AutoMapper;
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

        public async Task<GetSchoolDTO> GetSchoolById(int id)
        {
            School school = await _context.Schools.Where(s => s.Id == id && !s.Deleted).FirstOrDefaultAsync();
            return _mapper.Map<GetSchoolDTO>(school);

        }
        public async Task<IEnumerable<GetSchoolDTO>> GetSchoolByName(string name)
        {
            IEnumerable<School> schoolsList = await _context.Schools.Where(s => s.Name.Contains(name) && !s.Deleted).ToListAsync();

            return _mapper.Map<IEnumerable<GetSchoolDTO>>(schoolsList);
        }

        public async Task<int> CreateSchool(CreateSchoolDTO createDTO)
        {
            School model = _mapper.Map<School>(createDTO);
            int createdId = 0;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Schools.AddAsync(model);
                    await _context.SaveChangesAsync();
                    createdId = model.Id;
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

        public async Task<bool> UpdateSchool(UpdateSchoolDTO updateDTO)
        {
            bool updated = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var school = _context.Schools.AsNoTracking().FirstOrDefault(s=>s.Id == updateDTO.Id);
                    if (school != null)
                    {
                        school = _mapper.Map<School>(updateDTO);
                        _context.Schools.Update(school);
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

        public async Task<bool> SoftDeleteSchool(int id)
        {
            bool deleted = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var model = await _context.Schools.Where(s => s.Id == id).FirstOrDefaultAsync();
                    if (model != null)
                    {
                        model.Deleted = true;
                        _context.Schools.Update(model);
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

        public async Task<bool> HardDeleteSchool(int id)
        {
            bool deleted = false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    School school = await _context.Schools.FindAsync(id);
                    if (school != null)
                    {
                        _context.Remove(school);
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
