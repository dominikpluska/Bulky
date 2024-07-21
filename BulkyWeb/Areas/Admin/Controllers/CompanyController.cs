using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var companyList = _unitOfWork.CompanyRepository.GetAll().ToList();
            return View(companyList);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = _unitOfWork.CompanyRepository.GetFirstOrDefault(x => x.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else {
                Company company = _unitOfWork.CompanyRepository.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            
            if (ModelState.IsValid)
            {

                if(company.Id == 0)
                {
                    _unitOfWork.CompanyRepository.Add(company);
                }
                else
                {
                    _unitOfWork.CompanyRepository.Update(company);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company created!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error!";
                return View(company);
            }
            

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.CompanyRepository.GetFirstOrDefault(x => x.Id == id);
            if (companyToBeDeleted == null)
            {
                return NotFound();
            }

            _unitOfWork.CompanyRepository.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Company has been deleted!" });
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companys = _unitOfWork.CompanyRepository.GetAll().ToList();
            return Json(new {data = companys });
        }
        #endregion
    }
}

