using Data.DTOs.GradeDTO;
using Data.DTOs.ItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ItemRepo
{
    public interface IItemRepository
    {
        Task<IEnumerable<GetItemDTO>> GetAllItems();
        Task<GetItemDTO> GetItemById(int id);
        Task<IEnumerable<GetItemDTO>> GetItemByName(string name);
        Task<int> CreateItem(CreateItemDTO createDTO);
        Task<bool> UpdateItem(UpdateItemDTO updateDTO);
        Task<bool> SoftDeleteItem(int id);
        Task<bool> HardDeleteItem(int id);
    }
}
