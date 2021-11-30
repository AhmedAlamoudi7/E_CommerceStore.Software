using AutoMapper;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Exceptions;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using FourEstate.Infrastructure.AutoMapper;
using FourEstate.Infrastructure.Helpers;
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

        ////public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        ////{
        ////    var queryString = _Db.Categories.Where(x => (x.Name.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

        ////    var dataCount = queryString.Count();
        ////    var skipValue = pagination.GetSkipValue();
        ////    var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
        ////    var categories = _mapper.Map<List<CategoryViewModel>>(dataList);
        ////    var pages = pagination.GetPages(dataCount);
        ////    var result = new ResponseDto
        ////    {
        ////        data = categories,
        ////        meta = new Meta
        ////        {
        ////            page = pagination.Page,
        ////            perpage = pagination.PerPage,
        ////            pages = pages,
        ////            total = dataCount,
        ////        }
        ////    };
        ////    return result;
        ////}




        ////        public async Task<List<CategoryViewModel>> GetCategoryName()
        ////        {
        ////            var category = await _Db.Categories.ToListAsync();/*Where(/*x => !x.IsDelete)*/
        ////            return _mapper.Map<List<CategoryViewModel>>(category);
        ////        }


        ////        public async Task<int> Create(CreateCategoryDto dto) {
        ////            var mapper = _mapper.Map<CreateCategoryDto, Category>(dto);
        ////            await _Db.Categories.AddAsync(mapper);
        ////            await _Db.SaveChangesAsync();
        ////            return mapper.Id;
        ////        }

        ////        //public async Task<int> Update(UpdateContractDto dto)
        ////        //{
        ////        //    var contract = await _db.Contracts.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
        ////        //    if (contract == null)
        ////        //    {
        ////        //        throw new EntityNotFoundException();
        ////        //    }
        ////        //    var updatedcontract = _mapper.Map<UpdateContractDto, Contract>(dto, contract);
        ////        //    _db.Contracts.Update(updatedcontract);
        ////        //    await _db.SaveChangesAsync();
        ////        //    return updatedcontract.Id;
        ////        //}


        ////        public async Task<int> Update(UpdateCategoryDto dto) {
        ////            var category =await _Db.Categories.SingleOrDefaultAsync(x => x.Id == dto.Id);
        ////            var mapper = _mapper.Map<UpdateCategoryDto, Category>(dto,category);
        ////            _Db.Categories.Update(mapper);
        ////            await _Db.SaveChangesAsync();
        ////            return mapper.Id;
        ////        }

        ////        public async Task<UpdateCategoryDto> Get(int Id)
        ////        {
        ////            var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == Id /*&& !x.IsDelete*/);
        ////            //if (category == null)
        ////            //{
        ////            //    throw new EntityNotFoundException();
        ////            //}
        ////            return _mapper.Map<UpdateCategoryDto>(category);
        ////        }

        ////        public async Task<int> Delete(UpdateCategoryDto dto)
        ////        {
        ////            var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == dto.Id);
        ////            var mapper = _mapper.Map<UpdateCategoryDto, Category>(dto, category);
        ////            _Db.Categories.Remove(mapper);
        ////            await _Db.SaveChangesAsync();
        ////            return mapper.Id; 
        ////        }

        ////    }
        ////}





        //public async Task<List<CategoryViewModel>> GetCategoryName()
        //{
        //    var category = await _Db.Categories.ToListAsync();
        //    return _mapper.Map<List<CategoryViewModel>>(category);
        //}


        //public async Task<int> Create(CreateCategoryDto dto)
        //{
        //    var category = _mapper.Map<Category>(dto);
        //    await _Db.Categories.AddAsync(category);
        //    await _Db.SaveChangesAsync();
        //    return category.Id;
        //}


        //public async Task<int> Update(UpdateCategoryDto dto)
        //{
        //    var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == dto.Id);
        //    if (category == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }
        //    var updatedCategory = _mapper.Map<UpdateCategoryDto, Category>(dto, category);
        //    _Db.Categories.Update(updatedCategory);
        //    await _Db.SaveChangesAsync();
        //    return updatedCategory.Id;
        //}


        //public async Task<UpdateCategoryDto> Get(int Id)
        //{
        //    var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == Id);
        //    if (category == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }
        //    return _mapper.Map<UpdateCategoryDto>(category);
        //}


        //public async Task<int> Delete(int Id)
        //{
        //    var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == Id);
        //    if (category == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }
        //    _Db.Categories.Remove(category);
        //    await _Db.SaveChangesAsync();
        //    return category.Id;
        //}

        //public async Task<byte[]> ExportToExcel()
        //{
        //    var users = await _Db.Categories.ToListAsync();

        //    return ExcelHelpers.ToExcel(new Dictionary<string, ExcelColumn>
        //    {
        //        {"Name", new ExcelColumn("Name", 0)}

        //    }, new List<ExcelRow>(users.Select(e => new ExcelRow
        //    {
        //        Values = new Dictionary<string, string>
        //        {
        //            {"Name", e.Name},

        //        }
        //    })));

        //}
        /// <summary>
        /// /////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryViewModel>> GetAll()
        {
            var category = await _Db.Categories.Select(x=>new CategoryViewModel() { 
            Id= x.Id,
            Name =x.Name,
            DisplayOrder =x.DisplayOrder,
            CratedDateTime = x.CratedDateTime
            }).ToListAsync();
            //var mapper = _mapper.Map<List<CategoryViewModel>>(category);

            return category;
        }


        public async Task<List<CategoryViewModel>> GetCategoryName()
        {
            var category = await _Db.Categories.ToListAsync();/*Where(/*x => !x.IsDelete)*/
            return _mapper.Map<List<CategoryViewModel>>(category);
        }


        public async Task<int> Create(CreateCategoryDto dto)
        {
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


        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var category = await _Db.Categories.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateCategoryDto, Category>(dto, category);
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