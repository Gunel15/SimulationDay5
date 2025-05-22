using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.ViewModels.Account
{
    public class LoginVM
    {
        [MinLength(5), MaxLength(30)]
        public string Email { get; set; }
        [MinLength(4), MaxLength(30), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
