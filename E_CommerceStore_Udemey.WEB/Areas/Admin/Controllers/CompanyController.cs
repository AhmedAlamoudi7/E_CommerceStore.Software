using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.DATA.Data;
using E_CommerceStore_Udemey.Infrastructure.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceStore_Udemey.WEB.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly ICompanyService _companyService;

        public CompanyController(ApplicationDbContext db, ICompanyService companyService)
        {
            _Db = db;
            _companyService = companyService;
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
        public async Task<IActionResult> Create(CreateCompanyDto dto)
        {

            if (ModelState.IsValid)
            {
                await _companyService.Create(dto);
                TempData["success"] = "Create Company Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }




        public async Task<IActionResult> Edit(int Id)
        {

            var company = await _companyService.Get(Id);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCompanyDto dto)
        {

            if (ModelState.IsValid)
            {
                await _companyService.Update(dto);
                TempData["success"] = "Edit Company Successfully";
                return RedirectToAction("Index");
            }
            else { return View(dto); }
        }



        public async Task<IActionResult> Delete(int Id)
        {

            var company = await _companyService.Get(Id);
            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(UpdateCompanyDto dto)
        {

            await _companyService.Delete(dto);
            TempData["success"] = "Delete Company Successfully";
            return RedirectToAction("Index");

        }
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companyList = await  _companyService.GetAll();
            return Json(new { data = companyList });
        }
        #endregion

    }

}