using E_CommerceStore_Udemey.Core.Constans;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.ProductService;
using E_CommerceStore_Udemey.Infrastructure.Services.ShoppingCartServices;
using E_CommerceStore_Udemey.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Admin")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public ShoppingCartController(ApplicationDbContext db, IProductService productService, IUserService userService, IShoppingCartService shoppingCartService)
        {
            _Db = db;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewData["product"] = new SelectList(await _productService.GetAll(), "Id", "Title");
            ViewData["user"] = new SelectList(await _userService.GetAll(), "Id", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShoppingCartDto dto)
        {

            if (ModelState.IsValid)
            {
                ViewData["product"] = new SelectList(await _productService.GetAll(), "Id", "Title");
                ViewData["user"] = new SelectList(await _userService.GetAll(), "Id", "FullName");
                await _shoppingCartService.Create(dto);
                TempData["success"] = "Create Shopping Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }

        public async Task<IActionResult> Edit(int Id)
        {
            ViewData["product"] = new SelectList(await _productService.GetAll(), "Id", "Title");
            ViewData["user"] = new SelectList(await _userService.GetAll(), "Id", "FullName");
            var shoppingCart = await _shoppingCartService.Get(Id);
            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateShoppingCartDto dto)
        {

            if (ModelState.IsValid)
            {
                ViewData["product"] = new SelectList(await _productService.GetAll(), "Id", "Title");
                ViewData["user"] = new SelectList(await _userService.GetAll(), "Id", "FullName");
                await _shoppingCartService.Update(dto);
                TempData["success"] = "Edit ShoppingCart Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }



        public async Task<IActionResult> Delete(int Id)
        {

            var shoppingCart = await _shoppingCartService.Get(Id);
            return View(shoppingCart);
        }


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shoppingCartList = await  _shoppingCartService.GetAll();
            return Json(new { data = shoppingCartList });
        }
        #endregion

    }

}