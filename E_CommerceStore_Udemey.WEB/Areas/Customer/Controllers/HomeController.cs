using E_CommerceStore_Udemey.Core.Constants;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices;
using E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices;
using E_CommerceStore_Udemey.Infrastructure.Services.ProductService;
using E_CommerceStore_Udemey.Infrastructure.Services.Repository.IRepository;
using E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICoverTypeService _coverTypeService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ApplicationDbContext _Db;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ApplicationDbContext Db, IShoppingCartService shoppingCartService, IProductService productService, ICategoryService categoryService, ICoverTypeService coverTypeService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _coverTypeService = coverTypeService;
            _shoppingCartService = shoppingCartService;
            _Db = Db;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
         var productList =await  _productService.GetAll();

            return View(productList);
        }

        public async Task<IActionResult> Details(int productId)
        {
            ShopCartVm shop = new()
            {
                Product = _Db.Products.Include(x => x.Category).Include(x => x.CoverType).SingleOrDefault(x => x.Id == productId),
                Count = 2,
                ProductId = productId

            };

            return View(shop);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public async Task<IActionResult> Details(ShoppingCart
        //    shoppingCart)
        //{
        //    // var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    // var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    // shoppingCart.UserId = claim.Value;


        //    // //ShoppingCart cartFromDb = await _Db.ShoppingCarts.Where(u=>u.ProductId == shoppingCart.ProductId).SingleOrDefaultAsync(
        //    // //     u => u.UserId == claim.Value );

        //    // //if (cartFromDb == null)
        //    // ////{
        //    // //  await  _Db.ShoppingCarts.AddAsync(shoppingCart);
        //    // //  await _Db.SaveChangesAsync();

        //    // //}
        //    // //else
        //    // //{
        //    // //   _shoppingCartService.IncrementCount(cartFromDb, shoppingCart.Count);
        //    // //}


        //    //await _shoppingCartService.Create(shoppingCart);




        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    shoppingCart.UserId = claim.Value;

        //    ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
        //        u => u.UserId == claim.Value && u.ProductId == shoppingCart.ProductId);


        //    if (cartFromDb == null)
        //    {

        //        _unitOfWork.ShoppingCart.Add(shoppingCart);
        //        _unitOfWork.Save();
        //        HttpContext.Session.SetInt32(RolesConstant.SessionCart,
        //       _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value).ToList().Count);
        //    }
        //    else
        //    {
        //        _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
        //        _unitOfWork.Save();
        //    }

        //    return RedirectToAction(nameof(Index));

        //}






        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.UserId = claim.Value;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.UserId == claim.Value && u.ProductId == shoppingCart.ProductId);


            if (cartFromDb == null)
            {

                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(RolesConstant.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value).ToList().Count);
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
            }


            return RedirectToAction(nameof(Index));
        }







        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
