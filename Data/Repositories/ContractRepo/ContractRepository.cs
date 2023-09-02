using AutoMapper;
using Data.DTOs.ContractDTO;
using Data.DTOs.GradeDTO;
using Data.DTOs.ItemXContractDTO;
using DB;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ContractRepo
{
    public class ContractRepository : IContractRepository
    {
        private readonly PruebaContext _context;
        private readonly IMapper _mapper;

        public ContractRepository(PruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateContract(CreateContractDTO createDTO)
        {
            Contract contract = _mapper.Map<Contract>(createDTO);
            List<ItemXContract> itemsDetail = new List<ItemXContract>();
            double totalContract = 0;
            int quantityShipped = 0;
            foreach (var itemDTO in createDTO.Detail)
            {
                ItemXContract item = _mapper.Map<ItemXContract>(itemDTO);
                double itemPrecio = _context.Items.Where(i => i.Id == itemDTO.ItemId).FirstOrDefault().Price;

                item.Contract = contract;
                totalContract += itemPrecio * item.Quantity;
                quantityShipped += item.Quantity;
                itemsDetail.Add(item);
            }
            contract.QuantityShipped = quantityShipped;
            contract.Total = totalContract;
            
            contract.ItemXContract = itemsDetail.ToArray();
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract.Id;
        }

        public async Task<IEnumerable<GetContractDTO>> GetAllContracts()
        {
            throw new NotImplementedException();
        }

        public async Task<GetContractDTO> GetContractById(int id)
        {
            Contract contract = await _context.Contracts
                .Include(c => c.Grade)
                .ThenInclude(g => g.School)
                .Include(c => c.ItemXContract)
                .SingleOrDefaultAsync(c => c.Id == id);

            GetContractDTO contractDTO = _mapper.Map<GetContractDTO>(contract);

            //var itemsXContract = await _context.ItemXContract.Where(i => i.Id == id).ToListAsync();

            var itemsXContract = await _context.ItemXContract
                .Include(i => i.Item)
                .Where(i => i.ContractId == id).ToListAsync();

            foreach (var item in itemsXContract)
            {
                contractDTO.Detail.Add(_mapper.Map<GetItemXContractDTO>(item));
            }

            return (contractDTO);
        }

        public async Task<IEnumerable<GetContractDTO>> GetContractByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HardDeleteContract(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteContract(int id)
        {
            throw new NotImplementedException();
        }
    }
}
