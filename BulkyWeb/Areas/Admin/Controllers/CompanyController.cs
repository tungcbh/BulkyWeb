using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment; // Inject IWebHostEnvironment
        }
        public IActionResult Index()
        {
            List<Company> companyList = _unitOfWork.CompanyRepository.GetAll().ToList();

            return View(companyList);
        }

        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else //update
            {
                Company company = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {

                // Nếu là sản phẩm mới, thêm vào repository
                if (company.Id == 0)
                {
                    _unitOfWork.CompanyRepository.Add(company);
                }
                else // Nếu là cập nhật, cập nhật trong repository
                {
                    _unitOfWork.CompanyRepository.Update(company);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Create Successful";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                return View(company);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Company? company = _unitOfWork.CompanyRepository.Get(o => o.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            _unitOfWork.CompanyRepository.Remove(company);
            _unitOfWork.Save();
            TempData["success"] = "Company Delete Successful";
            return RedirectToAction("Index", "Company");
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {

            List<Company> companyList = _unitOfWork.CompanyRepository.GetAll().ToList();

            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToDelete = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
            if (companyToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }


            _unitOfWork.CompanyRepository.Remove(companyToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }

}