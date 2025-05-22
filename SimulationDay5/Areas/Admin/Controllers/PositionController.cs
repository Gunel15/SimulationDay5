using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationDay5.DataAccessLayer;
using SimulationDay5.Models;
using SimulationDay5.ViewModels.Positions;

namespace SimulationDay5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController(SafeCamDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas=await _context.Positions.Select(x=>new PositionGetVM
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return View(datas);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(PositionCreateVM vm)
        {
            if (!ModelState.IsValid) 
                return View(vm);
            await _context.Positions.AddAsync(new Position
            {
                Name = vm.Name,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            var position=await _context.Positions.Where(x=>x.Id==id).Select(x=>new PositionUpdateVM
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync();
            return View(position);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int?id, PositionUpdateVM vm)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(vm);
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
                return NotFound();
            position.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            var result = await _context.Positions.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result==0) return NotFound();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
