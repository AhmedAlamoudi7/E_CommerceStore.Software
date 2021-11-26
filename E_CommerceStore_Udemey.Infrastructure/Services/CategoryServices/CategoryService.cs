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

namespace E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAll() {
            var category = await _Db.Categories.ToListAsync();
            var mapper = _mapper.Map<List<CategoryViewModel>>(category);

            return mapper;
        }


        public async Task<List<CategoryViewModel>> GetCategoryName()
        {
            var category = await _Db.Categories.ToListAsync();/*Where(/*x => !x.IsDelete)*/
            return _mapper.Map<List<CategoryViewModel>>(category);
        }


        public async Task<int> Create(CreateCategoryDto dto) {
            var mapper = _mapper.Map<CreateCategoryDto, Category>(dto);
            await _Db.Categories.AddAsync(mapper);
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


        public async Task<int> Update(UpdateCategoryDto dto) {
            var category =await _Db.Categories.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCategoryDto, Category>(dto,category);
            _Db.Categories.Update(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

        public async Task<UpdateCategoryDto> Get(int Id)
        {
            var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == Id /*&& !x.IsDelete*/);
            //if (category == null)
            //{
            //    throw new EntityNotFoundException();
            //}
            return _mapper.Map<UpdateCategoryDto>(category);
        }

        public async Task<int> Delete(UpdateCategoryDto dto)
        {
            var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCategoryDto, Category>(dto, category);
            _Db.Categories.Remove(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id; 
        }

    }
}
