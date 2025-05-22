using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimulationDay5.Models;
using SimulationDay5.ViewModels.Account;

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
            User user=new User();
            {
                user.Email = vm.Email;
                user.FullName = vm.FullName;
                user.UserName=vm.Username;
            }
            
           _userManager.CreateAsync(user,vm.Password);
            
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Index","Home");
            
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
            var result=await _userManager.FindByEmailAsync(vm.Email);
            if (result == null)
                ModelState.AddModelError("", "Username or password is wrong");
            return RedirectToAction("Index", "Home");
        }
    }
}
