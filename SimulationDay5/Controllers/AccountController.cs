using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimulationDay5.Models;
using SimulationDay5.ViewModels.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimulationDay5.Controllers
{
    public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new() { Name = "Admin" });
            await _roleManager.CreateAsync(new() { Name = "Member" });
            return Ok("Roles Created");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            User user = new()
            {
                Email = vm.Email,
                FullName = vm.FullName,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(user, vm.Password); //bura await yazzzzzzz
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                    return View(vm);
            }


            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(vm);

            }
            var passResult = await _signInManager.PasswordSignInAsync(user, vm.Password, isPersistent: false, lockoutOnFailure: false);
            if(!passResult.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateAdmin()
        {
            User admin = new()
            {
                FullName = "Admin",
                UserName = "admin",
                Email = "admin@com"
            };
            await _userManager.CreateAsync(admin, "Admin123@"); //boyuk herf yaaz
            await _userManager.AddToRoleAsync(admin, "Admin");
            return Ok("Admin created");
        }
    }
}
