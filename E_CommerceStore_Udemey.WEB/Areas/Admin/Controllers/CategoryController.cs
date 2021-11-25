using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.DATA.Models;
using E_CommerceStore_Udemey.Infrastructure.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly ICategoryService _categoryService;

        public CategoryController(ApplicationDbContext db, ICategoryService categoryService)
        {
            _Db = db;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var category =await  _categoryService.GetAll();
            return View(category);
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
        public async Task<IActionResult> Edit(UpdateCategoryDto  dto)
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
            TempData["success"] = "Edit Category Successfully";
            return RedirectToAction("Index");

        }
    }

}
