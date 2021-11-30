using AutoMapper;
using E_CommerceStore_Udemey.Core.Constans;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.FileSerice;
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
        private readonly IFileService _fileService;
        public ProductService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _Db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<ProductViewModel>> GetAll() {
            var product = await _Db.Products.Include(x=>x.Category).Include(x=>x.CoverType)
                .Select(x=>new ProductViewModel() { 
                Id = x.Id,
                Title =x.Title,
                Author =x.Author,
                CategoryVMName =x.Category.Name,
                CoverTypeVMName =x.CoverType.CoverName,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl.ToString(),
                    ISBN = x.ISBN,
                    ListPrice = x.ListPrice,
                    Price = x.Price,
                    Price100 = x.Price100
                }).ToListAsync();
            //var mapper = _mapper.Map<List<ProductViewModel>>(product);

            return product;
        }


        public async Task<int> Create(CreateProductDto dto) {
            var mapper = _mapper.Map<CreateProductDto, Product>(dto);
            if (dto.ImageUrl != null)
            {
                mapper.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
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
            if (dto.ImageUrl != null)
            {
                mapper.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
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





        public async Task<int> Delete(int id)
        {
            var product = await _Db.Products.SingleOrDefaultAsync(x => x.Id == id);
            _Db.Products.Remove(product);
            await _Db.SaveChangesAsync();
            return product.Id;
        }

    }
}
