using E_CommerceStore_Udemey.WEB.Data;
using E_CommerceStore_Udemey.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _Db;

        public CategoryController(ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            var category = _Db.Categories.ToList();
            return View(category);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            //var categoryDb = new Category();
            //categoryDb.Name = category.Name;
            //categoryDb.DisplayOrder = category.DisplayOrder;

            // validMessage in Sammerry just appear
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
            }
            if (ModelState.IsValid)
            {
                await _Db.Categories.AddAsync(category);
                await _Db.SaveChangesAsync();
                TempData["success"] = "Create Category Successfully";
                return RedirectToAction("Index");
            }
            else { return View(category); }
        }




        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _Db.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            //var categoryDb = new Category();
            //categoryDb.Name = category.Name;
            //categoryDb.DisplayOrder = category.DisplayOrder;

            // validMessage in Sammerry just appear
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomErorr", "The DisplayOrder cannot exactly math the Name.");
            }
            if (ModelState.IsValid)
            {
                _Db.Categories.Update(category);
                await _Db.SaveChangesAsync();
                TempData["success"] = "Edit Category Successfully";

                return RedirectToAction("Index");
            }
            else { return View(category); }
        }





        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _Db.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var category = _Db.Categories.Find(Id);

            if (category == null)
            {
                return NotFound();
            }
            _Db.Categories.Remove(category);
            _Db.SaveChanges();
            TempData["success"] = "Delete Category Successfully";

            return RedirectToAction("Index");
        }


    }

}
