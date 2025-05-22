using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.ViewModels.Account
{
    public class RegisterVM
    {
        [MinLength(4),MaxLength(30)]
        public string FullName {  get; set; }
        [MinLength(4), MaxLength(30)]
        public string Username {  get; set; }
        [MinLength(5), MaxLength(30)]
        public string Email { get; set; }
        [MinLength(4), MaxLength(30),DataType(DataType.Password)]
        public string Password { get; set; }
        [MinLength(4), MaxLength(30), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }
    }
}
