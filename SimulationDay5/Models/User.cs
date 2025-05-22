using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SimulationDay5.Models
{
    public class User:IdentityUser
    {
        [MaxLength(50)]
        public string FullName {  get; set; }
    }
}
