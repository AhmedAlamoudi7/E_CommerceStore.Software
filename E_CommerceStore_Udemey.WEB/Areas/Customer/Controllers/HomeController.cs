using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices;
using E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices;
using E_CommerceStore_Udemey.Infrastructure.Services.ProductService;
using E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext Db, IShoppingCartService shoppingCartService, IProductService productService, ICategoryService categoryService, ICoverTypeService coverTypeService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _coverTypeService = coverTypeService;
            _shoppingCartService = shoppingCartService;
            _Db = Db;
        }

        public async Task<IActionResult> Index()
        {
         var productList =await  _productService.GetAll();

            return View(productList);
        }

        //[HttpGet]
        //public async Task<IActionResult> Details(int Id)
        //{
        //    //var product = await _productService.Detailes(Id);
        //    //ShopingCartsViewModel shop = new()
        //    //{
        //    //    Product = product,
        //    //    Count = 1,

        //    //};
        //    ViewData["category"] = new SelectList(await _categoryService.GetCategoryName(), "Id", "Name");
        //    ViewData["coverType"] = new SelectList(await _coverTypeService.GetCoverTypeName(), "Id", "CoverName");
        //    return View(shop);
        //}
        public async Task<IActionResult> Details(int Id)
        {
            shopCartVm shop = new()
            {
                Product = _Db.Products.Include(x=>x.Category).Include(x=>x.CoverType).SingleOrDefault(x => x.Id == Id),
                Count = 2


            };
            return View(shop);
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
