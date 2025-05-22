using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SimulationDay5.DataAccessLayer;
using SimulationDay5.Models;
using SimulationDay5.ViewModels.Persons;
using SimulationDay5.ViewModels.Positions;

namespace SimulationDay5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController(SafeCamDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Persons.Select(x => new PersonGetVM
            {
                FullName = x.FullName,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                PositionName = x.Position.Name
            }).ToListAsync();
            return View(datas);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _context.Positions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateVM vm)
        {
            ViewBag.Positions = await _context.Positions.ToListAsync();
            if (!ModelState.IsValid)
                return View(vm);
            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.ContentType.StartsWith("image"))
                    ModelState.AddModelError("ImageFile", "File must be an image ");
                if (vm.ImageFile.Length > 1024 * 1024 * 2)
                    ModelState.AddModelError("ImageFile", "File size must be less than 200kb");
            }
            if (!await _context.Persons.AnyAsync(x => x.Id == x.PositionId))
            {
                ModelState.AddModelError("PositionId", "Position does not exist");
                return View(vm);
            }

            string newImgName = Guid.NewGuid().ToString() + vm.ImageFile!.FileName;
            string path = Path.Combine("wwwroot", "imgs", "persons", newImgName);
            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            await vm.ImageFile.CopyToAsync(fs);

            Person person = new Person
            {
                FullName = vm.FullName,
                PositionId = vm.PositionId,
                ImageUrl= newImgName,
            };

            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Positions = await _context.Positions.ToListAsync();
            if (!id.HasValue || id.Value < 1)
                return View();
            var person = await _context.Persons.Select(x => new PersonUpdateVM
            {
                Id = x.Id,
                FullName = x.FullName,
                PositionId = x.PositionId,
            }).FirstOrDefaultAsync();
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, PersonUpdateVM vm)
        {
            ViewBag.Positions = await _context.Positions.ToListAsync();
            if (!ModelState.IsValid)
                return View(vm);
            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.ContentType.StartsWith("image"))
                    ModelState.AddModelError("ImageFile", "File must be an image ");
                if (vm.ImageFile.Length > 1024 * 1024 * 2)
                    ModelState.AddModelError("ImageFile", "File size must be less than 200kb");

                string newImgName = Guid.NewGuid().ToString() + vm.ImageFile!.FileName;
                string path = Path.Combine("wwwroot", "imgs", "persons", newImgName);
                using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                await vm.ImageFile.CopyToAsync(fs);
            }
            if (!await _context.Persons.AnyAsync(x => x.Id == x.PositionId))
            {
                ModelState.AddModelError("PositionId", "Position does not exist");
                return View(vm);
            }
            var person=await _context.Persons.FindAsync(id);
            if (person == null)
                return NotFound();
            person.FullName=vm.FullName;
            person.Id=vm.Id;
            person.PositionId=vm.PositionId;
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");



        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            var result = await _context.Persons.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == null) return NotFound();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
