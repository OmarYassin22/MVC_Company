using AutoMapper;
using Demo.Busiess.Interfaces;
using Demo.Busiess.Repositories;
using Demo.DataAccess.Models;
using Demo.Presentaion.Helpers;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Presentaion.Controllers
{
    [Authorize(Roles = "ReadAdmin,WriteAdmin")]

    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string search)
        {
            var users = mapper.Map<IEnumerable<UserViewModel>>(await userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                ImageUrl = u.ImageUrl,
                FirstName = u.FirstName,
                LastName = u.LastName,



            }).ToListAsync());
            if (search is not null)
                users = mapper.Map<IEnumerable<UserViewModel>>(await userManager.Users.Where(u => u.NormalizedEmail.Contains(search.ToUpper())).Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    ImageUrl = u.ImageUrl,
                    FirstName = u.FirstName,
                    LastName = u.LastName,

                }).ToListAsync());


            foreach (var user in users)
            {
                user.Roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id));
            }
            return View(users);
        }

        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var dbuser = await userManager.FindByIdAsync(id.ToString());
            var res = mapper.Map<UserViewModel>(dbuser);
            res.Roles = await userManager.GetRolesAsync(dbuser);

            return View(ViewName, res);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            //ViewData["Departments"] = departmentRepository.GetAll();
            return await Details(id, "Update");

        }
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var dbroles = roleManager.Roles.ToList();
                var dbuser = await userManager.FindByEmailAsync(model.Email);
                dbuser.UserName = model.UserName;
                dbuser.FirstName = model.FirstName;
                dbuser.LastName = model.LastName;
                dbuser.Email = model.Email;
                for (int i = 0; i < dbroles.Count; i++)
                {
                    await userManager.RemoveFromRoleAsync(dbuser, dbroles[i].Name);

                }
                if (model.Roles is not null)
                {
                    foreach (var role in model.Roles)
                    {
                        var dbrole = await roleManager.FindByIdAsync(role);
                        await userManager.AddToRoleAsync(dbuser, dbrole.Name);


                    }
                }


                if (model.Image is not null)
                {
                    if (dbuser.ImageUrl is not null)
                        DocumentSettings.DeleteFile(dbuser.ImageUrl, "images");
                    dbuser.ImageUrl = DocumentSettings.Upload(model.Image, "images");
                }
                var result = await userManager.UpdateAsync(dbuser);
                if (result.Succeeded)
                {
                    TempData["Updated"] = $"{model.UserName} has been updated ";
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
                return View(model);
            }
            return View(model);
        }



        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"{user.UserName} has been deleted";
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }

                    return View(model);
                }
                return View(model);

            }
            return View(model);
        }
   
    }
}
