using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Infrastructure.Services.CoverTypeServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
       private readonly ICoverTypeService _coverTypeService;

        public CoverTypeController(ICoverTypeService coverTypeService)
        {
            _coverTypeService = coverTypeService;
        }


        public async Task<IActionResult> Index()
        {

           var coverType = await _coverTypeService.GetAll();
            return View(coverType);
        }



        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCoverTypeDto dto)
        {
            if (ModelState.IsValid)
            {
                await _coverTypeService.Create(dto);
                TempData["success"] = "Create cover Type Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }




        public async Task<IActionResult> Edit(int Id)
        {

            var coverType = await _coverTypeService.Get(Id);
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCoverTypeDto dto)
        {
            if (ModelState.IsValid)
            {
                await _coverTypeService.Update(dto);
                TempData["success"] = "Edit cover Type Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }



        public async Task<IActionResult> Delete(int Id)
        {

            var coverType = await _coverTypeService.Get(Id);
            return View(coverType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(UpdateCoverTypeDto dto)
        {

            await _coverTypeService.Delete(dto);
            TempData["success"] = "Delete Cover Type Successfully";
            return RedirectToAction("Index");

        }
    }

}