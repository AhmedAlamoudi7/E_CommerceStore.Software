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

            var shoppingCart = await _Db.ShoppingCarts.Include(x => x.Product).ThenInclude(x => x.CoverType).Select(x=>new ShoppingCartViewModel() 
            { 
            Count =x.Count,
             ProductVM = new ProductViewModel()
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

             }

            }).ToListAsync();
            //var mapper = _mapper.Map<List<CategoryViewModel>>(category);

            return shoppingCart;
        }

        //public async Task<ShoppingCartViewModel> Get(int Id)
        //{
        //    var product = await _Db.ShoppingCarts.SingleOrDefaultAsync(x => x.ProductId == Id);
        //    ShoppingCartViewModel shop = new ShoppingCartViewModel();
        //    shop.ProductVM.Id = product;
        //    shop.Count = product.Count;
        //    return shop;
        //}

    }
}