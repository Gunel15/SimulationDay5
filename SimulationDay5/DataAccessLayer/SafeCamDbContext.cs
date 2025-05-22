using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using SimulationDay5.Models;

namespace SimulationDay5.DataAccessLayer
{
    public class SafeCamDbContext:DbContext
    {
        public SafeCamDbContext(DbContextOptions opt):base(opt)
        {
            
        }

        public DbSet<Position>Positions { get; set; }
        public DbSet<Person>Persons { get; set; }
    }
}
