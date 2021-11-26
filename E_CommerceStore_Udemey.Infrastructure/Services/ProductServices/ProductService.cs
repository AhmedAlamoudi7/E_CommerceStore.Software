using AutoMapper;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using FourEstate.Infrastructure.AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.Infrastructure.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public async Task<List<ProductViewModel>> GetAll() {
            var product = await _Db.Products.Include(x=>x.Category).Include(x=>x.CoverType).ToListAsync();
            var mapper = _mapper.Map<List<ProductViewModel>>(product);

            return mapper;
        }


        public async Task<int> Create(CreateProductDto dto) {
            var mapper = _mapper.Map<CreateProductDto, Product>(dto);
            await _Db.Products.AddAsync(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

        //public async Task<int> Update(UpdateContractDto dto)
        //{
        //    var contract = await _db.Contracts.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
        //    if (contract == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }
        //    var updatedcontract = _mapper.Map<UpdateContractDto, Contract>(dto, contract);
        //    _db.Contracts.Update(updatedcontract);
        //    await _db.SaveChangesAsync();
        //    return updatedcontract.Id;
        //}


        public async Task<int> Update(UpdateProductDto dto) {
            var product =await _Db.Products.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateProductDto, Product>(dto,product);
            _Db.Products.Update(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

        public async Task<UpdateProductDto> Get(int Id)
        {
            var product = await _Db.Products.SingleOrDefaultAsync(x => x.Id == Id /*&& !x.IsDelete*/);
            //if (category == null)
            //{
            //    throw new EntityNotFoundException();
            //}
            return _mapper.Map<UpdateProductDto>(product);
        }

        public async Task<int> Delete(UpdateProductDto dto)
        {
            var product = await _Db.Products.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateProductDto, Product>(dto, product);
            _Db.Products.Remove(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

    }
}
