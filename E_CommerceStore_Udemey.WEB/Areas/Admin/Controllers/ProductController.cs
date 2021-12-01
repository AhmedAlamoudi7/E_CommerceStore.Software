using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices;
using E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices;
using E_CommerceStore_Udemey.Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICoverTypeService _coverTypeService;

        public ProductController(ApplicationDbContext db, IProductService productService, ICategoryService categoryService, ICoverTypeService coverTypeService)
        {
            _Db = db;
            _productService = productService;
            _categoryService = categoryService;
            _coverTypeService = coverTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewData["category"] = new SelectList(await _categoryService.GetCategoryName(), "Id", "Name");
            ViewData["coverType"] = new SelectList(await _coverTypeService.GetCoverTypeName(), "Id", "CoverName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            


            //if (dto.Name == dto.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
            //}
            if (ModelState.IsValid)
            {
                ViewData["category"] = new SelectList(await _categoryService.GetCategoryName(), "Id", "Name");
                ViewData["coverType"] = new SelectList(await _coverTypeService.GetCoverTypeName(), "Id", "CoverName");
                await _productService.Create(dto);             
                TempData["success"] = "Create Product Successfully";
                return RedirectToAction("Index");
            }
           

            else { return View(dto); }
        }




        public async Task<IActionResult> Edit(int Id)
        {
            ViewData["category"] = new SelectList(await _categoryService.GetCategoryName(), "Id", "Name");
            ViewData["coverType"] = new SelectList(await _coverTypeService.GetCoverTypeName(), "Id", "CoverName");
            var product = await _productService.Get(Id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductDto dto)
        {

            if (ModelState.IsValid)
            {
                ViewData["category"] = new SelectList(await _categoryService.GetCategoryName(), "Id", "Name");
                ViewData["coverType"] = new SelectList(await _coverTypeService.GetCoverTypeName(), "Id", "CoverName");

                await _productService.Update(dto);
                TempData["success"] = "Edit Product Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }




        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _productService.Delete(id);
            return Json(new { success = true, message = "Delete Successful" });

        }




        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productList = await _productService.GetAll();
            return Json(new { data = productList });
        }
        #endregion





    }

}
