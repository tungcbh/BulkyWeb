using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult RoleManagement(string userId)
        {

            RoleManagementVM RoleVM = new RoleManagementVM()
            {
                ApplicationUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId),
                RoleList = _roleManager.Roles.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _unitOfWork.CompanyRepository.GetAll().Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            RoleVM.ApplicationUser.Role =
                _userManager.GetRolesAsync(_unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId)).GetAwaiter().GetResult().FirstOrDefault();

            return View(RoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleVM)
        {
            ApplicationUser user = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == roleVM.ApplicationUser.Id);
            string oldRole = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

            if (roleVM.ApplicationUser.Role != oldRole)
            {
                if (roleVM.ApplicationUser.Role == SD.Role_Company)
                {
                    user.CompanyId = roleVM.ApplicationUser.CompanyId;
                }
                if (oldRole == SD.Role_Company) user.CompanyId = null;
            }
            if (roleVM.ApplicationUser.Role == oldRole && oldRole == SD.Role_Company)
            {
                user.CompanyId = roleVM.ApplicationUser.CompanyId;
            }
            //user property not contain role, only exist in view model
            _unitOfWork.ApplicationUserRepository.Update(user);
            _unitOfWork.Save();

            _userManager.RemoveFromRoleAsync(user, oldRole).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, roleVM.ApplicationUser.Role).GetAwaiter().GetResult();


            return RedirectToAction("Index");
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {

            List<ApplicationUser> userList = _unitOfWork.ApplicationUserRepository.GetAll(includeProperties: "Company").ToList();
            foreach (var user in userList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = "None"
                    };
                }
            }

            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string? id)
        {
            var user = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                //current lock -> unlock
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                //lock
                user.LockoutEnd = DateTime.Now.AddYears(10);
            }

            _unitOfWork.ApplicationUserRepository.Update(user);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Locking/unlocking successful" });
        }
        #endregion
    }

}