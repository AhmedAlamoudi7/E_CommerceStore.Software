using E_CommerceStore_Udemey.Core.Constans;
using E_CommerceStore_Udemey.Core.Constants;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Helpers;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesConstant.Role_Admin)]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly ICategoryService _categoryService;

        public CategoryController(ApplicationDbContext db, ICategoryService categoryService)
        {
            _Db = db;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            //var categoryDb = new Category();
            //categoryDb.Name = category.Name;
            //categoryDb.DisplayOrder = category.DisplayOrder;

            // validMessage in Sammerry just appear
            if (dto.Name == dto.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
            }
            if (ModelState.IsValid)
            {
                await _categoryService.Create(dto);
                TempData["success"] = "Create Category Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }




        public async Task<IActionResult> Edit(int Id)
        {

            var category = await _categoryService.Get(Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoryDto dto)
        {

            // validMessage in Sammerry just appear
            if (dto.Name == dto.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
            }
            if (ModelState.IsValid)
            {
                await _categoryService.Update(dto);
                TempData["success"] = "Edit Category Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }



        public async Task<IActionResult> Delete(int Id)
        {

            var category = await _categoryService.Get(Id);
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(UpdateCategoryDto dto)
        {

            await _categoryService.Delete(dto);
            TempData["success"] = "Delete Category Successfully";
            return RedirectToAction("Index");

        }
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryList =await  _categoryService.GetAll();
            return Json(new { data = categoryList });
        }
        #endregion

    }

}