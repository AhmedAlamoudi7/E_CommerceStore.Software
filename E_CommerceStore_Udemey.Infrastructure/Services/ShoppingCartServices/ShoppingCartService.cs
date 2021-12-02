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

namespace E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _Db;
        private readonly IMapper _mapper;

        public ShoppingCartService(ApplicationDbContext db, IMapper mapper)
        {
            _Db = db;
            _mapper = mapper;
        }



        public async Task<List<ShoppingCartViewModel>> GetAll()
        {

            var shoppingCart = await _Db.ShoppingCarts.Include(x=>x.User).ThenInclude(x=>x.Company).Include(x => x.Product).ThenInclude(x => x.CoverType).Select(x=>new ShoppingCartViewModel() 
            { 
            Count =x.Count,
            Price =x.Price,
            Id =x.Id,
             ProductVm = new ProductViewModel()
             {
                 Id = x.Product.Id,
                 Title = x.Product.Title,
                 Author = x.Product.Author,
                 CategoryVMName = x.Product.Category.Name,
                 CoverTypeVMName = x.Product.CoverType.CoverName,
                 Description = x.Product.Description,
                 ImageUrl = x.Product.ImageUrl.ToString(),
                 ISBN = x.Product.ISBN,
                 ListPrice = x.Product.ListPrice,
                 Price = x.Product.Price,
                 Price100 = x.Product.Price100
                 
             },
             UserVm =new UserViewModel() { 
            FullName = x.User.FullName,
            DOB =x.User.DOB.ToString(),
        
                 CompanyVm =new CompanyViewModel()
            {
                Id =x.User.Company.Id,
                Name =x.User.Company.Name,
                City = x.User.Company.City,
                State = x.User.Company.State,
                PhoneNumber = x.User.PhoneNumber,
                PostalCode = x.User.Company.PostalCode,
                StreetAddress = x.User.Company.StreetAddress
            },
             PhoneNumber =x.User.PhoneNumber
             
             }

            }).ToListAsync();
            return shoppingCart;
        }


        public async Task<int> Create(CreateShoppingCartDto dto)
        {
            var mapper = _mapper.Map<CreateShoppingCartDto, ShoppingCart>(dto);
            await _Db.ShoppingCarts.AddAsync(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }

        public int IncrementCount(ShoppingCart dto,int count)
        {
            dto.Count += count;
            // _Db.ShoppingCarts.Update(dto);
            //await _Db.SaveChangesAsync();
            return dto.Count;
        }
        public async Task<int> DecrementCount(CreateShoppingCartDto dto, int count)
        {
            var mapper = _mapper.Map<CreateShoppingCartDto, ShoppingCart>(dto);
            dto.Count -= count;
            //await _Db.ShoppingCarts.AddAsync(mapper);
            //await _Db.SaveChangesAsync();
            return dto.Count;
        }
        public async Task<int> Update(UpdateShoppingCartDto dto)
        {
            var shoppingCart = await _Db.ShoppingCarts.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var mapper = _mapper.Map<UpdateShoppingCartDto, ShoppingCart>(dto, shoppingCart);
            _Db.ShoppingCarts.Update(mapper);
            await _Db.SaveChangesAsync();
            return mapper.Id;
        }




        public async Task<UpdateShoppingCartDto> Get(int Id)
        {
            var shopping = await _Db.ShoppingCarts.SingleOrDefaultAsync(x => x.Id == Id);
            if (shopping == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateShoppingCartDto>(shopping);
        }





        public async Task<int> Delete(int id)
        {
            var shopping = await _Db.ShoppingCarts.SingleOrDefaultAsync(x => x.Id == id);
            _Db.ShoppingCarts.Remove(shopping);
            await _Db.SaveChangesAsync();
            return shopping.Id;
        }

}

}