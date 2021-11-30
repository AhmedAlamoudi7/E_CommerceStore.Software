using E_CommerceStore_Udemey.Core.Constans;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.Helpers;
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
    //    [Area("Admin")]
    //    public class CategoryController : Controller
    //    {
    //        private readonly ApplicationDbContext _Db;
    //        private readonly ICategoryService _categoryService;

    //        public CategoryController(ApplicationDbContext db, ICategoryService categoryService)
    //        {
    //            _Db = db;
    //            _categoryService = categoryService;
    //        }

    //        //        public IActionResult Index()
    //        //        {

    //        //            return View();
    //        //        }

    //        //        public async Task<JsonResult> GetCategoryData(Pagination pagination, Query query)
    //        //        {
    //        //            var result = await _categoryService.GetAll(pagination, query);
    //        //            return Json(result);
    //        //        }



    //        //        public IActionResult Create()
    //        //        {

    //        //            return View();
    //        //        }

    //        //        [HttpPost]
    //        //        [ValidateAntiForgeryToken]
    //        //        public async Task<IActionResult> Create(CreateCategoryDto dto)
    //        //        {
    //        //            //var categoryDb = new Category();
    //        //            //categoryDb.Name = category.Name;
    //        //            //categoryDb.DisplayOrder = category.DisplayOrder;

    //        //            // validMessage in Sammerry just appear
    //        //            if (dto.Name == dto.DisplayOrder.ToString())
    //        //            {
    //        //                ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
    //        //            }
    //        //            if (ModelState.IsValid)
    //        //            {
    //        //                 await _categoryService.Create(dto);             
    //        //                TempData["success"] = "Create Category Successfully";
    //        //                return RedirectToAction("Index");
    //        //            }
    //        //            else { return View(dto); }
    //        //        }




    //        //        public async Task<IActionResult> Edit(int Id)
    //        //        {

    //        //            var category = await _categoryService.Get(Id);
    //        //            return View(category);
    //        //        }

    //        //        [HttpPost]
    //        //        [ValidateAntiForgeryToken]
    //        //        public async Task<IActionResult> Edit(UpdateCategoryDto  dto)
    //        //        {
    //        //            //var categoryDb = new Category();
    //        //            //categoryDb.Name = category.Name;
    //        //            //categoryDb.DisplayOrder = category.DisplayOrder;

    //        //            // validMessage in Sammerry just appear
    //        //            if (dto.Name == dto.DisplayOrder.ToString())
    //        //            {
    //        //                ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
    //        //            }
    //        //            if (ModelState.IsValid)
    //        //            {
    //        //                await _categoryService.Update(dto);
    //        //                TempData["success"] = "Edit Category Successfully";
    //        //                return RedirectToAction("Index");
    //        //            }
    //        //            else { return View(dto); }
    //        //        }



    //        //        public async Task<IActionResult> Delete(int Id)
    //        //        {

    //        //            var category = await _categoryService.Get(Id); 
    //        //            return View(category);
    //        //        }


    //        //        [HttpPost]
    //        //        [ValidateAntiForgeryToken]
    //        //        public async Task<IActionResult> DeletePost(UpdateCategoryDto dto)
    //        //        {

    //        //            await _categoryService.Delete(dto);
    //        //            TempData["success"] = "Delete Category Successfully";
    //        //            return RedirectToAction("Index");

    //        //        }








    //        //    }

    //        //}
    //        [HttpGet]
    //        public IActionResult Index()
    //        {
    //            return View();
    //        }

    //        public async Task<JsonResult> GetCategoryData(Pagination pagination, Query query)
    //        {
    //            var result = await _categoryService.GetAll(pagination, query);
    //            return Json(result);
    //        }

    //        [HttpGet]
    //        public IActionResult Create()
    //        {
    //            return View();
    //        }

    //        [HttpPost]
    //        public async Task<IActionResult> Create(CreateCategoryDto dto)
    //        {
    //            //if (ModelState.IsValid)
    //            //{
    //                await _categoryService.Create(dto);
    //                return Ok(Results.AddSuccessResult());
    //            //}
    //            //return View(dto);
    //        }

    //        [HttpGet]
    //        public async Task<IActionResult> Update(int id)
    //        {
    //            var user = await _categoryService.Get(id);
    //            return View(user);
    //        }

    //        [HttpPost]
    //        public async Task<IActionResult> Update(UpdateCategoryDto dto)
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                await _categoryService.Update(dto);
    //                return Ok(Results.EditSuccessResult());
    //            }
    //            return View(dto);
    //        }

    //        [HttpGet]
    //        public async Task<IActionResult> Delete(int id)
    //        {
    //            await _categoryService.Delete(id);
    //            return Ok(Results.DeleteSuccessResult());
    //        }

    //        [HttpGet]
    //        public async Task<IActionResult> ExportToExcel()
    //        {
    //            return File(await _categoryService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_Category.xlsx");
    //        }
    //    }
    //}





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