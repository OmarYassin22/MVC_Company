using AutoMapper;
using Demo.DataAccess.Models;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Presentaion.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string search)
        {

            var roles = roleManager.Roles.Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name }).ToList();
            if (!string.IsNullOrEmpty(search))
                roles = roles.Where(r => r.Name.ToUpper().Contains(search.ToUpper())).Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name }).ToList();
            ViewData["search"] = search;
            for (int i = 0; i < roles.Count(); i++)
            {
                roles[i].Users= userManager.GetUsersInRoleAsync(roles[i].Name).Result.Select(u=>u.UserName).ToList();

            }
            
            return View(roles);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole() { Name = model.Name };
                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded) return RedirectToAction(nameof(Index));
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return View(model);

                }
                ModelState.AddModelError(string.Empty, "Role Already Exist");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            var dbrole = await roleManager.FindByIdAsync(id.ToString());
            var role = new RoleViewModel() { Id = dbrole.Id, Name = dbrole.Name };
            return View(viewname, role);
        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbrole = await roleManager.FindByIdAsync(model.Id);
                dbrole.Name = model.Name;
                var result = await roleManager.UpdateAsync(dbrole);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id.ToString());
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }
            return View(model);
        }
        public async Task<IActionResult> AddOrRemoveUser(string id)
        {
            if (id is null)
                return NotFound();
            var role = await roleManager.FindByIdAsync(id);
            TempData["Id"] = role.Id;
            var usersinrole = new List<UserInRoleViewModel>();
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userinrole = new UserInRoleViewModel() { UserId = user.Id, UserName = user.UserName };
                userinrole.IsSelected = await userManager.IsInRoleAsync(user, role.Name);
                usersinrole.Add(userinrole);
            }


            return View(usersinrole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string id, List<UserInRoleViewModel> userinrole)
        {
            if (id is null)
                return NotFound();
            var role = await roleManager.FindByIdAsync(id);
            if (ModelState.IsValid)
            {
                foreach (var user in userinrole)
                {
                    var dbuser = await userManager.FindByIdAsync(user.UserId);
                    if (user.IsSelected && ! await userManager.IsInRoleAsync(dbuser, role.Name))
                        await userManager.AddToRoleAsync(dbuser, role.Name);
                    else if (!user.IsSelected && await userManager.IsInRoleAsync(dbuser, role.Name))
                        await userManager.RemoveFromRoleAsync(dbuser, role.Name);
                }

            return RedirectToAction(nameof(Update),new { id=id });
            }



            return View(userinrole);
            }
   
    }
}
