using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using SimulationDay5.Models;

namespace SimulationDay5.DataAccessLayer
{
    public class SafeCamDbContext:IdentityDbContext<User>   //dbni use ele
    {
        public SafeCamDbContext(DbContextOptions opt):base(opt)
        {
            
        }

        public DbSet<Position>Positions { get; set; }
        public DbSet<Person>Persons { get; set; }
    }
}
