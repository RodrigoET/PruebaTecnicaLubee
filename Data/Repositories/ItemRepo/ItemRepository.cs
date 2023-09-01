using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTOs.ItemDTO;
using DB;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ItemRepo
{
    public class ItemRepository : IItemRepository
    {
        private readonly PruebaContext _context;
        private readonly IMapper _mapper;

        public ItemRepository(PruebaContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetItemDTO>> GetAllItems()
        {
            var itemsList = await _context.Items
                .Where(i => !i.Deleted)
                .ProjectTo<GetItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return itemsList;
        }

        public async Task<GetItemDTO> GetItemById(int id)
        {
            var item = await _context.Items
                .Where(i => !i.Deleted && i.Id == id)
                .ProjectTo<GetItemDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return item;
        }

        public async  Task<IEnumerable<GetItemDTO>> GetItemByName(string name)
        {
            var item = await _context.Items
                .Where(i => !i.Deleted && i.Name.Contains(name))
                .ProjectTo<GetItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return item;
        }

        public async Task<int> CreateItem(CreateItemDTO createDTO)
        {
            Item model = _mapper.Map<Item>(createDTO);
            int createdId = 0;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Items.AddAsync(model);
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

        public async Task<bool> UpdateItem(UpdateItemDTO updateDTO)
        {
            bool updated = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var item = _context.Items.AsNoTracking().FirstOrDefault(s => s.Id == updateDTO.Id);
                    if (item != null)
                    {
                        item = _mapper.Map<Item>(updateDTO);
                        _context.Items.Update(item);
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

        public async Task<bool> SoftDeleteItem(int id)
        {
            bool deleted = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var model = await _context.Items.Where(s => s.Id == id).FirstOrDefaultAsync();
                    if (model != null)
                    {
                        model.Deleted = true;
                        _context.Items.Update(model);
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

        public async Task<bool> HardDeleteItem(int id)
        {
            bool deleted = false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Item Item = await _context.Items.FindAsync(id);
                    if (Item != null)
                    {
                        _context.Remove(Item);
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
