using AutoMapper;
using Demo.Busiess.Interfaces;
using Demo.Controllers;
using Demo.DataAccess.Models;
using Demo.Presentaion.Helpers;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Demo.Presentaion.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		#region SingUp

		public IActionResult SignUp()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(model.UserName);
				if (user is null)
				{
					user = await userManager.FindByEmailAsync(model.Email);
					if (user is null)
					{

						user = new AppUser()
						{

							UserName = model.UserName,
							Email = model.Email,
							FirstName = model.FirstName,
							LastName = model.LastName,
							IAccept = model.IAccept,
							ImageUrl=DocumentSettings.Upload(model.Image, "images")

						};

						var result = await userManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
							return RedirectToAction(nameof(SignIn));
						foreach (var item in result.Errors)
							ModelState.AddModelError(string.Empty, item.Description);
						return View(model);


					}
					ModelState.AddModelError("Errors", "Email Already Exist");
				}
				ModelState.AddModelError("Errors", "User Already Exist!");
			}


			return View(model);
		}
		#endregion

		#region SignIn

		public IActionResult SignIn()
		{ return View(); }
		[HttpPost]
		public async Task<IActionResult> SignIn(SingInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.UserName);
				if (user is not null)
				{
					if (await userManager.CheckPasswordAsync(user, model.Password))
					{
						var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe
							, false);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", controllerName: "Home");
						}

					}
					ModelState.AddModelError(string.Empty, "Wrong Password");
					return View(model);

				}
				ModelState.AddModelError(string.Empty, "Email Not Exist!!");

			}
			return View(model);
		}

		#endregion
		#region SingOut
		public new async Task<IActionResult> SignOut()
		{

			await signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion
		#region Forget Password
		public IActionResult ForgetPassword()
		{
			return View();
		}


		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var token = await userManager.GeneratePasswordResetTokenAsync(user);
					var url = Url.Action(action: "ResetPassword", controller: "Account", new { email = model.Email, token = token }, Request.Scheme);
					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Your Password",
						Body = url
					};
					EmailSettings.Send(email);
					return RedirectToAction(nameof(CheckYourInbox));

				}
				ModelState.AddModelError(string.Empty, "UserName Not Exist!!");

			}

			return View(model);
		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region Reset Password
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);
					if (result.Succeeded)
					{
						return RedirectToAction(controllerName: "Home",actionName: nameof(HomeController.Index));
					}
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError(string.Empty, item.Description);

						return View(model);

					}

				}
				ModelState.AddModelError(string.Empty, "User Doesn't Exist");

				return View(model);
			}
			return View(model);
		}

        #endregion


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
